namespace EulerHelper
open System.Collections.Generic
open System
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
        
        let rec fact x = 
            match x with
            |0 -> 1
            |x -> x * fact (x - 1)

        let divRemL (dividend:int64) (divisor:int64) = 
            let quot = dividend / divisor
            let remainder = dividend - (quot * divisor)
            (quot, remainder)
        
        let rec gcd a b = 
            if b = 0L then a else gcd b (a % b)

        let rec gcd32 a b = 
            if b = 0 then a else gcd32 b (a % b)

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
            
        let rec isPalindrome (arr : 'a[])=
            if( arr.Length = 0 || arr.Length = 1) then true
            else
                if arr.[0] = arr.[arr.Length - 1] then
                    isPalindrome (arr.[1..arr.Length - 2])
                else false
            
        let inline aToi (a:char) =
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

        ///Select random element from given sequence
        let selectRandom (sequence:seq<'a>) = 
            let rand = Random()
            let randomElement = int (floor (rand.NextDouble()* float (Seq.length sequence)))
            sequence
            |>Seq.nth randomElement      


////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////  Array ///////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////
    module Array = 

        let swap = Permutations.swap
        
        let intToArray i = 
            i.ToString()
            |>Seq.map (fun x -> Util.aToi x)
            |>Array.ofSeq          
    
        let arrayToInt (arr:int[]) = 
            let mutable sum = 0
            for i in 0 .. (arr.Length - 1) do
                sum <- sum + (arr.[i] * (pown 10 (arr.Length - 1 - i)))
            sum

        let permute arr = 
            Permutations.permute arr

        let rotateForward (arr:'a[]) =
            let mutable temp = arr.[arr.Length - 1]
            for i in (arr.Length - 1).. -1 .. 1 do
                arr.[i] <- arr.[i - 1]
            arr.[0] <- temp

        let rotateBackward (arr:'a[]) =
            let mutable temp = arr.[0]
            for i in 0..(arr.Length - 2) do
                arr.[i] <- arr.[i + 1]
            arr.[arr.Length - 1] <- temp

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

//        let primesBelow x = 
//            let NumList = [|0L..x|]
//            NumList.[1] <- 0L
//            for i in 2 .. (int (sqrt(float x))) do
//                if NumList.[i] <> 0L then
//                    for j in (i+i) .. i .. x do
//                        NumList.[j] <- 0L
//            NumList |> Array.filter (fun x -> x <> 0L)
