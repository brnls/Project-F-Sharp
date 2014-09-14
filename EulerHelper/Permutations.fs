namespace EulerHelper

    module internal Permutations =

        ///Swap two members of an array
        let swap i j (x:'a[]) = 
            let mutable temp = x.[i]
            x.[i] <- x.[j]
            x.[j] <- temp

        let private getPivot (num : 'a[]) =
            let mutable pivot = num.Length - 2
            while pivot <> -1 && num.[pivot] > num.[pivot + 1] do
                pivot <- pivot - 1
            pivot

        let private getMinimum pivot (num: 'a[]) =
            let mutable leastVal = pivot + 1
            for i in (pivot + 2)..(num.Length - 1) do
                if num.[i] < num.[leastVal] && num.[i] >= num.[pivot] then
                    leastVal <- i
            leastVal

        let private permuteSubset pivot (num:'a[]) =     
                let pivotChunk = num.[pivot + 1..] |>Array.sort
                for i in pivot + 1 .. num.Length - 1 do 
                    num.[i] <- pivotChunk.[i - (pivot + 1)]

        let permute num =
            let pivot = getPivot num
            if pivot <> -1 then
                let minimumPosition = getMinimum pivot num
                swap pivot minimumPosition num
                permuteSubset pivot num