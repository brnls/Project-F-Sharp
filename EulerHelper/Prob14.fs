module Prob14

open EulerHelper.Util
open System.Collections.Generic

let collatzDic = new System.Collections.Generic.Dictionary<int64,int>(1000000)
let collatzCount n = 
    let nConst = n
    let rec loop n acc =
        match collatzDic.TryGetValue(n) with
        |(false, y) ->
             match (n % 2L) with
                |0L ->
                    match n with
                    |1L -> 
                        collatzDic.Add(nConst, acc)
                        acc
                    |_ -> loop (n/2L) (acc + 1) 
                |_ -> 
                    match n with
                    |1L ->
                        collatzDic.Add(nConst, acc)
                        acc
                    |_ -> loop (3L*n + 1L) (acc + 1)
        |(true, count) -> 
            collatzDic.Add(nConst, count + acc)
            acc + count
    loop n 1

let answ =
    {2L..1000000L}
    |>Seq.map(fun x-> (x, collatzCount x))
    |>Seq.maxBy snd


