//open MSDN.FSharp.Charting
//open System.Windows.Forms.DataVisualization
//open Convolve
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

let swap i j (num:int[]) = 
    let mutable temp = num.[i]
    num.[i] <- num.[j]
    num.[j] <- temp

let getPivot (num:int[]) =
    let mutable pivot = num.Length - 2
    while pivot <> -1 && num.[pivot] > num.[pivot + 1] do
        pivot <- pivot - 1
    pivot

let getMinimum pivot (num:int[]) =
    let mutable leastVal = pivot + 1
    for i in (pivot + 2)..(num.Length - 1) do
        if num.[i] < num.[leastVal] && num.[i] >= num.[pivot] then
            leastVal <- i
    leastVal

let permuteSubset pivot (num:int[]) =     
        let pivotChunk = num.[pivot + 1..] |>Array.sort
        for i in pivot + 1 .. num.Length - 1 do 
            num.[i] <- pivotChunk.[i - (pivot + 1)]

let myArr = [|1;2;3;4;5;|]

let permute num =
    let pivot = getPivot num
    if pivot <> -1 then
        let minimumPosition = getMinimum pivot num
        swap pivot minimumPosition num
        permuteSubset pivot num


let pivot = getPivot myArr
let minimumPosition = getMinimum pivot myArr
//swap pivot minimumPosition myArr
//swap 3 4 myArr
myArr

permute myArr
myArr
for i in 0..10 do
    permute myArr
    printfn "%A" myArr
    