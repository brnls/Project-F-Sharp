module Prob22

open System.IO

let wordValue (word:string) = 
    word.ToCharArray()
    |>Array.map (fun x-> int x - 64)
    |>Array.sum

let words = 
    File.ReadAllLines(@"E:\GitHub\Project F Sharp\EulerHelper\Prob22.txt").[0].Replace("\"", "").Split(',')
    |>Array.sort
    |>Array.mapi (fun i x-> (i+1) * wordValue x)
    |>Array.sum

    