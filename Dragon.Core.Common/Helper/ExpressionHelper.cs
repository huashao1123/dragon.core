using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Common.Helper
{
    public class ExpressionHelper
    {
        public static Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expression1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expression1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expression2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expression2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
        }

        public static Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> WhereLambda<T, T1>(T1 dto)
        {
            var d = Expression.Parameter(typeof(SetPropertyCalls<T>), "d");
            var f = Expression.Parameter(typeof(T), "e");
            var methods = typeof(SetPropertyCalls<T>).GetMethods().FirstOrDefault(d => d.Name == "SetProperty" && d.GetParameters()[1].ParameterType.Name == "TProperty");
            var dtoProps = typeof(T1).GetProperties();
            var props = typeof(T).GetProperties().Select(d => new {d.Name,d.PropertyType}).ToList();
            //var ds = d;
            Expression expression = d;
            foreach (var dtoProp in dtoProps)
            {
                var value = dtoProp.GetValue(dto);
                string name = dtoProp.Name;
                var prop= props.Where(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (prop == null || value == null)
                {
                    continue;
                }
                Type t = prop.PropertyType;
                var nameProp = Expression.Property(f, prop.Name); //e.Name
                var nameLambda = Expression.Lambda(nameProp, f);// e => e.Name
                var constant = Expression.Constant(value,t);//value
                var method = methods.MakeGenericMethod(t);
                expression = Expression.Call(expression, method, nameLambda, constant);
            }
            return Expression.Lambda<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>>(expression, d);
        }


    }
    class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression Visit(Expression? node)
        {
            if (node == _oldValue)
            {
                return _newValue;
            }

            return base.Visit(node)!;
        }
    }
}
