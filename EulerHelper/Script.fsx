
#load "loadfs.fsx"
open EulerHelper.Util
open EulerHelper
open System.IO
open System.Numerics
open System
#indent "on"

let bigFact x = 
    let rec inner (num:BigInteger) acc =
        let numBig = bigint 1
        match num with
        |numBig -> acc
        |_ -> inner (num - bigint 1) (acc * num)
    inner x (bigint 1) 

bigFact (bigint 9)


 