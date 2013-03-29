
open System
module Convolve =

    ///Parameters are signal, filter, window mid point (the line on the 2D plot where you expect the signal to be, where the filter will be passed over)
    let convolve2D (signal:float[,]) (filter:float[,]) windowMidPoint =
        let conv:float[] = Array.zeroCreate (signal.GetUpperBound(0) + filter.GetUpperBound(0))
        let windowMiddle = windowMidPoint
        let filterLength = filter.GetUpperBound 0 + 1
        let signalLength = signal.GetUpperBound 0 + 1
        for i in (filterLength/2)..(signalLength - filterLength / 2) do
            let mutable total = 0.0
            let mutable count = 0.0
            for j in 0..(filterLength - 1) do
                for k in 0..(filterLength - 1) do
                    let signalVal = signal.[i - filterLength/2 + k, windowMiddle - filterLength/2 + j]
                    if signalVal <> 0.0 then
                        total <- total + signalVal * filter.[k,j] 
                        count <- count + 1.0
            conv.[i] <- total/(double count)
        conv

    ///Accepts signal, filter, returns convolvution
    let convolve1D (signal:float[]) (filter:float[]) =
        let conv:(float)[] = Array.zeroCreate (signal.Length + filter.Length)
        for i in (filter.Length/2)..(signal.Length - (filter.Length/2) - 1) do
            let mutable total = 0.0
            let mutable count = 0.0
            for j in 0..(filter.Length - 1) do
                let signalVal = signal.[i - (filter.Length/2) + j]
                if signalVal <> 0.0 then
                    total <- total +  signalVal * filter.[j] 
                    count <- count + 1.0
        conv

    let addNoise2D scale data = 
        let rand = new Random()
        data
        |>Array2D.map (fun x -> x + (scale * rand.NextDouble()))