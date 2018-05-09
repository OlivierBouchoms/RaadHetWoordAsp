using System;
using System.Collections.Generic;
using Models;

namespace Data
{
    public class GameMemoryContext: IGameContext
    {
        public Game AddTeams(Game game, List<Team> teams)
        {
            return new Game(game.Maxscore, teams);
        }

        public Game AddWordlist(Game game, Wordlist wordlist)
        {
            game.AddWordlist(wordlist);
            return game;
        }

        public Tuple<Game, int> ThrowDice(Game game, int maxValue)
        {
            int change = new Random().Next(0, maxValue);
            game.CurrentRound.Team.Score -= new Random().Next(0, maxValue);
            return new Tuple<Game, int>(game, change);
        }
    }
}