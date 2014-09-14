module Prob26

open EulerHelper.Util
open EulerHelper.Permutations
open EulerHelper.Primes


let (|Identity|_|) x a =
    if a % x = 1 then Some ()
    else None

let countCycle baseNum=
    let rec countCycle' x baseNum count =
        match x with
        |Identity baseNum-> count
        |0 -> count
        |_ -> countCycle' ((x*10) % baseNum) baseNum (count + 1)
    countCycle' 10 baseNum 1

let mutable max = 0
for i in primesBelow 1000 do
     let count = countCycle i
     if count > max then
        max <- count

