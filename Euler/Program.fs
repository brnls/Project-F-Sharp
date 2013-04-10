﻿open System
//open MSDN.FSharp.Charting
//open System.Windows.Forms.DataVisualization
//open Convolve
open System.IO
open System.Drawing
open System.Drawing.Imaging
open System.Runtime.InteropServices
open Microsoft.FSharp.NativeInterop
open Images

//let rand = new Random()
//let filepath = @"C:\Users\Brian\Desktop\" + "dog.JPG"
//let getImage func filepath=
//    use newImage =
//        Util.pictureToBmp(filepath)
//        //|>Util.filter (Filters.strongestColor 0.7)
//        |>Util.filter func
//        |>Util.bmpToPicture
//        
//    newImage.Save(@"C:\Users\Brian\Desktop\" + "test2.jpg")
//
//getImage (Filters.smear) filepath

let setAbundant n (arr:int[]) = 
    let mutable sum = 0
    for i in 1..n/2 do
        if n % i = 0 then
            sum <- sum + i
    if sum > n then arr.[n] <- n

let getAbundantArr max = 
    let abundants:int[] = Array.zeroCreate max
    for i in 0..abundants.Length - 1 do
        setAbundant i abundants
    abundants          

let abundants = getAbundantArr 28124   
let abundantSums:int[] = Array.zeroCreate 28124       
 
for i in 0..28123 do
    if abundants.[i] <> 0 then
        for j in i..28123 do
            if abundants.[j] <> 0 then
                let index = i + j
                if index < 28124 then
                    abundantSums.[index] <- index

let mutable total = 0
for i in 0..28123 do
    if abundantSums.[i] = 0 then
        total <- total + i
       
total
//getAbundantArr 28124
//|>Array.
//
