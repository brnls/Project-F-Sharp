module Prob6


open EulerHelper.Util

let a = [1..100]|>List.sumBy(fun x-> x * x)
let b = [1..100]|>List.sum |> fun x-> x * x
let answ = b - a