namespace MyONI

open FSharp.Data.UnitSystems.SI.UnitSymbols

[<AutoOpen>]
module Cargo =
    type Solids =
        | AlienArtifacts
        | Aluminum
        | CarbonFiber
        | CeramicPlates
        | CircuitBoards
        | Cobalt
        | Cocaine
        | ConcreteMix
        | Copper
        | FabricRolls
        | FertilizerPellets
        | FiberOptics
        | Fish
        | FreezeDriedMeals
        | Fruit
        | FuelRods
        | GlassPanels
        | Gold
        | Grain
        | Heroin
        | Ice
        | Iron
        | Ketamine
        | MDMA
        | MeatsAssorted
        | MedicalHerbs
        | Microchips
        | Nickel
        | NutrientBars
        | OldEarthAntiques
        | Platinum
        | Rubber
        | Salt
        | Sand
        | Silicon
        | Silver
        | Spices
        | StructuralSteel
        | Sulfur
        | TimberPlanks
        | Titanium
        | Uranium
        | Vegetables

    type Liquids =
        | CleanWater
        | PollutedWater
        | Alcohol
        | AntisepticSolution
        | Brine
        | Coolant
        | CrudeOil
        | Ethanol
        | LiquidAmmonia
        | Lubricant
        | NutrientSlurry
        | RefinedFuel

    type Gases =
        | CleanOxygen
        | PollutedOxygen
        | Ammonia
        | Argon
        | Bromine
        | CarbonDioxide
        | Chlorine
        | Fluoride
        | Helium
        | Hydrogen
        | Methane
        | Neon
        | Nitrogen
        | SulfurDioxide
        | Xenon

    type CargoType =
        | Solids of Solids * decimal<kg>
        | Liquids of Liquids * decimal<L>
        | Gases of Gases * decimal<L>
