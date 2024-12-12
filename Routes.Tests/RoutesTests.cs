using Routes.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;

namespace Routes.Tests
{
    public class RoutesTests
    {
        /***** ARRANGE *****/

        private List<Route> routeTable = new List<Route>
        {
            new Route('A', 'B', 5),
            new Route('B', 'C', 4),
            new Route('C', 'D', 8),
            new Route('D', 'C', 8),
            new Route('D', 'E', 6),
            new Route('A', 'D', 5),
            new Route('C', 'E', 2),
            new Route('E', 'B', 3),
            new Route('A', 'E', 7)
        };

        [Fact]
        [Description("The distance of the route A=>B=>C IS 9")]
        public void Test1()
        {
            var distance = Utilities.GetDistance(routeTable, 'A', 'B', 'C');
            Assert.Equal(distance, 9);
        }

        [Fact]
        [Description("The distance of the route A=>D is 5")]
        public void Test2()
        {
            var distance = Utilities.GetDistance(routeTable, 'A', 'D');
            Assert.Equal(distance, 5);
        }

        [Fact]
        [Description("The distance of the route A=>D=>C is 13")]
        public void Test3()
        {
            var distance = Utilities.GetDistance(routeTable, 'A', 'D', 'C');
            Assert.Equal(distance, 13);
        }

        [Fact]
        [Description("The distance of the route A=>E=>B=>C=>D is 22")]
        public void Test4()
        {
            var distance = Utilities.GetDistance(routeTable, 'A', 'E', 'B', 'C', 'D');
            Assert.Equal(distance, 22);
        }

        [Fact]
        [Description("Route A=>E=>D doesn't exist")]
        public void Test5()
        {
            var distance = Utilities.GetDistance(routeTable, 'A', 'E', 'D');
            Assert.Equal(distance, -1);
        }

        [Fact]
        [Description("Number of trips from C to C with maximum 3 stops is 2 ( C=>D=>C, C=>E=>B=>C )")]
        public void Test6()
        {
            var possibleRoutes = new PossibleRoutesWithPoints(routeTable, 'C', 'C');

            var subTable = possibleRoutes.Get();
            var list = subTable.Where(routes => routes.Count <= 3);

            Assert.Equal(list.Count(), 2);
            Assert.True(list.Any(routes => Utilities.GetRoutesString(routes).Equals("C=>D=>C")));
            Assert.True(list.Any(routes => Utilities.GetRoutesString(routes).Equals("C=>E=>B=>C")));
        }

        [Fact]
        [Description("Number of trips from A to C with exactly 4 stops is 3 ( A=>B=>C=>D=>C, A=>D=>C=>D=>C, A=>D=>E=>B=>C )")]
        public void Test7()
        {
            var possibleRoutes = new PossibleRoutesWithStops(routeTable, 'A', 4);
            var subTable = possibleRoutes.Get();

            var list = subTable.Where(routes => routes[3].To == 'C');
            int count = list.Count();
            Assert.Equal(count, 3);

            Assert.True(list.Any(routes => Utilities.GetRoutesString(routes).Equals("A=>B=>C=>D=>C")));
            Assert.True(list.Any(routes => Utilities.GetRoutesString(routes).Equals("A=>D=>C=>D=>C")));
            Assert.True(list.Any(routes => Utilities.GetRoutesString(routes).Equals("A=>D=>E=>B=>C")));
        }

        [Fact]
        [Description("The length of the shortest route from A to C is 9 ( A=>B=>C )")]
        public void Test8()
        {
            var possibleRoutes = new PossibleRoutesWithPoints(routeTable, 'A', 'C');

            var subTable = possibleRoutes.Get();

            var distances = subTable.Select(routes => routes.Select(r => r.Distance).Aggregate((acc, r) => acc + r)).ToList();
            int min = distances.Min();
            Assert.Equal(min, 9);
            
            var index = distances.FindIndex(x => x == min);
            var routes = subTable[index];
            Utilities.GetRoutesString(routes).Equals("A=>B=>C");
        }

        [Fact]
        [Description("The length of the shortest route from B to B is 9 ( B=>C=>E=>B )")]
        public void Test9()
        {
            var possibleRoutes = new PossibleRoutesWithPoints(routeTable, 'B', 'B');

            var subTable = possibleRoutes.Get();

            var distances = subTable.Select(routes => routes.Select(r => r.Distance).Sum()).ToList();
            int min = distances.Min();
            Assert.Equal(min, 9);

            var index = distances.FindIndex(x => x == min);
            var routes = subTable[index];
            Assert.True(subTable.Any(routes => Utilities.GetRoutesString(routes).Equals("B=>C=>E=>B")));
        }

        [Fact]
        [Description("The number of trips from C to C with distance less than 30 is 7 ( C=>D=>C, C=>D=>C=>E=>B=>C, C=>D=>E=>B=>C, C=>E=>B=>C, C=>E=>B=>C=>D=>C, C=>E=>B=>C=>E=>B=>C, C=>E=>B=>C=>E=>B=>C=>E=>B=>C )")]
        public void Test10()
        {
            var possibleRoutes = new PossibleRoutesWithDistance(routeTable, 'C', 'C', 30);

            var subTable = possibleRoutes.Get();

            Assert.Equal(subTable.Count, 7);

            Assert.True(subTable.Any(routes => Utilities.GetRoutesString(routes).Equals("C=>D=>C")));
        }
    }
}