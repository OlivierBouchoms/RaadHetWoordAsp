using System;
using Data;

namespace Logic
{
    public class ExceptionLogLogic
    {
        private ExceptionLogRepository repo;

        public ExceptionLogLogic(ExceptionLogRepository exceptionLogRepository)
        {
            repo = exceptionLogRepository;
        }

        public bool LogException(Exception e)
        {
            try
            {
                repo.LogException(e);
            }
            catch (Exception exception)
            {
                repo = new ExceptionLogRepository(new ExceptionXMLContext());
                repo.LogException(exception);
            }
            return repo.LogException(e);
        }
    }
}
