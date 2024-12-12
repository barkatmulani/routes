using Routes.Interfaces;
using Routes.Models;

namespace Routes
{
    public class PossibleRoutesWithDistance : PossibleRoutesBase, IPossibleRoutes
    {
        private char _to;
        private int _maxDistance;

        public PossibleRoutesWithDistance(List<Route> routes, char from, char to, int maxDistance) : base(routes, from)
        {
            _to = to;
            _maxDistance = maxDistance;
        }

        public List<List<Route>> Get()
        {
            var subTable = GetPossibleRoutes(_routeDict, _from, _maxDistance);

            foreach (var list in subTable)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    var route = list[i];

                    if (route.To != 'C')
                    {
                        list.RemoveAt(i);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            var result = new List<List<Route>>();

            foreach (var list in subTable)
            {
                if (!result.Exists(list2 => Utilities.GetRoutesString(list) == Utilities.GetRoutesString(list2)))
                {
                    result.Add(list);
                }
            }

            return result;
        }

        private List<List<Route>> GetPossibleRoutes(Dictionary<char, List<Route>> routeDict, char from, int maxDistance)
        {
            var result = new List<List<Route>>();

            foreach (var route in routeDict[from])
            {
                if (route.Distance < maxDistance)
                {
                    var list = GetPossibleRoutes(routeDict, route.To, maxDistance - route.Distance);
                    if (list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            var subResult = new List<Route> { route };
                            subResult.AddRange(item);

                            int totalDistance = subResult.Select(x => x.Distance).Sum();

                            if (totalDistance < maxDistance)
                            {
                                result.Add(subResult);
                            }
                            else if (route.Distance < maxDistance)
                            {
                                var subResult2 = new List<Route> { route };
                                result.Add(subResult2);
                            }
                        }
                    }
                    else
                    {
                        var subResult = new List<Route> { route };
                        result.Add(subResult);
                    }
                }
            }

            return result;
        }
    }
    }
