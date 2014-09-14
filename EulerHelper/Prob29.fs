module Prob29

open System.Numerics
let answ = 
    seq {for a in 2 .. 100 do for b in 2 .. 100 do yield (a,b)}
    |>Seq.map (fun (a,b) -> BigInteger.Pow(BigInteger a, b))
    |>Seq.distinct
    |>Seq.length