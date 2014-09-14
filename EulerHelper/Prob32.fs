module Prob32
open EulerHelper.Util
open System.Collections.Generic

let arrToNum = EulerHelper.Array.arrayToInt

let getPandigitalProduct (arr:int[]) = 
    let firstNum = arr.[0..1] |>arrToNum
    let secondNum = arr.[2..4] |>arrToNum
    let product = arr.[5..] |>arrToNum

    let firstNum' = arr.[0..0] |> arrToNum
    let secondNum' = arr.[1..4] |> arrToNum
    let product' = arr.[5..] |> arrToNum

    if (firstNum * secondNum = product) || (firstNum' * secondNum' = product')
    then
        Some product
    else
        None

let products = new HashSet<int>()

let arr = [|1;2;3;4;5;6;7;8;9|]
for i in 0 .. (fact 9) do
    match getPandigitalProduct arr with
    |Some num -> products.Add(num) |> ignore
    |None -> ()
    EulerHelper.Array.permute arr
    
let answer =
    products
    |>Seq.sum
