module Prob12
open EulerHelper.Util

triangleNumbers
|>Seq.find (fun x-> divisors x > 500)