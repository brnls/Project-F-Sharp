
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

let profitComb = [for i in 500m..600m -> (i, positionProfit longCall i)]

let chart1:ChartData.OneValue = 
    profitComb
    |>Array.ofSeq
    |>FSharpChart.Line
    |> FSharpChart.WithArea.AxisX(Minimum = 400.0, Maximum = 600.0)
    |> FSharpChart.WithArea.Name("Profit/Loss on expiry")
    |>FSharpChart.Create
        

chart1.SetData profitComb 



//////Buy a call
let longCall= {Instrument = Option {Type = Call; Strike = 550m} ; Type = Long; Holdings = 10m; PurchasePrice = 5m}
chart1.SetData [for i in 500m..600m -> (i, positionProfit longCall i)]


//////Buy a put
let longPut= {longCall with Instrument = Option {Type = Put; Strike = 550m;}} 
chart1.SetData [for i in 500m..600m -> (i, positionProfit longPut i)]


/////Buy physical Shares
let physical= {Instrument = Physical; Type = Long; Holdings = 10m; PurchasePrice = 550m;}
chart1.SetData [for i in 500m..600m -> (i, positionProfit physical i)]

////Long on underlying long on put
chart1.SetData [for i in 500m..600m -> (i, positionsSumProfit [physical; longPut] i)]

///Collar - Long on underlying, short call higher strike, long put lower strike. Sale of short call to cover price of put
let shortCallHigherStrike = {Instrument = Option {Type = Call; Strike = 580m;}; Type = Short; Holdings = 10m; PurchasePrice = 7m;}
let longPutLowerStrike= {longPut with Instrument = Option {Type = Put; Strike = 520m;}; PurchasePrice = 7m;}
chart1.SetData [for i in 500m..600m -> (i, positionsSumProfit [shortCallHigherStrike; longPutLowerStrike; physical] i)]

///Long Straddle - Long call and Long put
let LSshortPut = {Instrument = Option {Type = Call; Strike = 520m} ; Type = Long; Holdings = 1m; PurchasePrice = 28m}
let LSshortCall = {Instrument = Option {Type = Put; Strike = 520m} ; Type = Long; Holdings = 1m; PurchasePrice = 29m}
chart1.SetData [for i in 400m..600m -> (i, positionsSumProfit [LSshortPut; LSshortCall] i)]

///Short straddle - Short call and short put same strike
let SSshortPut = {Instrument = Option {Type = Call; Strike = 520m} ; Type = Short; Holdings = 1m; PurchasePrice = 28m}
let SSshortCall = {Instrument = Option {Type = Put; Strike = 520m} ; Type = Short; Holdings = 1m; PurchasePrice = 29m}
chart1.SetData [for i in 400m..600m -> (i, positionsSumProfit [SSshortPut; SSshortCall] i)]

//Iron Butterfly 500/520/540 --Combination of short straddle above with purchase of protective put/call against unlimited loss
let protPut = {Instrument = Option {Type = Put; Strike = 500m}; Type = Long; Holdings = 1m; PurchasePrice = 20m }
let protCall = {Instrument = Option {Type = Call; Strike = 540m}; Type = Long; Holdings = 1m; PurchasePrice = 19m }
chart1.SetData [for i in 400m..600m -> (i, positionsSumProfit [SSshortPut; SSshortCall; protPut; protCall] i)]


///Bull Call Spread (Long Call Spread - long call above currently trading price with short call higher than first
let longCallLowerStrike= {Instrument = Option {Type = Call; Strike = 560m;}; Type = Long; Holdings = 10m; PurchasePrice = 14m;}
chart1.SetData [for i in 500m..600m -> (i, positionsSumProfit [shortCallHigherStrike; longCallLowerStrike] i)]

//Bear Put spread 
let longPutHigherStrike = {Instrument = Option {Type = Put; Strike = 500m;}; Type = Long; Holdings = 10m; PurchasePrice = 20m;}
let shortPutLowerStrike = {Instrument = Option {Type = Put; Strike = 460m;}; Type = Short; Holdings = 10m; PurchasePrice = 10m;}
chart1.SetData [for i in 400m..600m -> (i, positionsSumProfit [longPutHigherStrike; shortPutLowerStrike] i)]

//Bear Call Spread
let BCShortLeg = {Instrument = Option {Type = Call; Strike = 540m;}; Type = Short; Holdings = 1m; PurchasePrice = 19m;}
let BCLongLeg = {Instrument = Option {Type = Call; Strike = 560m;}; Type = Long; Holdings = 1m; PurchasePrice = 12m;}
chart1.SetData [for i in 400m..600m -> (i, positionsSumProfit [BCLongLeg; BCShortLeg;] i)]

//Bull Put Spread
let BPShortLeg = opt Put 500m Short 1m 20m
let BPLongLeg = opt Put 460m Long 1m 10m
chart1.SetData [for i in 400m..600m -> (i, positionsSumProfit [BPShortLeg; BPLongLeg;] i)]


let LC = opt Call 580m Long 1m 7m
let LP = opt Put 460m Long 1m 10m
chart1.SetData [for i in 400m..600m -> (i, positionsSumProfit [LC;LP;] i)]