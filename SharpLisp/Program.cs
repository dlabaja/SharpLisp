using SharpLisp;

Interpreter.Eval("""
                 (defmacro and2 (a b)
                    (list (quote if) a (list (quote if) b b nil) nil))
                 """);
Interpreter.Eval("(and2 t nil)");
