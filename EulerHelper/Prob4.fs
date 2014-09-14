module Prob4

open EulerHelper.Util
let answ = 
    seq{for i in [1..999] do
        for j in [1..999] do
            yield i*j}
    |>Seq.filter(fun x -> isPalindrome ((string x).ToCharArray()))
    |>Seq.max


