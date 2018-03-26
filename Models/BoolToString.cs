namespace Models
{
    public class BoolToString
    {
        public static string Convert(bool input)
        {
            if (input)
            {
                return "Ja";
            }
            return "Nee";
        }
    }
}