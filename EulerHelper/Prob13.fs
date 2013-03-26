module Prob13

open EulerHelper.Util
open System.IO

let answ = 
    File.ReadLines(@"E:\GitHub\Project F Sharp\EulerHelper\Prob13.txt")
    |>Seq.map (fun x-> int64(x.Substring(0, 11)))
    |>Seq.sum