using Routes.Models;

namespace Routes.Interfaces
{
    public interface IPossibleRoutes
    {
        List<List<Route>> Get();
    }
}
