using System;
using System.Data;
using System.Data.SQLite;

namespace Data
{
    public class ExceptionSqLiteContext : IExceptionContext
    {
        /// <summary>
        /// Insert data from exception that is thrown
        /// </summary>
        /// <param name="exception">Exception that was thrown</param>
        public bool LogException(Exception e)
        {
            var sqLiteConnection = DataBase.SqLite;
            sqLiteConnection.Open();

            var commandText =
                "INSERT INTO Exception (Data, HelpLink, HResult, InnerException, Message, Source, StackTrace, TargetSite) VALUES (@data, @helplink, @hresult, @innerexception, @message, @source, @stacktrace, @targetsite)";
            var sqLiteCommand = new SQLiteCommand(commandText, sqLiteConnection);
            sqLiteCommand.CommandType = CommandType.Text;
            sqLiteCommand.Parameters.AddWithValue("data", e.Data);
            sqLiteCommand.Parameters.AddWithValue("helplink", e.HelpLink);
            sqLiteCommand.Parameters.AddWithValue("hresult", e.HResult);
            sqLiteCommand.Parameters.AddWithValue("innerexception", e.InnerException);
            sqLiteCommand.Parameters.AddWithValue("message", e.Message);
            sqLiteCommand.Parameters.AddWithValue("source", e.Source);
            sqLiteCommand.Parameters.AddWithValue("stacktrace", e.StackTrace);
            sqLiteCommand.Parameters.AddWithValue("targetsite", e.TargetSite);

            if (sqLiteCommand.ExecuteNonQuery() > 0)
            {
                sqLiteConnection.Close();
                return true;
            }

            sqLiteConnection.Close();
            return false;
        }
    }
}