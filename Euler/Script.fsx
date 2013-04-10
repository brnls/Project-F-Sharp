#load "Probability.fsx"
//#load @"FSharpChart.fsx"
#load "Convolve.fsx"
#load "Images.fsx"
#load "Filters.fsx"
open System.IO
open System
//open MSDN.FSharp.Charting
//open System.Windows.Forms.DataVisualization
//open Convolve
open System.IO
open System.Drawing
open System.Drawing.Imaging
open System.Runtime.InteropServices
open Microsoft.FSharp.NativeInterop
open Images
open Filters.ImgFilters
#time


let rand = new Random()
let filepath = @"C:\Users\Brian\Desktop\" + "dog.JPG"
let getImage func filepath=
    use newImage =
        Util.pictureToBmp(filepath)
        |>Images.Util.filter func
        |>Util.bmpToPicture
        
    newImage.Save(@"C:\Users\Brian\Desktop\" + "test2.jpg")

getImage (Filters.ImgFilters.blur 50) filepath
