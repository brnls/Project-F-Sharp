
#load "loadfs.fsx"
open EulerHelper.Util
open EulerHelper
open System.IO
open System.Numerics

EulerHelper.Util.aToi

type Age =
| PossiblyAlive of int
| NotAlive

type AgeBuilder() =
    member this.Bind(x, f) =
        match x with
        | PossiblyAlive(x) when x >= 0 && x <= 120 -> f(x)
        | _ -> NotAlive
    member this.Delay(f) = f()
    member this.Return(x) = PossiblyAlive x

let age = new AgeBuilder()

let willBeThere a y =
  age { 
    let! current = PossiblyAlive a
    let! future = PossiblyAlive y

    return current + future
  }
willBeThere 38 50