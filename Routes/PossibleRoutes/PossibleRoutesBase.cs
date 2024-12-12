using Routes.Models;

namespace Routes
{
    public abstract class PossibleRoutesBase
    {
        protected List<Route> _routes;
        protected char _from;
        protected Dictionary<char, List<Route>> _routeDict;

        public PossibleRoutesBase(List<Route> routes, char from)
        {
            _routes = routes;
            _routeDict = GetPossibleRoutesBySource(_routes);
            _from = from;
        }

        private Dictionary<char, List<Route>> GetPossibleRoutesBySource(List<Route> routes)
        {
            var result = new Dictionary<char, List<Route>>();

            foreach (Route route in routes)
            {
                if (result.ContainsKey(route.From))
                {
                    result[route.From].Add(route);
                }
                else
                {
                    result.Add(route.From, new List<Route> { route });
                }
            }
            return result;
        }
    }
}
