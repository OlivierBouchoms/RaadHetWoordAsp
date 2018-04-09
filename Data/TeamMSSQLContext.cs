﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using Models;

namespace Data
{
    public class TeamMSSQLContext : ITeamContext
    {
        /// <summary>
        /// Checks if a team already exists in the database.
        /// </summary>
        /// <param name="team">Input team</param>
        /// <returns>True/false value</returns>
        public bool CheckIfExists(Team team)
        {
            string result = String.Empty;
            using (var sqlconn = DataBase._SqlConn)
            {
                sqlconn.Open();
                string query = String.Format("Select [Name] From [Team] where [Name] ='{0}'", team.Name);

                using (var sqlcommand = new SqlCommand(query, sqlconn))
                {
                    using (var reader = sqlcommand.ExecuteReader())
                    {
                        {
                            try
                            {
                                if (reader.HasRows)
                                {
                                    sqlconn.Close();
                                    return true;
                                }
                                sqlconn.Close();
                                return false;
                            }
                            catch { }
                        }
                    }
                }
                sqlconn.Close();
            }
            return result == team.Name;
        }

        /// <summary>
        /// Adds a team to the database
        /// </summary>
        /// <param name="team">Input team</param>
        /// <returns>True/false value</returns>
        public bool AddTeam(Team team)
        {
            string query = String.Format(
                "Insert into Team ([Name], [Score], [Turns], [Wins], [Losses]) " +
                "Values ('{0}', 0, 0, 0, 0)",
                team.Name);
            using (var sqlconn = DataBase._SqlConn)
            {
                sqlconn.Open();
                using (var sqlcommand = new SqlCommand(query, sqlconn))
                {
                    sqlcommand.ExecuteNonQuery();
                    sqlconn.Close();
                    return true;
                }
            }
        }

        public bool FillTeamWithData(Team team)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mag weg
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool DeleteTeam(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A list with teams, sorted by alphabet (a-z)</returns>
        public List<Team> GetTeamsByAlphabet()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A list with teams, sorted by score</returns>
        public List<Team> GetTeamsByScore()
        {
            throw new NotImplementedException();
        }

        public List<Team> GetTeamByTurns()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A list with teams, sorted by wins</returns>
        public List<Team> GetTeamsByWins()
        {
            throw new NotImplementedException();
        }

        public List<Team> GetTeamsByWinLoss()
        {
            throw new NotImplementedException();
        }
    }
}
