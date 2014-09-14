module Prob34
open EulerHelper.Array
open EulerHelper.Util
open System.Collections.Generic

let factArr = 
    [|0..9|]
    |>Array.map fact

let factorialSum i = 
    intToArray i
    |>Array.map (fun x -> factArr.[x])
    |>Array.sum

let answ = 
    {10..9999999}
    |>Array.ofSeq
    |>Array.Parallel.partition(fun x -> factorialSum x = x)
    |>(fun (x,y) -> Array.sum x)