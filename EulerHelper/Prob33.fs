module Prob33

open EulerHelper.Util

type Fraction  ={
        Numerator : int * int;
        Denominator : int* int;
    }

let toNum tuple =
    fst tuple * 10 + snd tuple

let value fraction = 
    float (toNum fraction.Numerator) / float (toNum fraction.Denominator)

let cancel fraction =
    let fracVal = value fraction
    match fraction with
    |x when fst fraction.Numerator = snd fraction.Denominator -> 
        match x with
        |y when float(snd fraction.Numerator) / float (fst fraction.Denominator) = fracVal  -> Some fraction
        |_ -> None
    |x when snd fraction.Numerator = fst fraction.Denominator -> 
        match x with
        |y when float(fst fraction.Numerator) / float (snd fraction.Denominator) = fracVal -> Some fraction
        |_ -> None
    |_ -> None

let fractions = seq { for i in 1 .. 9 do
                        for j in 1 .. 9  do
                            for k in i .. 9 do
                                for l in 1 .. 9 do yield {Numerator=i,j; Denominator = k,l}
                                    }

let answer = 
    fractions
    |>Seq.filter(fun x -> value x < 1.)
    |>Seq.choose cancel
    |>Seq.fold(fun (x,y) frac -> (toNum frac.Numerator * x, toNum frac.Denominator * y)) (1,1)
    |> (fun (x,y) -> y / EulerHelper.Util.gcd32 x y)
    