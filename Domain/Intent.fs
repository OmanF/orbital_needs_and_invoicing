namespace MyONI

[<AutoOpen>]
module Intent =
    type DockingIntent =
        | Refuel
        | Resupply
        | Repairs
        | Commerce
        | CrewRotate

    type Intent = Set<DockingIntent>

    let emptyIntent: Intent = Set.empty

    let intentOfList (items: DockingIntent list) : Intent = Set.ofList items

    let hasIntent (item: DockingIntent) (intent: Intent) : bool = Set.contains item intent

    let addIntent (item: DockingIntent) (intent: Intent) : Intent = Set.add item intent

    let removeIntent (item: DockingIntent) (intent: Intent) : Intent = Set.remove item intent
