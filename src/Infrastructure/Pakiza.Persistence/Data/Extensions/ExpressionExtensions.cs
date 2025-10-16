

namespace Pakiza.Persistence.Data.Extensions;


public static class ExpressionExtensions
{
    public static string ToSql<T>(this Expression<Func<T, bool>> expression)
    {
        if (expression.Body is BinaryExpression binaryExpression)
        {
            return ParseBinaryExpression(binaryExpression);
        }
        throw new NotSupportedException("Only binary expressions are supported.");
    }

    private static string ParseBinaryExpression(BinaryExpression expression)
    {
        var left = ParseExpression(expression.Left);
        var right = ParseExpression(expression.Right);
        var @operator = GetOperator(expression.NodeType);

        return $"{left} {@operator} {right}";
    }

    private static string ParseExpression(Expression expression)
    {
        switch (expression)
        {
            case MemberExpression member:
                return member.Member.Name;
            case ConstantExpression constant:
                return constant.Value.ToString();
            default:
                throw new NotSupportedException("Unsupported expression type.");
        }
    }

    private static string GetOperator(ExpressionType expressionType)
    {
        return expressionType switch
        {
            ExpressionType.Equal => "=",
            ExpressionType.AndAlso => "AND",
            ExpressionType.OrElse => "OR",
            _ => throw new NotSupportedException($"Unsupported operator {expressionType}")
        };
    }
}