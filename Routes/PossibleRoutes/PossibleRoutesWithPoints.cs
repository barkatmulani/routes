using Routes.Interfaces;
using Routes.Models;

namespace Routes
{
    public class PossibleRoutesWithPoints : PossibleRoutesBase, IPossibleRoutes
    {
        private char _to;

        public PossibleRoutesWithPoints(List<Route> routes, char from, char to) : base(routes, from)
        {
            _to = to;
        }

        public List<List<Route>> Get()
        {
            return GetPossibleRoutes(_routeDict, _from, _to);
        }

        private List<List<Route>> GetPossibleRoutes(Dictionary<char, List<Route>> routeDict, char from, char to, List<Route>? parentRoutes = null)
        {
            var result = new List<List<Route>>();

            if (parentRoutes == null)
            {
                parentRoutes = new List<Route>();
            }

            foreach (var route in routeDict[from])
            {
                if (route.To == to)
                {
                    var subResult = new List<Route> { route };
                    result.Add(subResult);
                    parentRoutes.Add(route);
                }
                else
                {
                    if (!parentRoutes.Contains(route))
                    {
                        var parentRoutes2 = parentRoutes.Select(x => x).ToList();
                        parentRoutes2.Add(route);
                        var list = GetPossibleRoutes(routeDict, route.To, to, parentRoutes2);
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
                }
            }

            return result;
        }
    }
}
