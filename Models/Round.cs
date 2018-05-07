namespace Models
{
    public class Round
    {
        public Team Team { get; set; }
        public static int Playerindex { get; set; }

        public Round()
        {
            //Empty
        }

        public Round(Game game)
        {
            Team = game.TeamList[Playerindex];
            if (Playerindex == game.TeamList.Count - 1)
            {
                Playerindex = 0;
            }
            else
            {
                Playerindex++;
            }
        }
    }
}
