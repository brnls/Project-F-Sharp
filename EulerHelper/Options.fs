module Options


type OptionType =
    |Call
    |Put

type Option = {
    Type : OptionType;
    Strike: decimal;
}

type Instrument = 
    |Option of Option
    |Physical

type PositionType =
    |Long
    |Short
 
type Position = {
    Instrument : Instrument;
    Type: PositionType;
    Holdings : decimal;
    PurchasePrice : decimal; //Per unit holding
}

let optionValue (option:Option) currentPrice = 
     match option.Type with
    |Put -> max (option.Strike - currentPrice) 0m
    |Call -> max (currentPrice - option.Strike) 0m

let instrumentValue (instrument:Instrument) currentPrice =
    match instrument with
    |Physical -> currentPrice
    |Option opt -> optionValue opt currentPrice

let positionProfit (position:Position) currentPrice =
    match position.Type with
    |Long -> (instrumentValue position.Instrument currentPrice - position.PurchasePrice) * position.Holdings
    |Short -> (position.PurchasePrice - instrumentValue position.Instrument currentPrice) * position.Holdings

let positionsSumProfit positions currentPrice =
    positions
    |>Seq.sumBy(fun x-> positionProfit x currentPrice)

let opt optType strike posType holdings purchasePrice =
    {Instrument = Option {Type = optType; Strike = strike;}; Type = posType; Holdings = holdings; PurchasePrice = purchasePrice;}



