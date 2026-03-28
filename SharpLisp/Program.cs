using SharpLisp;

Interpreter.Eval("""
                 (defun = (a b)
                    (eql a b))
                 """);
Interpreter.Eval("""
                 (defun fact (x) 
                    (if (= x 0)
                        1
                        (* x (fact (- x 1)))))
                 """);
Interpreter.Eval("(fact 5)");
Interpreter.Eval("""
                 (labels ((inc (x) 
                            (+ 1 x)))
                    (inc 5))
                 """);
Interpreter.Eval("(inc 1)"); // tohle má zhučet, což funguje
