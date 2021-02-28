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

let pworkout = pstring "treadmill" >>% Treadmill

let workout input =
    match run pworkout input with
        | Success(res, _, _) -> Result.Ok res
        | Failure(err, _, _) -> Result.Error err

workout "Treadmill"