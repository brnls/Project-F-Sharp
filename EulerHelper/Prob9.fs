﻿module Prob9

let sqr x = x * x

let answ = 
    for i in 1..1000 do
        for j in (i+1)..1000 do
                if sqr i + sqr j = sqr (1000-i-j) then
                    printfn "%i %i" i j 

let answ2 =
    seq {for i in 1..1000 do for j in 1..1000 do yield (i, j, 1000-i-j)}
    |>Seq.filter(fun (x,y,z) -> sqr x + sqr y = sqr z)
    |>Seq.nth 0

// F# does not support breaks, continue from loops. In order to stop when the correct answer was found, use tail recursion.                    
let rec pythag a b = 
    match a,b with
    |(a,b) when sqr a + sqr b <> sqr (1000-a-b) && b < 1000 -> pythag a (b+1)
    |(a,b) when sqr a + sqr b <> sqr (1000 - a - b) && b >= 1000 ->pythag (a+1) (a+2)
    |_ -> (a,b)

let answTailRecursive = 
    pythag 1 2
    |>(fun (x,y) ->  x * y * (1000 - x - y))
