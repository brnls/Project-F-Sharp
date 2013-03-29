module Prob18

open System.IO

let data = 
    File.ReadAllLines @"E:\GitHub\Project F Sharp\EulerHelper\Prob18.txt"
    |>Array.map (fun x -> x.Split(' ') |> Array.map int)
                

for i in 1..data.Length - 1 do
    for j in 0 .. data.[i].Length - 1 do
        if j = 0 then data.[i].[j] <- data.[i].[j] + data.[i-1].[j]
        else if j = (data.[i].Length - 1) then data.[i].[j] <- data.[i].[j] + data.[i-1].[data.[i-1].Length - 1]
        else data.[i].[j] <- data.[i].[j] + max data.[i-1].[j-1] data.[i-1].[j]

data.[data.Length - 1]
|>Array.max


