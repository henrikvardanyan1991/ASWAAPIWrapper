using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ASWAWrapper.Common.Extensions
{
    public static class MemberInfoGetting
    {
        public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
        {
            MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }
    }
}
