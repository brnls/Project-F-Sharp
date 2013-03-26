module EulerHelper.Prob3

open EulerHelper.Util

let rec div (a:int64) (b:int64) =
    match divRemL a b with 
    |(x, 0L) -> div x b
    |_ -> if( b * b > a ) then 
                   a
              else 
                    div a (b + 2L)
let c = div 600851475143332L 3L 