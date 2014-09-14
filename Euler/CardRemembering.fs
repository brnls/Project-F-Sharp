module CardRemembering

open StockTools
open Global
open System.Configuration
open System
open System.Collections.Generic
open System.Linq
open EulerHelper.Util
open Cards

let selectRandom (sequence:seq<'a>) = 
    let rand = Random()
    let randomElement = int (floor (rand.NextDouble()* float (Seq.length sequence)))
    sequence
    |>Seq.nth randomElement      

let binaryToHex (bin:int) = 
    let stringBinary = Convert.ToString(bin)
    Convert.ToInt32(stringBinary,2).ToString("x")

let hexToBinary (hex:string) =
    let stringHex = Convert.ToInt32(hex,16)
    Convert.ToString(stringHex,2).PadLeft(4, '0')

let intToBinary (i:int) = 
    Convert.ToString(i,2).PadLeft(4, '0')

let intToHex (i:int) = 
    Convert.ToString(i,16)

let suitOrder suit =
    match suit with
    |Spade -> 0
    |Heart -> 1
    |Diamond -> 2
    |Club -> 3

let suitOrderRev int = 
    match int with
    |0 -> Spade
    |1 -> Heart
    |2 -> Diamond
    |3 -> Club
    |_ -> failwith "invalid Suit"

let getRandomBinary (rand:Random) =
    let mutable chars : int list = []
    for i in 0..3 do
        chars <- (int (floor(2.0 * rand.NextDouble())))::chars
    chars

let getRandomBinaryInt (binList : int list) = 
    let mutable binAnsw = 0;
    for i in 0..3 do
        if binList.[i] = 1 then
            binAnsw <- binAnsw + int (Math.Pow(10.0,float (3-i)))
    binAnsw

let getCardsOfSuit suit (cardList:Card list) = 
    cardList
    |>List.filter(fun x-> cardSuit x = suit)

let binarySetToCards (rank:Rank) (binary: int list)  = 
    let bin = binary |>Array.ofList
    let cardsOfRank = Deck |>List.filter(fun x-> cardRank x = rank)
    let mutable cards : Card list = []
    for i in 0..3 do
        if bin.[i] = 1 then
            cards <- (cardsOfRank |>List.find(fun x-> cardSuit x = suitOrderRev i))::cards
    cards


let makeGuess duration (cards:('a * 'b) list) =
    let timer = Diagnostics.Stopwatch()
    let currentCard = selectRandom cards
    Console.WriteLine(fst currentCard)
    timer.Start()
    let guess = Console.ReadKey().KeyChar
    timer.Stop()
    let elapsed = timer.Elapsed.TotalSeconds
    if guess = Convert.ToChar(snd currentCard) && elapsed < duration then 
        Console.WriteLine("\n Got it! \n")
        (cards |>List.filter (fun x-> (fst x <> fst currentCard)), elapsed )
    else 
        Console.WriteLine("\n Took too long! The answer is " + (snd currentCard).ToString() + "\n")
        System.Threading.Thread.Sleep(600)
        (currentCard::cards, elapsed)

let playGame duration (cards:seq<'a * 'b>) = 
    let mutable playAgain = 'y'
    let rec loop (cards:('a * 'b) list) = 
        match fst(makeGuess duration cards) with
        |[] ->()
        |x->
           // x |>List.iter(fun x -> printfn "%A \n" x) 
            loop x
    while playAgain = 'y' do
        loop (List.ofSeq cards)
        Console.WriteLine("\n Would you like to play again? (y/n) \n")
        playAgain <- Console.ReadKey().KeyChar
        Console.WriteLine("\n")
    
    Console.WriteLine("Done")
