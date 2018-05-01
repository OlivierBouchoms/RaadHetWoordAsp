using System;
using System.Collections;
using System.Data;
using System.Data.SQLite;

namespace Data
{
    public class ExceptionSqLiteContext : IExceptionContext
    {
        /// <summary>
        /// Insert data from exception that is thrown
        /// </summary>
        /// <param name="e">Exception that was thrown</param>
        public bool LogException(Exception e)
        {
            var sqLiteConnection = DataBase.SqLite;
            sqLiteConnection.Open();

            const string commandText =
                "INSERT INTO Exception (Data, HelpLink, HResult, InnerException, Message, Source, StackTrace, TargetSite) VALUES (@data, @helplink, @hresult, @innerexception, @message, @source, @stacktrace, @targetsite)";
            var sqLiteCommand = new SQLiteCommand(commandText, sqLiteConnection) {CommandType = CommandType.Text};
            sqLiteCommand.Parameters.AddWithValue("data", e.ToString());
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
                sqLiteConnection.Dispose();
                return true;
            }

            sqLiteConnection.Close();
            sqLiteConnection.Dispose();
            return false;
        }

    }
}