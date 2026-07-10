namespace MyONI

open FSharp.Data.UnitSystems.SI.UnitSymbols

[<AutoOpen>]
module Spaceships =
    type DependentClass =
        | Gunship // Avg length(m): 22, Avg max width(m): 16, Avg max height(m): 7, Avg tonnage(kg): 19000, Avg solids intake(kg/s): 0, Avg gas/liquids intake(L/s): 0
        | Interceptor // Avg length(m): 15, Avg max width(m): 12, Avg max height(m): 3.5, Avg tonnage(kg): 8500, Avg solids intake(kg/s): 0, Avg gas/liquids intake(L/s): 0
        | Scout // Avg length(m): 15, Avg max width(m): 18, Avg max height(m): 4, Avg tonnage(kg): 9200, Avg solids intake(kg/s): 0, Avg gas/liquids intake(L/s): 0

    type IndependentClass =
        | Battleship // Avg length(m): 450, Avg max width(m): 120, Avg max height(m): 85, Avg tonnage(kg): 125000000, Avg solids intake(kg/s): 2500, Avg gas/liquids intake(L/s): 1000
        | Drone // Avg length(m): 35, Avg max width(m): 25, Avg max height(m): 12, Avg tonnage(kg): 45000, Avg solids intake(kg/s): 50, Avg gas/liquids intake(L/s): 10
        | Freighter // Avg length(m): 550, Avg max width(m): 200, Avg max height(m): 110, Avg tonnage(kg): 65000000, Avg solids intake(kg/s): 150, Avg gas/liquids intake(L/s): 600
        | ScienceVessel // Avg length(m): 180, Avg max width(m): 60, Avg max height(m): 45, Avg tonnage(kg):, Avg solids intake(kg/s): 400, Avg gas/liquids intake(L/s): 250

    type StarshipClass =
        | Dependent of DependentClass
        | Independent of IndependentClass
        | Carrier of Option<List<int * DependentClass>> // Carrier: Avg length(m): 900, Avg max width(m): 320, Avg max height(m): 130, Avg tonnage(kg): 210000000, Avg solids intake(kg/s): 6000, Avg gas/liquids intake(L/s): 3000

    type Manifest =
        { Name: string
          CaptainName: string
          FirstOfficerName: string
          Sponsor: Sponsor option
          Class: StarshipClass
          Length: decimal<m>
          Width: decimal<m>
          Height: decimal<m>
          Tonnage: decimal<ton>
          SolidsIntake: decimal<kg / s>
          GasLiquidsIntake: decimal<L / s>
          Cargo: CargoType list }

    type Spaceship =
        { Manifest: Manifest
          Crew: (Profession * int) list }

    type DockRequest =
        { Manifest: Manifest
          Intent: Intent
          Currency: LegalTender }
