module Prob19

open System

let dt = new DateTime(1901,1,1)

let dates = Seq.unfold (fun (x:DateTime)-> if x.Year > 2000 then None else Some(x, x.AddDays(1.0))) dt

let answ = 
    dates
    |>Seq.filter(fun x -> x.Day = 1 && x.DayOfWeek = DayOfWeek.Sunday)
    |>Seq.length

