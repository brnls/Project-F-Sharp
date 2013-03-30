module Prob19

open EulerHelper.Util
open EulerHelper
open System.IO
open System.Numerics
open System

let mutable date = new DateTime(1901,1,1)

let mutable count = 0
while date < new DateTime(2001,1,1) do
    if date.Day = 1 && date.DayOfWeek = DayOfWeek.Sunday then
        count <- count + 1
    date <- date.AddDays(1.0)
