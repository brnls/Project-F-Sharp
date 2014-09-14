module Prob27

open System.Collections.Generic
open EulerHelper.Primes

let primes = new HashSet<int>((primesBelow 1000000))

let (|Prime|_|) x = if primes.Contains(x) then Some (x) else None

let value a b n =
    n * n + a * n + b

let consecutivePrimes a b = 
    Seq.unfold(fun i ->
        match value a b i with
        |Prime result -> Some(result, i + 1)
        |_ -> None) 0

let mutable maxVal = (0,0)
for i in -1000 .. 1000 do
    for j in -1000 .. 1000 do
        let seqCount = consecutivePrimes i j |>Seq.length
        if seqCount > fst maxVal then
            maxVal <- (seqCount, i * j)