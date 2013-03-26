
#load "loadfs.fsx"
open EulerHelper.Util
open EulerHelper
open System.IO

let triangleNumbers = 
    Seq.initInfinite ((+) 2)
    |> Seq.scan (+) 1

let total = 500 
let sq = {1..10}
Seq.scan (fun tot change -> tot - change) total sq
|>Seq .iter (printfn "%i")
