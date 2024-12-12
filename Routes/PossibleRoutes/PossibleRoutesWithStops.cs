using Routes.Interfaces;
using Routes.Models;

namespace Routes
{
    public class PossibleRoutesWithStops : PossibleRoutesBase, IPossibleRoutes
    {
        private int _stops;

        public PossibleRoutesWithStops(List<Route> routes, char from, int stops) : base(routes, from)
        {
            _stops = stops;
        }

        public List<List<Route>> Get()
        {
            return GetPossibleRoutes(_routeDict, _from, _stops);
        }

        private List<List<Route>> GetPossibleRoutes(Dictionary<char, List<Route>> routeDict, char from, int stops)
        {
            var result = new List<List<Route>>();

            foreach (var route in routeDict[from])
            {
                if (stops > 1)
                {
                    var list = GetPossibleRoutes(routeDict, route.To, stops - 1);
                    if (list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            var subResult = new List<Route> { route };
                            subResult.AddRange(item);
                            result.Add(subResult);
                        }
                    }
                    else
                    {
                        var subResult = new List<Route> { route };
                        result.Add(subResult);
                    }
                }
                else
                {
                    var subResult = new List<Route> { route };
                    result.Add(subResult);
                }
            }

            return result;
        }
    }
}
