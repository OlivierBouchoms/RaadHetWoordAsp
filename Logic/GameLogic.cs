using System.Collections.Generic;
using System.Diagnostics;
using Data;
using Models;

namespace Logic
{
    public class GameLogic
    {
        private readonly GameRepository _repo;

        public GameLogic(GameRepository repo)
        {
            _repo = repo;
        }

        public Game AddTeams(List<Team> teams, Game game)
        {
            return _repo.AddTeams(teams, game);
        }

        public Game AddWordlist(Game game, Wordlist wordlist)
        {
            return _repo.AddWordlist(game, wordlist);
        }

        public bool GameIsOver(Game game)
        {
            Debug.WriteLine($"Maximum score: {game.Maxscore}");
            for (int i = 0; i < 2; i++)
            {
                if (game.TeamList[i].Score >= game.Maxscore)
                {
                    return true;
                }
            }
            return false;
        }

        public Team GetWinner(Game game)
        {
            var winner = new Team();
            for (int i = 0; i < 2; i++)
            {
                if (game.TeamList[i].Score > winner.Score)
                {
                    winner = game.TeamList[i];
                }
            }

            return winner;
        }

        public Team GetLoser(Game game)
        {
            var loser = new Team(true);
            for (int i = 0; i < 2; i++)
            {
                if (game.TeamList[i].Score < loser.Score)
                {
                    loser = game.TeamList[i];
                }
            }

            return loser;
        }

       
    }
}
