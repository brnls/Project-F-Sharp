
#load @"FSharpChart.fsx"
#load "Convolve.fsx"
#load "Options.fs"
open MSDN.FSharp.Charting
open System.Windows.Forms.DataVisualization
open Convolve
open System.IO
open Options



/////////////////////////////////////
///////////// Example ///////////////
/////////////////////////////////////


let longCall = {Type = Call; Strike = 540m;}
let shortCall = {Type = Call; Strike = 560m;}

let longCallpos = {Instrument = Option longCall; Type = Long; Holdings = 1m; PurchasePrice = 19m;}
let shortCallpos = {Instrument = Option shortCall; Type = Short; Holdings = 1m; PurchasePrice = 12m;}

let longUnderlying = { Instrument = Physical; Type = Long; Holdings = 10m; PurchasePrice = 20m;}

let positions = [longCallpos; shortCallpos;]

let profit = 
    seq{for i in 500m..600m -> (i, positionProfit longUnderlying i)}
    |>Array.ofSeq

let profitComb = 
    seq{for i in 500m..600m -> (i,positionsSumProfit positions i)}
    |>Array.ofSeq

positionProfit longCallpos 20m

let chart1:ChartData.OneValue = 
    profitComb
    |>Array.ofSeq
    |>FSharpChart.Line
    //|> FSharpChart.WithArea.AxisX(Minimum = 0.0, Maximum = 30.0)
    |>FSharpChart.Create
        

chart1.SetData profitComb 




