#r "nuget: FParsec, 1.1.1"

open FParsec

[<Measure>] type kg
[<Measure>] type meter
[<Measure>] type second

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
    | Speed of int<meter/second>

type Rep = {
    Exercise: Exercise;
    Equipement: Equipement;
    Load: Load;
}

let pworkout =
    choice [
        stringReturn "Treadmill" { Exercise=Walk; Equipement=Treadmill; Load=3<meter/second> }
        stringReturn "Walk"  { Exercise=Walk; Equipement= None }
    ]

let workout input =
    match run pworkout input with
        | Success(res, _, _) -> Result.Ok res
        | Failure(err, _, _) -> Result.Error err

workout "Walks"