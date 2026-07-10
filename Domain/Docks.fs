namespace MyONI

open FSharp.Data.UnitSystems.SI.UnitSymbols

[<AutoOpen>]
module Docks =
    type DockProfile =
        { Name: string
          Length: decimal<m>
          Width: decimal<m>
          Height: decimal<m>
          PumpRate: decimal<L / s> }

    type DockType =
        | LightBay of DockProfile // Length(m): 28, Width(m): 24, Height(m): 15, Volume(m^3): 10080, Pump rate(L/s): 28000
        | UtilityHanger of DockProfile // Length(m): 45, Width(m): 35, Height(m): 22, Volume(m^3): 34650, Pump rate(L/s): 64100
        | CruiserDock of DockProfile // Length(m): 220, Width(m): 80, Height(m): 70, Volume(m^3): 1232000, Pump rate(L/s): 342000
        | CapitalDrydock of DockProfile // Length(m): 650, Width(m): 240, Height(m): 140, Volume(m^3): 21840000, Pump rate(L/s): 3033000
        | SuperDrydock of DockProfile // Length(m): 1050, Width(m): 380, Height(m): 180, Volume(m^3): 71820000, Pump rate(L/s): 6650000

    let calculateFillTime (dock: DockProfile) : decimal<s> =
        let roomVolume = dock.Length * dock.Width * dock.Height
        // 1 cubic meter = 1000 liters
        let totalGasNeeded = roomVolume * 1000.0M<L / m^3>
        let totalPumpRate = dock.PumpRate * 6.0M // 6 pumps per dock, each dock with its own pump type!
        totalGasNeeded / totalPumpRate

// Kept as documentation for the moment, until I decide if I want it as-is or refactor it
// let getDockForShip (ship: Spaceships.StarshipClass) (stationDocks: DockType list) : DockProfile option =
//     match ship, stationDocks with
//     | (Dependent Interceptor | Dependent Scout | Dependent Gunship), LightBay profile :: _ -> Some profile
//     | Independent Drone, UtilityHanger profile :: _ -> Some profile
//     | Independent ScienceVessel, CruiserDock profile :: _ -> Some profile
//     | (Independent Battleship | Independent Freighter), CapitalDrydock profile :: _ -> Some profile
//     | Carrier _, SuperDrydock profile :: _ -> Some profile
//     | _ -> None // Compiler forces you to handle mismatches!
