module EulerHelper.Prob2

let answ1 = 
    EulerHelper.Fibonacci.getAllFib
    |> Seq.filter(fun x -> x % 2 = 0)
    |> Seq.takeWhile(fun x -> x < 4000000)
    |> Seq.sum

//Using unfold
let answ2 = 
    EulerHelper.Fibonacci.getAllFibUnfold
    |>Seq.filter(fun x-> x % 2 = 0)
    |>Seq.sum