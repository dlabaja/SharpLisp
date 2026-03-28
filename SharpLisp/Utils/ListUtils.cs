using SharpLisp.DataTypes;
using SharpLisp.Factories;

namespace SharpLisp.Utils;

public static class ListUtils
{
    public static T Car<T>(this List<T> list)
    {
        return list[0];
    }
    
    public static List<T> Cdr<T>(this List<T> list)
    {
        return list.Skip(1).ToList();
    }
    
    public static T Foldr<T>(List<T> list, Func<T, T, T> func, T init)
    {
        if (list.Count == 0)
        {
            return init;
        }

        return func(list[0], Foldr(list.Cdr(), func, init));
    }

    public static List<TRes> Mapcar<T, TRes>(List<T> list, Func<T, TRes> func)
    {
        return list.Select(func).ToList();
    }

    public static List<SymbolicExpression> ConsToList(SymbolicExpression expr)
    {
        var res = new List<SymbolicExpression>();
        var current = expr;
        while (current.IsCons())
        {
            res.Add(current.Cons.Car);
            current = current.Cons.Cdr;
        }

        return res;
    }

    public static SymbolicExpression ListToCons(List<SymbolicExpression> list)
    {
        if (list.Count == 0)
        {
            return SymbolicExpressionFactory.Nil;
        }

        return SymbolicExpressionFactory.Cons(list.Car(), ListToCons(list.Cdr()));
    }
}
