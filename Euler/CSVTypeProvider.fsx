
type Result<'myGenTypeA, 'myGenTypeB> =
    |Success of 'myGenTypeA
    |Failure of 'myGenTypeB

type Request = {name:string; email:string}

let validateInput input = 
    if input.name = "" then Failure "Name must not be blank"
    else if input.email = "" then Failure "Email must not be blank"
    else Success input

let bind func =
    function
    |Success s -> func s
    |Failure f -> Failure f

let validate1 input =
   if input.name = "" then Failure "Name must not be blank"
   else Success input

let validate2 input =
   if input.name.Length > 50 then Failure "Name must not be longer than 50 chars"
   else Success input

let validate3 input =
   if input.email = "" then Failure "Email must not be blank"
   else Success input

let validate1' = bind validate1

let combinedValidation =
     validate1 >> (bind validate2) >>(bind validate3)

let (>>=) twoTrackInput switchFunction =
    bind switchFunction twoTrackInput

let eval x = 
    validate1 x
    >>= validate2
    >>= validate3

let b = {name = ""; email = "abc@a.com" }

let (>=>) switch1 switch2 =
    switch1 >> (bind switch2)

let validate4 x = 
    {x with email="Brian"}

let switchify func =
    fun x-> Success (func x)

let switch f x =
    f x |> Success

let canonicalizeEmail input =
   { input with email = input.email.Trim().ToLower() }


let usecase = 
    validate1
    >=> validate2
    >=> validate3
    >=> (switchify canonicalizeEmail)

let goodInput = {name="Alice"; email="UPPERCASE    "}
usecase goodInput

let badInput = {name=""; email="UPPERCASE    "}
usecase badInput
|> printfn "Canonicalize Bad Result = %A"

let map func =
    function
    |Success s -> Success(func s)
    |Failure f -> Failure f

let map2 func twoTrackInput = 
    match twoTrackInput with
    |Success s -> Success(func s)
    |Failure f -> Failure f

let map3 func =
    bind (switchify func)

let usecase2 = 
    validate1
    >=> validate2
    >=> validate3
    >> map3 canonicalizeEmail 

usecase2 badInput

let deadend input =
    failwith "ERROR1234"
    printfn "deadend"

let tee f x =
    f x |> ignore
    x

let usecase4 = 
    validate1
    >> tee deadend
  
usecase4 badInput

let tryCatch f x =
    try 
        f x |> Success
    with
    | ex -> Failure ex.Message

let usecase5 =
    validate1
    >> bind validate2
    >=> validate3
    >> map2 canonicalizeEmail
    >=> tryCatch (tee deadend)

usecase5 goodInput


let succeed x =
    Success x

let fail x = 
    Failure x

let plus1 switch1 switch2 x=
    match (switch1 x), (switch2 x) with
    |Success s1, Success s2 -> Success (s1 + s2)
    |Failure f1, Success _ -> Failure f1
    |Success _, Failure f2 -> Failure f2
    |Failure f1, Failure f2 -> Failure (f1 + f2)

let plus addSuccess addFailure switch1 switch2 x=
    match (switch1 x), (switch2 x) with
    |Success s1, Success s2 -> Success (addSuccess s1 s2)
    |Failure f1, Success _ -> Failure f1
    |Success _, Failure f2 -> Failure f2
    |Failure f1, Failure f2 -> Failure (addFailure f1 f2)
    
let (&&&) v1 v2 =
    let addSuccess r1 r2 = r2
    let addFailure s1 s2 = s1 + "; " + s2
    plus addSuccess addFailure v1 v2

let combinedValidation2 = 
    validate1
    &&& validate2
    &&& validate3


let input1 = {name=""; email=""}
combinedValidation2 input1

let input2 = {input1 with email="brnls"}
combinedValidation2 input2

let input3 = {input2 with name="brian"}

let usecase6 =
    validate1
    &&& validate2
    &&& validate3
    >> map2 canonicalizeEmail
    >=> tryCatch (tee deadend)

let doubleMap successFunc failureFunc =
    function
    |Success s -> Success(successFunc s)
    |Failure f -> Failure(failureFunc f)

type Config = {debug:bool}

let debugLogger twoTrackInput = 
    let success x = printfn "DEBUG. SUCCESS so far: %A" x; x
    let failure = id
    doubleMap success failure twoTrackInput

let injectableLogger config = 
    if config.debug then debugLogger else id

let usecase7 config =
    combinedValidation
    >> injectableLogger config
    >> map canonicalizeEmail
    >> injectableLogger config

let input4 = {name="Alice"; email="GOOD"}

let releaseConfig = {debug=false}
let debugConfig = {debug=true}

input4
|>usecase7 releaseConfig

input4
|> usecase7 debugConfig

let add a b =
    a + b

let timesTwo a =
    a * 2


let bef = 
    timesTwo 
    >> add 4

let a = (|>)

//>> add 4 