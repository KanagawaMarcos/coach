#r "nuget: FParsec, 1.1.1"

open FParsec

type Exercise = 
    | Curl
    | Pulley
    | Walk

type Equipement = 
    | Smith
    | Cross
    | Treadmill
    | None

type Rep = {
    Exercise: Exercise;
    Equipement: Equipement;
}

let pworkout =
    choice [
        stringReturn "Treadmill" { Exercise=Walk; Equipement=Treadmill}
        stringReturn "Walk"  {Exercise=Walk; Equipement= None}
    ]

let workout input =
    match run pworkout input with
        | Success(res, _, _) -> Result.Ok res
        | Failure(err, _, _) -> Result.Error err

workout "Walks"