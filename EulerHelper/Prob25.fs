module Prob25

let bigFib = 
    Seq.unfold (fun (x,y) -> Some (x + y, (y, (x + y)))) (0I,1I)

bigFib
|>Seq.findIndex(fun x-> x.ToString().Length = 1000)
|>(fun x-> x + 2) // Sequence is 0 indexed and starts on second member of fib sequence.