using System;

namespace Models
{
    public class Team
    {
        public string Name { get; set; }
        public int Score { get; private set; }
        public int Turns { get; private set; }
        public int Wins { get; private set; }
        public int Losses { get; private set; }
        public decimal ScorePerTurn { get; private set; }
        public decimal WinLoss { get; private set; }

        public Team(string name)
        {
            Name = name;
            Score = 0;
            Turns = 0;
        }

        public void IncreaseScore()
        {
            Score++;
        }

        public void DecreaseScore()
        {
            Score--;
        }

        public void IncreaseTurns()
        {
            Turns++;
        }

        public override string ToString()
        {
            return $"Team {Name} - Turns {Score} - Score {Turns}";
        }
    }
}
