module Prob31

let monies = [100;50;20;10;5;2;1;200]

let combinations coins total =
    let rec inner coinsInner innerTotal count =
        match coinsInner with
        |[x] -> if innerTotal > 0 then inner [x] (innerTotal - x) count
                else if innerTotal = 0 then count + 1
                else count
        |x::s -> seq{for i in 0..(innerTotal/x) -> i}
                 |> Seq.sumBy (fun y-> inner s (innerTotal - y * x) 0)
        |_ -> 0
    inner coins total 0

combinations monies 200


let combinations2 coins total =
    let rec inner coinsInner innerTotal count =
        match coinsInner with
        |[x] -> if innerTotal > 0 then inner [x] (innerTotal - x) count
                else if innerTotal = 0 then count + 1
                else count
        |x::s -> let mutable c = 0
                 for i in 0 .. innerTotal/x do
                    c <- c + inner s (innerTotal - i * x) 0
                 c + count
        |_ -> 0
    inner coins total 0

combinations2 monies 200