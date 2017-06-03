using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MUMScrum.Infrastructure.Core
{
    public static class LinqExtensions
    {
        public static IQueryable<T> AddCondition<T>(this IQueryable<T> queryable, bool isSearch, Expression<Func<T, bool>> filter)
        {
            if (isSearch)
            {
                return queryable.Where(filter);

                #region UnusedCode

                //MemberExpression memberAccess = null;
                //foreach (var property in column.Split('.'))
                //{
                //    memberAccess = MemberExpression.Property
                //       (memberAccess ?? (parameter as Expression), property);
                //}

                //change param value type
                //necessary to getting bool from string
                //ConstantExpression filter = Expression.Constant
                //(
                //    ChangeType(value, memberAccess.Type)
                //);

                //switch operation
                //Expression condition = null;
                //LambdaExpression lambda = null;

                //condition = Expression.Call(memberAccess,
                //            typeof(string).GetMethod("Contains"),
                //            Expression.Constant(value));
                //lambda = Expression.Lambda(filter, parameter);


                //MethodCallExpression result = Expression.Call(
                //       typeof(Queryable), "Where",
                //       new[] { queryable.ElementType },
                //       queryable.Expression,
                //       lambda);

                //return queryable.Provider.CreateQuery<T>(result);


                //ParameterExpression pe = Expression.Parameter(typeof (string), "filter");

                //Expression<Func<T, bool>> __ = filter;

                //Expression predicateBody = Expression.And(__,filter);

                //MethodCallExpression result = Expression.Call(
                //typeof(Queryable),
                //"Where",
                //new[] { queryable.ElementType },
                //queryable.Expression,
                //Expression.Lambda<Func<T, bool>>(predicateBody, pe));

                //return queryable.Provider.CreateQuery<T>(result);

                //return queryable.Where(filter);

                //public static IQueryable<T> Where<T>(this IQueryable<T> query,
                //    string column, object value)
                //{
                //    if (string.IsNullOrEmpty(column))
                //        return query;

                //    ParameterExpression parameter = Expression.Parameter(query.ElementType, "p");

                //    MemberExpression memberAccess = null;
                //    foreach (var property in column.Split('.'))
                //    {
                //        memberAccess = MemberExpression.Property
                //           (memberAccess ?? (parameter as Expression), property);
                //    }

                //    //change param value type
                //    //necessary to getting bool from string
                //    ConstantExpression filter = Expression.Constant
                //    (
                //        ChangeType(value, memberAccess.Type)
                //    );

                //    //switch operation
                //    Expression condition = null;
                //    LambdaExpression lambda = null;

                //    condition = Expression.Call(memberAccess,
                //                typeof(string).GetMethod("Contains"),
                //                Expression.Constant(value));
                //    lambda = Expression.Lambda(condition, parameter);


                //    MethodCallExpression result = Expression.Call(
                //           typeof(Queryable), "Where",
                //           new[] { query.ElementType },
                //           query.Expression,
                //           lambda);

                //    return query.Provider.CreateQuery<T>(result);
                //}

                //public static object ChangeType(Object value, Type conversionType)
                //{
                //    // DateTime format is dd/MM/yyyy in frontend so need to convert into MM/dd/yyyy.
                //    //if (conversionType.Name.Equals("DateTime"))
                //    //{
                //    //    var d = string.Format("{0}", value).Split('/');
                //    //    value = string.Format("{0}/{1}/{2}", d[1], d[0], d[2]);
                //    //}

                //    return conversionType.UnderlyingSystemType.BaseType != null &&
                //           conversionType.UnderlyingSystemType.BaseType.Name.Equals("Enum")
                //        ? Enum.Parse(conversionType.UnderlyingSystemType, value.ToString())
                //        : Convert.ChangeType(value, conversionType);
                //}

                #endregion
            }
            return queryable;
        }
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }
        public static Expression<Func<T, bool>> Make<T>() { return null; }
        public static Expression<Func<T, bool>> Make<T>(this Expression<Func<T, bool>> predicate)
        {
            return predicate;
        }
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression,
                                                       Expression<Func<T, bool>> andExpression)
        {
            //var invokedExpr = Expression.Invoke(andExpression, expression.Parameters.Cast<Expression>());
            //return Expression.Lambda<Func<T, bool>>
            //     (Expression.AndAlso(expression.Body, expression), expression.Parameters);

            if (expression == null) return andExpression;

            return Expression.Lambda<Func<T, bool>>
                  (Expression.And(expression.Body, andExpression), expression.Parameters);
            //var invokedExpression = Expression.Invoke(andExpression, expression.Parameters);
            //return Expression.Lambda<Func<T, bool>>
            //      (Expression.AndAlso(expression.Body, invokedExpression), expression.Parameters);
        }


        private static Expression RebindParameter(Expression expression, ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            if (expression == null)
                return null;

            switch (expression.NodeType)
            {
                case ExpressionType.Parameter:
                    {
                        ParameterExpression parameterExpression = (ParameterExpression)expression;

                        return (parameterExpression.Name == oldParameter.Name ? newParameter : parameterExpression);
                    }
                case ExpressionType.MemberAccess:
                    {
                        MemberExpression memberExpression = (MemberExpression)expression;

                        return memberExpression.Update(RebindParameter(memberExpression.Expression, oldParameter, newParameter));
                    }
                case ExpressionType.AndAlso:
                case ExpressionType.OrElse:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                    {
                        BinaryExpression binaryExpression = (BinaryExpression)expression;

                        return binaryExpression.Update(RebindParameter(binaryExpression.Left, oldParameter, newParameter), binaryExpression.Conversion, RebindParameter(binaryExpression.Right, oldParameter, newParameter));
                    }
                case ExpressionType.Call:
                    {
                        MethodCallExpression methodCallExpression = (MethodCallExpression)expression;

                        return methodCallExpression.Update(RebindParameter(methodCallExpression.Object, oldParameter, newParameter), methodCallExpression.Arguments.Select(arg => RebindParameter(arg, oldParameter, newParameter)));
                    }
                case ExpressionType.Invoke:
                    {
                        InvocationExpression invocationExpression = (InvocationExpression)expression;

                        return invocationExpression.Update(RebindParameter(invocationExpression.Expression, oldParameter, newParameter), invocationExpression.Arguments.Select(arg => RebindParameter(arg, oldParameter, newParameter)));
                    }
                default:
                    {
                        return expression;
                    }
            }
        }
        public static Predicate<T> And<T>(params Predicate<T>[] predicates)
        {
            return delegate (T item)
            {
                foreach (Predicate<T> predicate in predicates)
                {
                    if (!predicate(item))
                    {
                        return false;
                    }
                }
                return true;
            };
        }
    }
}
