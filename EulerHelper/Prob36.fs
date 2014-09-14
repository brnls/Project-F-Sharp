module Prob36
open EulerHelper.Util
open System

let convertToBinary (int:int) =
    Convert.ToString(int,2)

let isPalindromicInBothBases num = 
    isPalindrome (EulerHelper.Array.intToArray num) && 
    isPalindrome ((convertToBinary num).ToCharArray())

let answ = 
    {0..999999}
    |>Seq.filter isPalindromicInBothBases
    |>Seq.sum