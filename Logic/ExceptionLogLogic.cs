using System;
using System.Collections.Generic;
using System.Text;
using Data;

namespace Logic
{
    public class ExceptionLogLogic : IDisposable
    {
        private ExceptionLogRepository repo;

        public ExceptionLogLogic(ExceptionLogRepository exceptionLogRepository)
        {
            repo = exceptionLogRepository;
        }

        public bool LogException(Exception e)
        {
            if (!repo.LogException(e))
            {
                repo = new ExceptionLogRepository(new ExceptionXMLContext());
                return repo.LogException(e);
            }

            return true;
        }

        public void Dispose()
        {
        }
    }
}
