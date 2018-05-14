using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace Data
{
    public class TeamMSSQLContext : ITeamContext
    {
        /// <summary>
        /// Checks if a team already exists in the database
        /// </summary>
        public bool CheckIfExists(Team team)
        {
            var sqlConnection = DataBase.MsSql;
            sqlConnection.Open();

            const string prcoedure = "TeamExists";
            var sqlCommand = new SqlCommand(prcoedure, sqlConnection) { CommandType= CommandType.StoredProcedure};
            sqlCommand.Parameters.Add("name", SqlDbType.NChar).Value = team.Name;
            var sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
                return true;
            }
            sqlConnection.Close();
            sqlConnection.Dispose();
            return false;
        }

        /// <summary>
        /// Adds a team to the database
        /// </summary>
        public bool AddTeam(Team team)
        {
            var sqlConnection = DataBase.MsSql;
            sqlConnection.Open();

            const string procedure = "AddTeam";
            var sqlCommand = new SqlCommand(procedure, sqlConnection) { CommandType = CommandType.StoredProcedure };
            sqlCommand.Parameters.Add("name", SqlDbType.NChar).Value = team.Name;
            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
                return true;
            }
            sqlConnection.Close();
            sqlConnection.Dispose();
            return false;
        }

        /// <summary>
        /// Increase score of given team
        /// </summary>
        public bool IncreaseScore(Team team)
        {
            var sqlConnection = DataBase.MsSql;
            sqlConnection.Open();

            const string prcoedure = "IncreaseScore";
            var sqlCommand = new SqlCommand(prcoedure, sqlConnection) { CommandType = CommandType.StoredProcedure };
            sqlCommand.Parameters.Add("name", SqlDbType.NChar).Value = team.Name;
            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
                return true;
            }
            sqlConnection.Close();
            sqlConnection.Dispose();

            return false;
        }

        /// <summary>
        /// Decrease score of given team
        /// </summary>
        public bool DecreaseScore(Team team)
        {
            var sqlConnection = DataBase.MsSql;
            sqlConnection.Open();

            const string procedure = "DecreaseScore";
            var sqlCommand = new SqlCommand(procedure, sqlConnection) {CommandType = CommandType.StoredProcedure};
            sqlCommand.Parameters.Add("name", SqlDbType.NChar).Value = team.Name;
            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
                return true;
            }
            sqlConnection.Close();
            sqlConnection.Dispose();
            return false;
        }

        /// <summary>
        /// Increase turns of given team
        /// </summary>
        public bool IncreaseTurns(Team team)
        {
            var sqlConnection = DataBase.MsSql;
            sqlConnection.Open();

            const string commandText = "IncreaseTurns";
            var sqlCommand = new SqlCommand(commandText, sqlConnection) {CommandType = CommandType.StoredProcedure};
            sqlCommand.Parameters.Add("name", SqlDbType.NChar).Value = team.Name;
            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
                return true;
            }
            sqlConnection.Close();
            sqlConnection.Dispose();
            return false;
        }

        public bool IncreaseWins(Team team)
        {
            var sqlConnection = DataBase.MsSql;
            sqlConnection.Open();

            const string procedure = "IncreaseWins";
            var sqlCommand = new SqlCommand(procedure, sqlConnection) {CommandType = CommandType.StoredProcedure};
            sqlCommand.Parameters.Add("name", SqlDbType.NChar).Value = team.Name;
            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
                return true;
            }
            sqlConnection.Close();
            sqlConnection.Dispose();
            return false;
        }

        public bool IncreaseLosses(Team team)
        {
            var sqlConnection = DataBase.MsSql;
            sqlConnection.Open();

            const string procedure = "IncreaseLosses";
            var sqlCommand = new SqlCommand(procedure, sqlConnection) {CommandType = CommandType.StoredProcedure};
            sqlCommand.Parameters.Add("name", SqlDbType.NChar).Value = team.Name;
            if (sqlCommand.ExecuteNonQuery() > 0)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
                return true;
            }
            sqlConnection.Close();
            sqlConnection.Dispose();
            return false;
        }

        /// <summary>
        /// Get all teams sorted by score
        /// </summary>
        public List<Team> GetTeams()
        {
            var teams = new List<Team>();
            var sqlConnection = DataBase.MsSql;
            sqlConnection.Open();

            const string procedure = "GetTeams";
            var sqlCommand = new SqlCommand(procedure, sqlConnection) {CommandType = CommandType.StoredProcedure};
            var sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                teams.Add(new Team(sqlDataReader.GetString(1), sqlDataReader.GetInt32(2), sqlDataReader.GetInt32(3), sqlDataReader.GetInt32(4), sqlDataReader.GetInt32(5), sqlDataReader.GetDecimal(7), sqlDataReader.GetDecimal(6)));
            }

            sqlDataReader.Close();
            sqlConnection.Close();
            sqlConnection.Dispose();

            return teams;
        }
    }
}
