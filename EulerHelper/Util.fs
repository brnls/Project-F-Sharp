﻿namespace Euler

    module Util = 
    //Class to keep track of game score
        type gameScore() =
            let mutable A = 0
            let mutable B = 0
            member this.AScores x =
                A <- A + x
                printfn "%d" A
            member this.BScores x =
                B <- B + x
                printfn "%d" B

            member this.Totals =
                printfn "A - %d  B - %d" A B
    
            member this.reset =
                A <- 0
                B <- 0

        let divRemL (dividend:int64) (divisor:int64) = 
            let quot = dividend / divisor
            let remainder = dividend - (quot * divisor)
            (quot, remainder)
            

    module Fibonacci   =     
        //Defining recursive function
        let getAllFib =
            let rec getFibInf x = 
                let rec fib n =
                    if n= 0 then 1
                    elif n = 1 then 1 
                    else fib (n-1) + fib (n-2)
                seq {yield fib x
                     yield! getFibInf (x + 1)}
            getFibInf 0

        let getAllFibUnfold =
            (0,1)
            |> Seq.unfold(fun (x,y) -> if x + y <= 4000000 then Some(x+y, (y,x+y)) else None)