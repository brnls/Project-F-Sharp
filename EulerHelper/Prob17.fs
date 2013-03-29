module Prob17

open System.Numerics

let rec iToWords = function
    |0 -> ""
    |1 -> "one"
    |2 -> "two"
    |3 -> "three"
    |4 -> "four"
    |5 -> "five"
    |6 -> "six" 
    |7 -> "seven"
    |8 -> "eight"
    |9 -> "nine"
    | 10 -> "ten"
    | 11 -> "eleven"
    | 12 -> "twelve"
    | 13 -> "thirteen"
    | 14 -> "fourteen"
    | 15 -> "fifteen"
    | 16 -> "sixteen"
    | 17 -> "seventeen"
    | 18 -> "eighteen"
    | 19 -> "nineteen" 
    | x when x < 30 -> "twenty" + (if x <> 20 then "-" else "") + iToWords (x-20)
    | x when x < 40 -> "thirty" + (if x <> 30 then "-" else "") + iToWords (x-30)
    | x when x < 50 -> "fourty" + (if x <> 40 then "-" else "") + iToWords (x-40)
    | x when x < 60 -> "fifty" + (if x <> 50 then "-" else "") + iToWords (x-50)
    | x when x < 70 -> "sixty" + (if x <> 60 then "-" else "") + iToWords (x-60)
    | x when x < 80 -> "seventy" + (if x <> 70 then "-" else "") + iToWords (x-70)
    | x when x < 90 -> "eighty" + (if x <> 80 then "-" else "") + iToWords (x-80)
    | x when x < 100 -> "ninety" + (if x <> 90 then "-" else "") + iToWords (x-90)
    | x when x < 1000 ->
        let hundreds = x/100
        let tens = x % 100
        iToWords hundreds + " hundred" + (if x % 100 = 0 then "" else " and " + iToWords tens)
    | x when x < 1000000 ->
        let thousands = x/1000
        let hundreds = x % 1000
        iToWords thousands + " thousand" + (if x % 1000 = 0 then "" else " " + iToWords hundreds)                                            
    | _ -> "to high"

let countChars str = 
    str
    |>String.map (fun x-> if (x = ' ' || x = '-') then '$' else x)
    |>List.ofSeq
    |>Seq.sumBy(fun x -> if x <> '$' then 1 else 0)
   
iToWords 5353   
|>countChars

{1..1000}
|>Seq.map iToWords
|>Seq.sumBy(fun x-> countChars x)

iToWords 999999

