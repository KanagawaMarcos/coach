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

type Equipement = 
    | Smith
    | Cross
    | Treadmill
    | None

type Load = 
    | Weight of int<kg>
    | Speed of int<m/s>

type Rep = {
    Exercise: Exercise;
    Equipement: Equipement;
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
        stringReturn "treadmill" { Exercise=Walk; Equipement=Treadmill; }
        stringReturn "walk"  { Exercise=Walk; Equipement= None; }
    ]

let workout input =
    match run pRep input with
        | Success(res, _, _) -> Result.Ok res
        | Failure(err, _, _) -> Result.Error err

workout "walk"
workout "treadmill"

// =================== Specification ===================

[<Fact>]
let ``walk`` () =
    let expected = { Exercise= Walk; Equipement= None }
    let actual = workout "walk"
    
    Assert.Equal(expected, actual)

``Full Workout Session``()