using System;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;

namespace Data
{
    public class ExceptionSqLiteContext : IExceptionContext
    {
        /// <summary>
        /// Insert data from exception that is thrown
        /// </summary>
        /// <param name="exception">Exception that was thrown</param>
        public bool LogException(Exception exception)
        {
            var sqLiteConnection = DataBase.SqLite;
            try
            {
                sqLiteConnection.Open();
            }
            catch (Exception e)
            {
                return false;
            }

            var commandText =
                "INSERT INTO Exception (Data, HelpLink, HResult, InnerException, Message, Source, StackTrace, TargetSite) VALUES (@data, @helplink, @hresult, @innerexception, @message, @source, @stacktrace, @targetsite)";
            var sqLiteCommand = new SQLiteCommand(commandText, sqLiteConnection);
            sqLiteCommand.CommandType = CommandType.Text;
            sqLiteCommand.Parameters.AddWithValue("data", exception.Data);
            sqLiteCommand.Parameters.AddWithValue("helplink", exception.HelpLink);
            sqLiteCommand.Parameters.AddWithValue("hresult", exception.HResult);
            sqLiteCommand.Parameters.AddWithValue("innerexception", exception.InnerException);
            sqLiteCommand.Parameters.AddWithValue("message", exception.Message);
            sqLiteCommand.Parameters.AddWithValue("source", exception.Source);
            sqLiteCommand.Parameters.AddWithValue("stacktrace", exception.StackTrace);
            sqLiteCommand.Parameters.AddWithValue("targetsite", exception.TargetSite);

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