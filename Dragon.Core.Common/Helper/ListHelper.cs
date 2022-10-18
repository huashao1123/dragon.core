using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Common
{
    public static class ListHelper
    {
        /// <summary>
        /// 将列表转换为树形结构
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">数据</param>
        /// <param name="rootwhere">根条件</param>
        /// <param name="childswhere">节点条件</param>
        /// <param name="addchilds">添加子节点</param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static List<T> ToTree<T>(this List<T> list, Func<T, T, bool> rootwhere, Func<T, T, bool> childswhere, Action<T, IEnumerable<T>> addchilds, T entity = default(T))
        {
            var treelist = new List<T>();
            //空树
            if (list == null || list.Count == 0)
            {
                return treelist;
            }
            if (!list.Any<T>(e => rootwhere(entity, e)))
            {
                return treelist;
            }

            //树根
            if (list.Any<T>(e => rootwhere(entity, e)))
            {
                treelist.AddRange(list.Where(e => rootwhere(entity, e)));
            }

            //树叶
            foreach (var item in treelist)
            {
                if (list.Any(e => childswhere(item, e)))
                {
                    var nodedata = list.Where(e => childswhere(item, e)).ToList();
                    foreach (var child in nodedata)
                    {
                        //添加子集
                        var data = list.ToTree(childswhere, childswhere, addchilds, child);
                        addchilds(child, data);
                    }
                    addchilds(item, nodedata);
                }
            }

            return treelist;
        }

        /// <summary>
        /// 将列表转换为树形结构
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">数据</param>
        /// <param name="rootwhere">根条件</param>
        /// <param name="childswhere">节点条件</param>
        /// <param name="addchilds">添加子节点</param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static List<T> ToTree<T>(this List<T> list, Func<T, bool> rootwhere, Func<T, T, bool> childswhere, Action<T, IEnumerable<T>> addchilds)
        {
            var treelist = new List<T>();
            //空树
            if (list == null || list.Count == 0)
            {
                return treelist;
            }
            if (!list.Any<T>(e => rootwhere(e)))
            {
                return treelist;
            }

            //树根
            if (list.Any<T>(e => rootwhere(e)))
            {
                treelist.AddRange(list.Where(e => rootwhere(e)));
            }

            //树叶
            foreach (var item in treelist)
            {
                if (list.Any(e => childswhere(item, e)))
                {
                    var nodedata = list.Where(e => childswhere(item, e)).ToList();
                    foreach (var child in nodedata)
                    {
                        //添加子集
                        var data = list.ToTree(childswhere, childswhere, addchilds, child);
                        addchilds(child, data);
                    }
                    addchilds(item, nodedata);
                }
            }

            return treelist;
        }


        private static List<T> BuildChildList<T>( List<T> list, string idName, string pIdName, object rootValue, bool isContainOneself)
        {
            var type = typeof(T);
            var idProp = type.GetProperty(idName);
            var pIdProp = type.GetProperty(pIdName);

            var kvpList = list.ToDictionary(x => x, v => idProp.GetValue(v).ObjToString());
            var groupKv = list.GroupBy(x => pIdProp.GetValue(x).ObjToString()).ToDictionary(k => k.Key, v => v.ToList());

            Func<string, List<T>> fc = null;
            fc = (rootVal) =>
            {
                var finalList = new List<T>();
                if (groupKv.TryGetValue(rootVal, out var nextChildList))
                {
                    finalList.AddRange(nextChildList);
                    foreach (var child in nextChildList)
                    {
                        finalList.AddRange(fc(kvpList[child]));
                    }
                }
                return finalList;
            };

            var result = fc(rootValue.ObjToString());

            if (isContainOneself)
            {
                var root = kvpList.FirstOrDefault(x => x.Value == rootValue.ObjToString()).Key;
                if (root != null)
                {
                    result.Insert(0, root);
                }
            }

            return result;
        }

        private static List<T> GetChildList<T>(Expression<Func<T, object>> parentIdExpression, Expression<Func<T, object>> idExpression, List<T> list, object rootValue, bool isContainOneself)
        {
            
            var parentIdName = GenMemberName(parentIdExpression);

            string pkName = GenMemberName(idExpression);
            var result = BuildChildList(list, pkName, parentIdName, rootValue, isContainOneself);
            return result;
        }
        /// <summary>
        /// 根据节点Id获取子节点集合(包含自己)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="parentIdExpression"></param>
        /// <param name="idExpression"></param>
        /// <param name="primaryKeyValue"></param>
        /// <param name="isContainOneself"></param>
        /// <returns></returns>
        public static async Task<List<T>> ToChildListAsync<T>(this IQueryable<T> query, Expression<Func<T, object>> parentIdExpression, Expression<Func<T, object>> idExpression, object primaryKeyValue, bool isContainOneself = true)
        {
            var list = await query.ToListAsync();
            return GetChildList(parentIdExpression, idExpression,  list, primaryKeyValue, isContainOneself);
        }

        private static string GenMemberName<T>(Expression<Func<T, object>> expression)
        {
            var exp = (expression as LambdaExpression).Body;
            if (exp is UnaryExpression)
            {
                exp = (exp as UnaryExpression).Operand;
            }
            var name = (exp as MemberExpression).Member.Name;
            return name;
        }

        /// <summary>
        /// List
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageIndex">1为起始页</param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        public static async Task<List<T>> ToListAsync<T>(
            this IQueryable<T> query,
            int pageIndex = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            if (pageIndex < 1) throw new ArgumentOutOfRangeException(nameof(pageIndex));
            int realIndex = pageIndex - 1;
            //int count = await query.CountAsync(cancellationToken).ConfigureAwait(false);
            var items = await query.Skip(realIndex * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync(cancellationToken)
                                   .ConfigureAwait(false);
            return items;
        }
    }
}
