namespace Routes.Models
{
    public class Route
    {
        public char From {  get; set; }
        public char To { get; set; }
        public int Distance { get; set; }

        public Route(char from, char to, int distance)
        {
            From = from;
            To = to;
            Distance = distance;
        }
    }
}
