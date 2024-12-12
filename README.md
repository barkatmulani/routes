# Design Decisions

The solution contains two projects:

## Routes Project

### Concrete Classes
#### PossibleRoutesWithPoints Class
Possible Routes, given the **Source & Destination Points**

#### PossibleRoutesWithStops Class
Possible Routes, given the total **Stops**

#### PossibleRoutesWithDistance Class
Possible Routes, given the total **Max Distance** covered by all the routes

### Other Classes / Interfaces

#### PossibleRoutesBase Class
Base abstract class for the concrete classes containing a few shared functions

#### IPossibleRoutes Interface
Interface to be implemented by the concrete classes with a method ```Get```

#### Utilities Class
Contains static functions used by the above classes as well as the unit tests

#### Route Class
Contains the model properties of a Route object (From / To / Distance)

### Notes
- The ```Get``` method in each concrete class is responsible to get a list, whose each element is a list of routes meeting the given criteria.
- Each concrete class has a private recursive method ```GetPossibleRoutes``` to calculate the routes relevant the concrete class instance created.

## Routes Tests Project

Contains a RoutesTests.cs file containing 10 unit tests.
