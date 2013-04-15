module Prob24

let swap i j (num:int[]) = 
    let mutable temp = num.[i]
    num.[i] <- num.[j]
    num.[j] <- temp

let getPivot (num:int[]) =
    let mutable pivot = num.Length - 2
    while pivot <> -1 && num.[pivot] > num.[pivot + 1] do
        pivot <- pivot - 1
    pivot

let getMinimum pivot (num:int[]) =
    let mutable leastVal = pivot + 1
    for i in (pivot + 2)..(num.Length - 1) do
        if num.[i] < num.[leastVal] && num.[i] >= num.[pivot] then
            leastVal <- i
    leastVal

let permuteSubset pivot (num:int[]) =     
        let pivotChunk = num.[pivot + 1..] |>Array.sort
        for i in pivot + 1 .. num.Length - 1 do 
            num.[i] <- pivotChunk.[i - (pivot + 1)]

let myArr = [|1;2;3;4;5;|]

let permute num =
    let pivot = getPivot num
    if pivot <> -1 then
        let minimumPosition = getMinimum pivot num
        swap pivot minimumPosition num
        permuteSubset pivot num

let digits = [|0;1;2;3;4;5;6;7;8;9;|]
for i in 1..999999 do
    permute digits
digits


