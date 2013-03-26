module Prob10

open EulerHelper.Primes

primesBelow 2000000
|>Seq.sumBy(fun x-> int64 x)
