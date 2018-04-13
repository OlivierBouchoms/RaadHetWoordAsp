namespace Models
{
    public class Team
    {
        public string Name { get; set; }
        public int Score { get; private set; }
        public int Turns { get; private set; }
        public int Wins { get; }
        public int Losses { get; }
        public decimal ScorePerTurn { get; }
        public decimal WinLoss { get; }

        public Team()
        {
            //Empty
        }

        public Team(string name)
        {
            Name = name;
            Score = 0;
            Turns = 0;
            Wins = 0;
            Losses = 0;
            ScorePerTurn = 0;
            WinLoss = 0;
        }

        public Team(string name, int score, int turns, int wins, int losses, decimal scorePerTurn, decimal winLoss)
        {
            Name = name;
            Score = score;
            Turns = turns;
            Wins = wins;
            Losses = losses;
            ScorePerTurn = scorePerTurn;
            WinLoss = winLoss;
        }

        public void IncreaseScore()
        {
            Score++;
        }

        public void DecreaseScore()
        {
            if (Score > 0)
            {
                Score--;
            }
        }

        public void IncreaseTurns()
        {
            Turns++;
        }

        public override string ToString()
        {
            return $"Team {Name} - Turns {Turns} - Score {Score}";
        }
    }
}
