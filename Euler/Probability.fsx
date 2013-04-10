let probDist (intervalSize:float) vals =
    let max = vals |> List.max
    let min = vals |> List.min
    let intervalCount = int ((max - min)/intervalSize) + 1
    let distribution = Array.zeroCreate (int intervalCount)
    for i in 0..(intervalCount - 1) do
        let valsInRange = 
            vals
            |>List.filter (fun x -> float(x) >= min + float(i) * intervalSize && float(x) < min + float(i + 1) * intervalSize)
            |>List.length

        distribution.[int i] <- valsInRange
            
    distribution

let nearestNeighboorSmoothing (vals:int[]) = 
    for i in 1..(vals.Length - 2) do
        vals.[i] <- (vals.[i-1] + vals.[i + 1] + vals.[i])/3
    vals


