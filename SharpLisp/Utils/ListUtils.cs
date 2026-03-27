namespace SharpLisp.Utils;

public static class ListUtils
{
    public static List<T> Cdr<T>(this List<T> list)
    {
        return list.Skip(1).ToList();
    }
    
    public static T Foldr<T>(List<T> args, Func<T, T, T> func, T init)
    {
        return args.Count == 0 ? init : func(args[0], Foldr(args.Cdr(), func, init));
    }
}
