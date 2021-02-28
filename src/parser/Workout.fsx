#r "nuget: FParsec, 1.1.1"
#r "nuget: xunit, 2.4.1"

open FParsec
open System
open Xunit

// =================== Domain ===================
[<Measure>] type kg // kilogram
[<Measure>] type m  // meter
[<Measure>] type s  // second

type Exercise = 
    | Curl
    | Pulley
    | Walk

type Equipment = 
    | Smith
    | Cross
    | Treadmill
    | None

type Load = 
    | Weight of int<kg>
    | Speed of int<m/s>

type Rep = {
    Exercise: Exercise;
    Equipment: Equipment;
    //Load: Load;
}

type Amount = 
    | Int
    | TillFailure

type Set = {
    Rep: Rep;
    Amount: Amount;
}

// =================== Logic ===================
let pRep =
    choice [
        stringReturn "treadmill" { Exercise=Walk; Equipment=Treadmill; }
        stringReturn "walk"  { Exercise=Walk; Equipment= None; }
        // stringReturn "walk"  { Exercise=Walk; Equipment= None; Load=?? }
    ]

let workout input =
    match run pRep input with
        | Success(res, _, _) -> Result.Ok res
        | Failure(err, _, _) -> Result.Error err

workout "walk"
workout "treadmill"

// =================== Specification ===================

[<Fact>]
let ``Syntax: walk -> ( Exercise: Walk,  Equipment: None)`` () =
    let expected = { Exercise= Walk; Equipment= None }
    let actual = 
        match workout "walk" with 
            | Result.Ok(res) -> res
            | Result.Error(errorValue) -> failwith errorValue
    Assert.Equal(expected, actual)

[<Fact>]
let ``Syntax: treadmill -> ( Exercise: Walk,  Equipment: Treadmill)`` () =
    let expected = { Exercise= Walk; Equipment= Treadmill }
    let actual = 
        match workout "treadmill" with 
            | Result.Ok(res) -> res
            | Result.Error(errorValue) -> failwith errorValue
    Assert.Equal(expected, actual)

``Syntax: walk -> ( Exercise: Walk,  Equipment: None)``()
``Syntax: treadmill -> ( Exercise: Walk,  Equipment: Treadmill)``()
