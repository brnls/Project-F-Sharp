module Prob16
open EulerHelper.Util
open EulerHelper
open System.IO
open System.Numerics

let x = 
    BigInteger.Pow(bigint 2,1000).ToString().ToCharArray()
    |>Array.map aToi
    |>Array.sum

// Problem 15 - Pascals triangle