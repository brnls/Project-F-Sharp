module Prob30

open EulerHelper.Util

let digitSum power num=
    num.ToString()
    |>Seq.sumBy(fun x-> pown (aToi x) power)

let answ = 
    seq{for i in 2 .. 354294 do yield i}
    |> Seq.filter (fun x-> (digitSum 5 x) = x)
    |> Seq.sum