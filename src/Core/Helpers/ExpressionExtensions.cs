using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class ExpressionExtensions
    {
        public static string ToCacheKey<T>(this Expression<Func<T, bool>> predicate)
        {
            var visitor = new PredicateVisitor();
            visitor.Visit(predicate);
            return visitor.GetKey();
        }

        private class PredicateVisitor : ExpressionVisitor
        {
            private readonly StringBuilder _sb = new StringBuilder();

            public string GetKey() => _sb.ToString();

            protected override Expression VisitBinary(BinaryExpression node)
            {
                _sb.Append("(");
                Visit(node.Left);

                _sb.Append(node.NodeType switch
                {
                    ExpressionType.Equal => "==",
                    ExpressionType.NotEqual => "!=",
                    ExpressionType.GreaterThan => ">",
                    ExpressionType.GreaterThanOrEqual => ">=",
                    ExpressionType.LessThan => "<",
                    ExpressionType.LessThanOrEqual => "<=",
                    ExpressionType.AndAlso => "&&",
                    ExpressionType.OrElse => "||",
                    _ => node.NodeType.ToString()
                });

                Visit(node.Right);
                _sb.Append(")");
                return node;
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                _sb.Append(node.Member.Name);
                return node;
            }

            protected override Expression VisitConstant(ConstantExpression node)
            {
                _sb.Append(node.Value?.ToString() ?? "null");
                return node;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                _sb.Append(node.Type.Name); 
                return node;
            }
        }
        public static string ToHash<T>(this Expression<Func<T, bool>> predicate)
        {
            string expressionString = predicate.ToCacheKey();

            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(expressionString));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

    }
}
