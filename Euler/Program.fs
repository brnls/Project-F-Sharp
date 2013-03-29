module Euler

open MSDN.FSharp.Charting
open System.Windows.Forms.DataVisualization

let f x y = 
    if x + y = 0.0 then 1.0
    else
        abs(1.0/(x + y))

let heaviside x=
    if x >= 0.0 then 1.0
    else -1.0


let signal = 
    Array2D.init 128 128 (fun x y -> (64 - x), (64 - y))
    |>Array2D.map (fun (x,y) -> f (double x) (double y))

let filter = 
    Array2D.init 20 20 (fun x y -> (10 - x), (10-y))
    |>Array2D.map (fun (x,y) -> heaviside (double x))

let convolve (signal:float[,]) (filter:float[,]) =
    let conv:(float)[] = Array.zeroCreate (signal.GetUpperBound(0) + filter.GetUpperBound(0))
    let windowMiddle = 64
    let filterLength = filter.GetUpperBound 1 + 1
    for i in (filterLength/2)..((signal.GetUpperBound 0) - filterLength / 2) do
        let mutable total = 0.0
        let mutable count = 0.0
        for j in 0..(filterLength - 1) do
            for k in 0..(filterLength - 1) do
                let signalVal = signal.[i - filterLength/2 + k, windowMiddle - filterLength/2 + j]
                if signalVal <> 0.0 then
                    total <- total + signalVal * filter.[k,j] 
                    count <- count + 1.0
        conv.[i] <- total/(double count)
    conv

let data = convolve signal filter  