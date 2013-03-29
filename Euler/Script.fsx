
#load @"FSharpChart.fsx"
#load "Convolve.fsx"
open MSDN.FSharp.Charting
open System.Windows.Forms.DataVisualization
open Convolve

let f x y =
    let value = x**2.0 + y**2.0 
    if value = 0.0 then 0.0
    else
        1.0 * abs(1.0/(value))

let heaviside x=
    if x <= 0.0 then 1.0
    else -1.0

for i in 9.0.. 0.1 ..10.0  do
    let signal = 
        Array2D.init 128 128 (fun x y -> (64 - x), (64 - y))
        |>Array2D.map (fun (x,y) -> f (double x) (double y))

    let noisySignal =
        signal
        |>Convolve.addNoise2D (i)

    let dataLine = Array.init 128 (fun x -> noisySignal.[x,64])

    let filter = 
        Array2D.init 20 20 (fun x y-> (10 - x), (10 - y))
        |>Array2D.map (fun (x,y) -> heaviside (double x))

    let data = Convolve.convolve2D noisySignal filter 64
    
    let chart1:ChartData.OneValue = 
        data
        |>FSharpChart.Line
        //|> FSharpChart.WithArea.AxisX(Minimum = -10.0, Maximum = 10.0)
        |>FSharpChart.Create
    
    ()    

[for x in -10.0 .. 0.1 .. 10.0 -> x, f x]
|>chart1.SetData


