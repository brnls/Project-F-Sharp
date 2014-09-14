module Prob28

let spiralSequence1 = 
    Seq.unfold(fun (rowSize, currentSum, currentValue, cornerCount) ->
        let newRowSize = if cornerCount + 1 = 4 then rowSize + 2 else rowSize
        let newCurrentValue = currentValue + rowSize
        let newCurrentSum = currentSum + newCurrentValue
        let newCornerCount = if cornerCount + 1 = 4 then 0 else cornerCount + 1
        (newCurrentSum, (newRowSize, newCurrentSum, newCurrentValue, newCornerCount))
        |>Some
        )

spiralSequence1 (0,0,1,3)
|>Seq.take 2001
|>Seq.last |> printfn "%A"

//Alternate Solution using sequences
let rec getSpiralDiagonals i interval=
    seq{
        yield! seq{for j in 0 .. 3 do yield i + interval * j}
        yield! getSpiralDiagonals (i + interval * 3 + (interval + 2)) (interval + 2)
    }

getSpiralDiagonals 3 2
|>Seq.take(2000)
|>Seq.sum
|> (+) 1

