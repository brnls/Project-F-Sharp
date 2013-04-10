<<<<<<< HEAD
﻿let isAbundant n = 
    [1 .. (n/2)]
    |>List.filter (fun x -> n % x = 0)
    |>List.sum
    |>(fun x-> x > n)

let getAbundants max = 
    let abundants:int[] = Array.zeroCreate max
    for i in 0..abundants.Length - 1 do
        if abundants.[i] = 0 then
            if isAbundant i then 
                for j in i .. i .. abundants.Length - 1 do
                    abundants.[j] <- j
    abundants  
            
getAbundants 28123
|>Array.filter(fun x-> x <>0)
|>Array.sum

#time
[1..28123]
|>List.filter (fun x-> isAbundant x |> not)
|>List.sum

let isAbundant x =

let properDivisorsOf n =
    [1 .. (n/2)]
    |> List.filter (fun x -> n % x = 0)

let isAbundant n =
    (properDivisorsOf n |> List.sum) > n

let abundantNumbers =
    [1 .. 28123]
    |> List.filter isAbundant

let sumsOf2AbundantNumbers =
    [ for i in abundantNumbers do
         for j in (abundantNumbers 
                   |> Seq.skipWhile (fun x -> x < i)
                   |> Seq.takeWhile (fun x -> x + i <= 28123)) do
             yield i + j ]

let euler023 = 
    let sums = new Set<_>(sumsOf2AbundantNumbers)
    [1 .. 28123]
    |> List.filter (fun x -> sums.Contains(x) |> not)
    |> List.sum
=======
﻿
#load "loadfs.fsx"
open EulerHelper.Util
open EulerHelper
open System.IO
open System.Numerics
open System
#indent "on"

let bigFact x = 
    let rec inner (num:BigInteger) acc =
        let numBig = bigint 1
        match num with
        |numBig -> acc
        |_ -> inner (num - bigint 1) (acc * num)
    inner x (bigint 1) 

bigFact (bigint 9)


 
>>>>>>> d93d0e1c2f9c984812c6735ab3032b330d2c43dc
