module Cards

type Suit = 
    |Spade
    |Heart
    |Diamond 
    |Club
    with 
        override this.ToString() = 
            match this with
            |Spade -> "Spade"
            |Heart -> "Heart"
            |Diamond -> "Diamond"
            |Club -> "Club"


type Rank = 
    |Ace 
    |King
    |Queen
    |Jack 
    |Value of int
    with
        override this.ToString() =
            match this with
            |Ace -> "Ace"
            |King -> "King"
            |Queen -> "Queen"
            |Jack -> "Jack"
            |Value x-> x.ToString()

type Card = 
    |Card of (Rank * Suit)
    with
        override this.ToString() =
            match this with
            |Card (x,y) -> x.ToString() + " of " + y.ToString() + "s"

let cardSuit card = 
    match card with
    |Card (x,y) -> y

let cardRank card = 
    match card with
    |Card (x,y) -> x

let Deck = 
    [for suit in [Spade;Heart;Diamond;Club;] do
        for rank in [Ace;King;Queen;Jack;] do
            yield Card(rank,suit)
        for i in 2..10 do
            yield Card(Value i,suit)
        ]

