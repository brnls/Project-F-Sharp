module Prob35
open EulerHelper
open System.Collections.Generic

let primes = Primes.primesBelow 1000000
let hashSet = new HashSet<int>(primes)
let isPrime = hashSet.Contains

let allRotationsPrime (arr:int[]) =
    let rec inner count = 
        match count with 
        |0 -> true
        |n -> 
            EulerHelper.Array.rotateForward arr
            let num = EulerHelper.Array.arrayToInt arr
            if isPrime num then inner (count - 1)
            else false
    inner (arr.Length - 1)

let answer =
    primes
    |>Seq.map EulerHelper.Array.intToArray
    |>Seq.filter allRotationsPrime
    |>Seq.length