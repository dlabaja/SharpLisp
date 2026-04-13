(defmacro and2 (a b)
    (list (quote if) a (list (quote if) b b nil) nil))

(defmacro let1 (params body)
    (list (quote funcall) 
          (list (quote lambda) (list (car (car params))) body)
          (car (cdr (car params)))))

(defun = (a b)
    (eql a b))

(defun fact (x) 
    (if (= x 0)
        1
        (* x (fact (- x 1)))))

(defun test-mutation ()
  (progn
    (let1 ((a 0)) 
        (progn 
          (setq a 1)
          (print a)))
    (let1 ((a (cons 1 2))) 
        (progn 
          (set-car a 10)
          (print a)))
    (let1 ((a (cons 1 2))) 
        (progn 
          (set-cdr a (list 3 4 5))
          (print a)))))