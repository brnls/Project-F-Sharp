module Prob20

open System.Numerics
open EulerHelper

let rec bigFact num = 
    let bigOne = bigint 1
    if num = bigint 1 then num 
    else num * bigFact (num - 1I)

let answ = 
    (bigFact 100I).ToString().ToCharArray()
    |>Array.map(fun x -> EulerHelper.Util.aToi x)
    |>Array.sum
