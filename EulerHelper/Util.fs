namespace EulerHelper
open System.Collections.Generic
////////////////////////////////////////////////////////////////////////////////////
////////////////////////   General Utility functions   /////////////////////////////
////////////////////////////////////////////////////////////////////////////////////
    module Util = 

        let memoize f = 
            let myDic= Dictionary<_,_>()
            fun x -> 
                match myDic.TryGetValue(x) with
                |true, res -> res
                |false, _ ->
                    let res = f x
                    myDic.Add(x,res)
                    res

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
        
        let rec gcd a b = 
            if b = 0L then a else gcd b (a % b)

        let lcm a b:int64 = a * b / gcd a b

        let lcmSet sequence =
            sequence
            |>Seq.fold lcm 1L

        let myFold sequence func acc =
            let rec inner innerSeq acc =
                match innerSeq with
                |x::s -> inner s (func x acc)
                |[] -> acc
            inner sequence acc
            
        let rec isPalindrome (str:string) =
            if( str.Length = 0 || str.Length = 1) then true
            else
                match (str.[0] = str.[str.Length - 1]) with
                |true -> isPalindrome (str.Substring(1,(str.Length - 2)))
                |false -> false      


        let aToi (a:char) =
            (int)a - 48

        let triangleNumbers =  
            Seq.initInfinite((+) 2)
            |>Seq.scan (+) 1

        let divisors n =
            //Check up to square root and then multiply by two
            let maxDivisor = int(sqrt((double n)))
            let rec loop num div acc = 
                match num % div with
                | 0 when div <= maxDivisor -> loop num (div + 1) (acc + 2)
                | _ when div < maxDivisor -> loop num (div + 1) acc
                | _ when div > maxDivisor -> acc
                |_ -> acc
    
            //Subtract one from total divisors if number is square because square was counted twice in the above
            if floor(sqrt(double n)) = sqrt(double n) then
                loop n 1 -1
            else
                loop n 1 0

      

////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////  Fibonacci Generation /////////////////////////////
////////////////////////////////////////////////////////////////////////////////////
    module Fibonacci   =     

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


////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////  Prime List Generation  //////////////////////////
////////////////////////////////////////////////////////////////////////////////////
    module Primes = 
        let isPrime n =
            {2..int(sqrt(float(n)))} |> Seq.exists(fun x -> n % x = 0) |> not
        //IEnumerable sequence of all primes. Slower to generate
        let PrimeList = 
            let rec nextPrime n = 
                if isPrime (n + 1) then
                    (n + 1)
                else
                    nextPrime (n + 1)
            let rec primeInner a = 
                seq{yield a
                    yield! primeInner(nextPrime a)}
            primeInner 2
        //Array of fixed size x, faster to generate
        let primesBelow x = 
            let NumList = [|0..x|]
            NumList.[1] <- 0
            for i in 2 .. (int (sqrt(float x))) do
                if NumList.[i] <> 0 then
                    for j in (i+i) .. i .. x do
                        NumList.[j] <- 0
            NumList |> Array.filter (fun x -> x <> 0)

