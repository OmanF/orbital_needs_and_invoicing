# Oribtal Needs and Invoicing - Spec/PRD (WIP - 17.07.26)

An exercise in modelling a complex, multi-system, domain simulating space survival sandbox games, specifically, interactions between a spacestation and visiting spaceships, by exposing the APIs of the spacestation's management console.

## Technical consideration

The simulation is modelled to, as much as possible, be agnostic. Not of something specific, just agnostic. Of whatever can be.  
Types and interfaces are defined upfront, and all "side effects", e.g., logging, data obtaining, etc., is validated at the boundaries and transformed to a form the simulation is able to ingest.

## Spacestation

By far the most important aspect of the simulation: the spacestation is run for profit. Any interaction between ships and the station incurs a cost. (More about costs and payment later in the spec).

Other than general regulations imposed by "the powers that be", e.g., what materials are considered contraband across the entire simulation, the station's management, via its various APIs and random generators, is at liberty to decide its own rules and actions. The ships are at the whims of the station totally.

The station acts like the Continental hotels in the "John Wick" movies, i.e., no violence is allowed within its perimeter. (Technically, the application simply doesn't have APIs for violence).

This section detailing the station will elaborate on:

* The ships' requests interface - the way ships request services from the station (and what those services are), including, most importantly, the right to dock
* The station's docking facilities and how they're handled
* Crew rotation protocol
* Payment mechanism - nothing is free...unless the station management decides it is
* Randomized station management decisions

### Ships' request interface

The bulk of the simulation is handling spaceships interaction with the station. All such interaction begins with a ship submitting a request via the dedicated requests API.

In the request the ship states its manifest and intent (note that while the intent is always true, the manifest, especially the cargo onboard, might not be):

* Ship's name
* Ship's captain's name
* Ship's first officer's name
* Ship's sponsor (optional)
* Ship's class
* **Actual** ship's measurements
  * Length (in meters)
  * Maximal width (in meters)
  * Maximal height (in meters)
  * Tonnage (i.e., ship's hull mass, not regarding crew size, cargo, etc.) (in kg)
* Ship's solids intake rate (in kg/s)
* Ship's gases/liquids intake rate (in L/s)
* Ship's cargo
  * List of itemized pairs: item, weight (in kg) (see Appendix A)
* Ship's docking intent (See Appendix C)
  * In case crew rotation is requested, request must detail:
    * List of itemized pair to relieve: profession, number of members (see Appendix B)
    * List of itemized pair to onboard: profession, number of members (see Appendix B)
    * There is no obligation the lists are equal - a ship is free to relieve any combination of current crew, and onboard any combination of fresh crew
* The ship's currency of choice

### Docks

The station berths spaceships in specialized docks: each size category of ships can dock in its own size-apporpriate dock. This is important to prevent the supply pumps of a large-sized dock, which are suitable for larger ships, with enhanced intake rates, from pushing too much supplies, too fast, to a smaller ship, effectively risking literally exploding the ship (think blowing a party balloon with an industrial pump).

All docks are essentially rooms, with reinforced floors to allow withstanding even the largest, heaviest, ships, that have space-sealed doors that close once the ship has come to a full stop on the floor.  
Once docked, and the pod bay doors closed, depending on the ship's needs, the dock can be filled with either inert gas **or** breathable (but also combustible) oxygen. Not both at once.  
Inert gas, which is of course cheaper, allows resupplying, and is the **only** environment in which repairs can be made (it being non-reactive, allows welding, and other operation where reactive gases, like oxygen, are not ideal, to say the least). It does **not** however allow crew rotation - humans need to breath oxygen.  
Breathable oxygen (actually a mixture of oxygen and nitrogen that simulates Earth's atmosphere at about 1.223 Kg/m^3... it doesn't matter, the station's computer are correctly calibrated for this, and this environment is referred to as "breathable oxygen") allows crew rotation and resupplying, but is prohibited for repairs.

Each dock has 6 pumps, optimized for the dock's size and purpose. Those pumps pump in the inert gas/oxygen.  
As one gas is pumped in it eventually displaces the other one completely.

The initial state of the dock, once the doors have been closed is vacuum, of course, which is also the environment if the pod doors have been opened for any reason (one such reason is clearing out a dock's enviornment instatnounsly - the space vacuum drains even the largest docks environment **instantly**).

### Crew rotation protocol

For the matter of accepting relieved crew from docked ships, the station has infinite capcaity - all relieved crew can embark.

All relieved crew members are entitled to 7 days vacation before being rotated again.  
This vacation is **mandatory** and can't be waived, shortened, or in any other way disturbed.

Crew members might also become ill at any moment: either while waiting for assignment on the station, or while on a ship... being relieved. The station accepts all crew, regardless of their current health status.

Due to the spacestation's excellent medical care, once at the station's facilities, any ill crew member has a 50% chance, of recovering on the start of each new day.  
If a crew member has been ill for 3 days straight and starts the 4th day on the station (i.e., has not been assigned) is cured on the start of the 4th day.

Sick crew members are the lowest priority of being assigned, but, **can** be if there's no other option to fulfill a crew rotation request from a ship.

No crew member can decline and assignment.

Crew rotation happens on the beginning of the following day. In case of sick members, if assigned on their 3rd day of sickness, according to previous clause, will become healthy on the beginning of the next day, their 4th day of sickness, and will therefore board their assgined ship healthy.

### Payment

Every operatrion the station takes on behalf of the ships visting is billable and due at the moment of incurring the cost, i.e., docking costs must be covered to enter the dock, resupplies costs must be covered for supplies to be transferred, and so on.

There are (currently) three currencies that are legal tender in the system:

* Galactic Dalla (`GDR`) - the leading currency, modelling `USD`
* Emperial Zuz (`EMZ`) - rated at the exchange rate between `USD` and `JPY`, the Japenese Yen
* Universal Peso (`UNP`) - rated at the exchange rate between `USD` and `ARS`, the Argentinian Peso
* All rates to be retrieved via HTTP call to a validated exchange rate API (to be decided later) once per **real-life** day, at 00:00:00

Ships paying in currencies other than `GDR` incur a flat 50.0 `GDR` fee on top of the costs of the operation being performed. This commission is attached **per operation, not globably**!  
For example: a ship asking to dock and resupply while paying in `EMZ` will incur commission for: 1. Docking, 2. Filling the docking bay with inert gas (mandatory for resuplying, see section about docking), 3. on top of the cost of supplies.

Payment is one of two options: payed from the captain's personal account, or by the ship's sponsor.  
Personal payments are easy enough to figure: **before** service is supplied, the account is charged. If charge is fully covered, service is rendered completely and fully (technically, there's no API for either the station or the ship to renege on service, once payment is finalized).  
Sponsorship is more complex: The ship claims its sponsor. If the sponsor is **not** on the station's known sponsors, the deal is off (unless paid for by the captain's personal account, of course). Even if the sponsor is accepted, each sponsor has set a **global** limit per each ship it's sponsoring (technically, draw a random number, the minimal and maximal limit of which to be decided at a later (real-world) date) - if, at any moment a service request by the ship breaches that limit, that service, and any subsequent requests, are denied. Only if the sponsor is recognized, and the total amount of requests is under the limit will the request be fulfilled by the station. (Technically, use ROP to model this chain of decisions, as each step is dependent upon its predecessors, and failing fast is a requirement).

#### Appendix A - List of cargo

* Solids
  * Alien artifacts
  * Aluminum
  * Carbon fiber
  * Ceramic plates
  * Circuit boards
  * Cobalt
  * Cocaine
  * Concrete mix
  * Copper
  * Fabric rolls
  * Fertilizer pellets
  * Fiber optics
  * Fish
  * Freeze-dried meals
  * Fruit
  * Fuel rods
  * Glass panels
  * Gold
  * Grain
  * Heroin
  * Ice
  * Iron
  * Ketamine
  * MDMA
  * Meats (assorted)
  * Medical herbs
  * Microchips
  * Nickel
  * Nutrient bars
  * Old Earth antiques
  * Platinum
  * Rubber
  * Salt
  * Sand
  * Silicon
  * Silver
  * Spices
  * Structural steel
  * Sulfur
  * Timber planks
  * Titanium
  * Uranium
  * Vegetables
* Liquids
  * Clean water
  * Polluted water
  * Alcohol
  * Antiseptic solution
  * Brine
  * Coolant
  * Crude oil
  * Ethanol
  * Liquid ammonia
  * Lubricant
  * Nutrient slurry
  * Refined fuel
* Gases
  * Clean Oxygen
  * Polluted Oxygen
  * Ammonia
  * Argon
  * Bromine
  * Carbon dioxide
  * Chlorine
  * Fluoride
  * Helium
  * Hydrogen
  * Methane
  * Neon
  * Nitrogen
  * Sulfur dioxide
  * Xenon

#### Appendix A1 - List of globally recognized contraband

* Alien artifact
* Cocaine
* Heroin
* Ketamine
* MDMA
* Old earth antiques
* Uranium

#### Appendix B - List of professions

* Captain
* FirstOffice
* Pilots
* Engineers
* Doctors
* Nurses
* Scientists
* Security (AKA "Red shirts")
* Drones
* Cooks

#### Appendix C - List of ship docking intents

* Refueling
* Resupplying
* Repairs
* Commerce
* Crew rotation
* **Any** combination of the above

--------------------------------------------------

The station management's at liberty to declare any other material contraband, and remove such status, at will.

When a ship asks to dock, it is automatically scanned for contraband, detection rate is at `P(x) = 1 - exp(-x / 1500.0)` where `x` is the weight of the contraband in kilogram.  
This test is applied to each contraband the ship is carrying!  
The test is applied whether the ship requests to do commerce or not, purely on intent of docking.  
The station's management is at liberty to decide how to handle each contraband infraction on a case-by-case basis... there's no one rule fits all! The options available to the management are to: refuse docking the ship entirely; allow docking but refuse either commerce, or crew rotation, or both (assuming either, or both, those intents were requested); or, allow dock and effecitvely ignore the detected contraband.
