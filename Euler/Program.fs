module Program

open System.Collections.Generic
open EulerHelper.Util
open System



let divideBy bottom top =
    if bottom = 0
    then None
    else Some(top/bottom)

let divideByWorkflow init x y z =
    match (init |> divideBy x) with
    |None -> None
    |Some a' ->
        match (a' |> divideBy y) with
        |None -> None
        |Some b' ->
            match(b' |> divideBy z) with
            |None -> None
            |Some c' -> Some c'

let answer2 = divideByWorkflow 1000 5 4 2

let bad = divideByWorkflow 1000 4 0 2 

type MaybeBuilder() =
    member this.Bind(x,f) =
       match x with
       |None -> None 
       |Some x' -> f x'

    member this.Return(x) =
        Some x

    member this.ReturnFrom(x) =
        x

let maybe = new MaybeBuilder()

let test = maybe{
    let! x = 6 |> divideBy 3
    let! y = x |> divideBy 1
    let! z = y |> (fun x' -> if x' = 2 then Some "Success!" else None)
    return! Some z
    }

let noSugar = 
    maybe.Bind(6 |> divideBy 3, fun x ->
        maybe.Bind(x |> divideBy 1, fun y ->
            maybe.Return y))

open System
let strToInt x = 
    let success, parsedInt = Int32.TryParse(x)
    if success then Some parsedInt
    else None

let stringAddWorkflow x y z = 
    maybe{
        let! a = strToInt x
        let! b = strToInt y
        let! c = strToInt z
        return a + b + c
    }

let strAdd str i =
    let success, parsedInt = Int32.TryParse(str)
    if success then Some (parsedInt + i)
    else None

let (>>=) m f = 
    match m with
    |Some i -> f i
    |None -> None


type DbResult<'a> =
    |Success of 'a
    |Error of string

type DbResultBuilder() =

    member this.Bind(x,f) =
        match x with
        |Error _ -> x
        |Success s -> f s

    member this.Return(x) =
        Success x

let db = new DbResultBuilder()

let getCustomerId name =
    if (name = "") 
    then Error "getCustomerId failed"
    else Success "Cust42"

let getLastOrderForCustomer custId =
    if (custId = "") 
    then Error "getLastOrderForCustomer failed"
    else Success "Order123"

let getLastProductForOrder orderId =
    if (orderId  = "") 
    then Error "getLastProductForOrder failed"
    else Success "Product456"

let workFlow = db{
    let! customerId = getCustomerId "org"
    let! orderId = getLastOrderForCustomer ""
    let! productId = getLastProductForOrder orderId
    return productId
    }


type CustomerId =  CustomerId of string
type OrderId =  OrderId of int
type ProductId =  ProductId of string

module EmailAddress =

    type T = EmailAddress of string

    let Create (s:string) =
        if System.Text.RegularExpressions.Regex.IsMatch(s,@"^\S+@\S+\.\S+$")
            then Some (EmailAddress s)
            else None

    let Value (EmailAddress e) = e


let myEmail = EmailAddress.Create "brnls1@aol.com"
let badEmail = EmailAddress.Create "xyzbad.com"

match badEmail with
|Some e -> EmailAddress.Value e |> printfn "the value is %s"
|None -> ()


type StringIntBuilder() =
    
    member this.Bind(m,f) =
        let b,i = System.Int32.TryParse(m)
        match b,i with
        |false, _ -> "error"
        |true, i -> f i
        
    member this.Return(x) =
        sprintf "%i" x


let stringint = new StringIntBuilder()

let good =
    stringint{
        let! i = "42"
        let! j = "43"
        return i+j
        }
        

type ListWorflowBuilder() =

    member this.Bind(list,f) = 
        list |> List.collect f      

    member this.Return(x) =
        [x]

    member this.For(list,f) =
        this.Bind(list,f)

let listWorkflow = new ListWorflowBuilder()

let testWorkflowNosugar =
    listWorkflow.Bind([2;3;4], fun elem1 ->
    listWorkflow.Bind([6;7;8], fun elem2 ->
        listWorkflow.Return(elem1 + elem2)
    ))


let testWorkflow = 
    listWorkflow{
        for i in [2;3;4] do
        for j in [10;11;12] do
        return i*s
        }

let testWorkflow2 = 
    listWorkflow{
        let! i = [2;3;4]
        let! j = [10;11;12] 
        return i*j
        }