
#load @"FSharpChart.fsx"
#load "Convolve.fsx"
open MSDN.FSharp.Charting
open System.Windows.Forms.DataVisualization
open Convolve

let f x = 
    if x = 0.0 then 10.0
    else
        abs(1.0/x)

let heaviside x =
    if x >= 0.0 then 1.0
    else -1.0

let signal = 
    [|-10.0.. 0.1 .. 10.0|]
    |>Array.map (fun x-> f x)

let filter = 
    [|-1.0 .. 0.1 .. 1.0|]
    |>Array.map (fun x -> heaviside x)
    

let data = Convolve.convolve1D signal filter

let chart1:ChartData.OneValue = 
    data
    |>FSharpChart.Line
    //|> FSharpChart.WithArea.AxisX(Minimum = -10.0, Maximum = 10.0)
    |>FSharpChart.Create

[for x in -10.0 .. 0.1 .. 10.0 -> x, f x]
|>chart1.SetData




