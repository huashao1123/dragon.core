using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
