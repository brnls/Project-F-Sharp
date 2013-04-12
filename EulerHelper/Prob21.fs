module Prob21
open System.Collections.Generic

open System
open EulerHelper

let properDivisors n = 
    [1 .. (n/2)]
    |>List.filter (fun x -> n % x = 0)

let memoize f = 
    let myDic= Dictionary<_,_>()
    fun x -> 
        match myDic.TryGetValue(x) with
        |true, res -> res
        |false, _ ->
            let res = f x
            myDic.Add(x,res)
            res

let sumOfProperDivisors= 
    memoize (fun x-> 
        properDivisors x
        |>List.sum)

let isAmicable n =
    let da = sumOfProperDivisors n
    let db = sumOfProperDivisors da
    da <> n && n = db

[1..10000]
|>List.filter isAmicable
|>List.sum

