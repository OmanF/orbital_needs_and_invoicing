namespace MyONI

open FSharp.Data.UnitSystems.SI.UnitSymbols

[<AutoOpen>]
module Measures =
    [<Measure>]
    type GDR // Galactic Dalla

    [<Measure>]
    type EMZ // Emperial Zuz

    [<Measure>]
    type UNP // Universal Peso

    [<Measure>]
    type ton

    [<Measure>]
    type L

    type LegalTender =
        | GDR
        | EMZ
        | UNP

    let toTonnage (mass: decimal<kg>) : decimal<ton> = mass / 1000.0m<kg / ton>

    let fromTonnage (tonnage: decimal<ton>) : decimal<kg> = tonnage * 1000.0m<kg / ton>
