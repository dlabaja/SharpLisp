(defmacro and2 (a b)
    (list (quote if) a (list (quote if) b b nil) nil))

(defun = (a b)
    (eql a b))

(defun fact (x) 
    (if (= x 0)
        1
        (* x (fact (- x 1)))))