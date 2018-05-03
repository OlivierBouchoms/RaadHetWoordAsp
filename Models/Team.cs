using System;

namespace Models
{
    public class Team
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int Turns { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set;  }
        public decimal ScorePerTurn { get; set; }
        public decimal WinLoss { get; set; }

        public Team()
        {
            //Empty
        }

        public Team(bool loser)
        {
            Score = int.MaxValue;
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
            return $"Team {Name} - Score {Score}";
        }
    }
}
