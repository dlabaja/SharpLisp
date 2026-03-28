using SharpLisp.DataTypes;
using SharpLisp.Factories;

namespace SharpLisp.Parsers;

public static class ExpressionParser
{
    private static string _atomChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-+/*-%!?.><=\"";
    private static string _exprChars = "()";
    
    public static SymbolicExpression ParseExpression(string expr)
    {
        var items = SplitExpression(expr);
        return ParseExpressionRec(items);
    }
    
    private static SymbolicExpression ParseExpressionRec(List<string> items)
    {
        if (items.Count == 0)
        {
            return SymbolicExpressionFactory.Nil;
        }

        var first = items[0];
        items.RemoveAt(0);
        return new SymbolicExpression(new Cons(Parser.Parse(first), ParseExpressionRec(items)));
    }

    private static List<string> SplitExpression(string expr)
    {
        var result = new List<string>();
        var slicedExpr = expr.Substring(1, expr.Length - 2).Trim();
        if (slicedExpr.Length == 0)
        {
            return result;
        }
        
        return IndexesToParts(slicedExpr, GetExpressionPartsIndexes(slicedExpr));
    }

    private static List<string> IndexesToParts(string expr, List<(int start, int end)> list)
    {
        var result = new List<string>();
        foreach (var (start, end) in list)
        {
            var part = expr.Substring(start, end - start + 1).Trim();
            result.Add(part);
        }

        return result;
    } 
    
    private static List<(int start, int end)> GetExpressionPartsIndexes(string expr)
    {
        var indexList = new List<(int start, int end)>();
        int start = 0;
        var atomProcessing = false;
        var expressionProcessing = false;
        var numberOfBracketsToFind = 0;
        for (int i = 0; i < expr.Length; i++)
        {
            if (_atomChars.Contains(expr[i]) && !atomProcessing && !expressionProcessing)
            {
                atomProcessing = true;
                start = i;
                continue;
            }

            if (!_atomChars.Contains(expr[i]) && atomProcessing)
            {
                atomProcessing = false;
                indexList.Add((start, i));
                continue;
            }

            if (_exprChars.Contains(expr[i]) && !atomProcessing && !expressionProcessing)
            {
                expressionProcessing = true;
                numberOfBracketsToFind = 0;
                start = i;
                continue;
            }

            if (expr[i] == '(' && expressionProcessing)
            {
                numberOfBracketsToFind++;
                continue;
            }

            if (expr[i] == ')' && expressionProcessing)
            {
                if (numberOfBracketsToFind == 0)
                {
                    expressionProcessing = false;
                    indexList.Add((start, i));
                }
                numberOfBracketsToFind--;
            }
            
        }

        if (atomProcessing)
        {
            indexList.Add((start, expr.Length - 1));
        }

        if (expressionProcessing)
        {
            indexList.Add((start, expr.Length - 1));
        }

        return indexList;
    }
}
