using System;
using System.Collections.Generic;
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
            var winner = new Team { Score = 0 };
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
            var loser = new Team { Score = Int32.MaxValue};
            for (int i = 0; i < 2; i++)
            {
                if (game.TeamList[i].Score < loser.Score)
                {
                    loser = game.TeamList[i];
                }
            }
            
            return loser;
        }

        public Tuple<Game, int> ThrowDice(Game game)
        {
        	if (game.CurrentRound.Team.Score == 0) { return new Tuple<Game, int>(game, 0); }
        	var maxValue = 2;
        	if (game.CurrentRound.Team.Score == 1) { maxValue = 1;}

            return _repo.ThrowDice(game, maxValue);
        }

       
    }
}
