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

let optionValue (opt:Option) currentPrice = 
     match opt.Type with
    |Put -> max (opt.Strike - currentPrice) 0m
    |Call -> max (currentPrice - opt.Strike) 0m

let instrumentValue (inst:Instrument) currentPrice =
    match inst with
    |Physical -> currentPrice
    |Option opt -> optionValue opt currentPrice

let positionProfit (pos:Position) currentPrice =
    match pos.Type with
    |Long -> (instrumentValue pos.Instrument currentPrice - pos.PurchasePrice) * pos.Holdings
    |Short -> (pos.PurchasePrice - instrumentValue pos.Instrument currentPrice) * pos.Holdings

let positionsSumProfit positions currentPrice =
    positions
    |>Seq.sumBy(fun x-> positionProfit x currentPrice)



