module EulerHelper.Prob3

open EulerHelper.Util
open EulerHelper.Primes

let num = 600851475143L
let root = float >> sqrt >> int <| num

let answ = 
    primesBelow (int root)
    |> Array.rev 
    |> Seq.filter (fun x -> num % (int64 x) = 0L)
    |> Seq.head