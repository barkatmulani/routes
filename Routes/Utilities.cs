using Routes.Models;
using System.Data;

namespace Routes
{
    public static class Utilities
    {
        public static int GetDistance(IList<Route> routes, params char[] points)
        {
            int distance = 0;

            for (int i = 0; i < points.Length - 1; i++)
            {
                var pointA = points[i];
                var pointB = points[i + 1];

                var route = routes.FirstOrDefault(r => (r.From == pointA) && (r.To == pointB));
                if (route != null)
                {
                    distance += route.Distance;
                }
                else
                {
                    distance = -1;
                    break;
                }
            }

            return distance;
        }

        public static string GetRoutesString(List<Route> routes)
        {
            if (routes.Count == 0)
            {
                return string.Empty;
            }

            string res = routes[0].From.ToString();

            foreach (var route in routes)
            {
                res += "=>" + route.To.ToString();
            }

            return res;
        }
    }
}
