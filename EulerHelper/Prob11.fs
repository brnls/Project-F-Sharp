module Prob11

open EulerHelper.Util
open EulerHelper
open System.IO

let a = 
    File.ReadAllLines(@"E:\GitHub\Project F Sharp\EulerHelper\Prob11Text.txt")
    |>Array.map(fun x-> x.Split(' '))

let grid = Array2D.init a.Length a.[0].Length (fun x y -> int a.[x].[y])

//These represent the four point coordinates of the four possible line orientations on the grid. Parameter is staring coordinate, output is 
//four coordinates that make up the line
let vertical (x:int,y:int) = 
    [|(x,y); (x+1,y); (x+2, y); (x+3, y);|]

let horizontal (x:int,y:int)= 
    [|(x,y); (x,y+1); (x, y+2); (x, y + 3);|]

let diagBack (x:int,y:int) = 
    [|(x,y); (x+1,y+1); (x+2,y+2); (x+3,y+3);|]

let diag (x:int,y:int)= 
    [|(x,y); (x-1,y+1); (x-2,y+2); (x-3,y+3);|]

let multLine line =
    line
    |>Array.map(fun (a,b) -> grid.[a,b])
    |>Array.fold (*) 1

// These are all sets of each type of line orientation in a 20x20 grid
let Verts = 
    seq{for i in 0..16 do
            for j in 0..19 do
                yield vertical (i,j)}
let Horizontals =
    seq{for i in 0..19 do
            for j in 0..16 do
                yield horizontal(i,j)}

let Diagonals = 
    seq{for i in 3..19 do
            for j in 0..16 do
                yield diag(i,j)}

let DiagonalBack = 
    seq{for i in 0..16 do
            for j in 0..16 do
                yield diagBack(j,i)}

//Combine all sequences, find the product, find the maximum product
Seq.append Verts Horizontals |> Seq.append Diagonals |> Seq.append DiagonalBack
|>Seq.map(fun x-> multLine x)
|>Seq.max


