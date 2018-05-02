using System;
using Data;

namespace Logic
{
    public class ExceptionLogLogic
    {
        private ExceptionLogRepository _repo;

        public ExceptionLogLogic(ExceptionLogRepository repo)
        {
            _repo = repo;
        }

        public bool LogException(Exception e)
        {
            try
            {
                _repo.LogException(e);
            }
            catch (Exception exception)
            {
                _repo = new ExceptionLogRepository(new ExceptionXMLContext());
                _repo.LogException(exception);
            }
            return _repo.LogException(e);
        }
    }
}
