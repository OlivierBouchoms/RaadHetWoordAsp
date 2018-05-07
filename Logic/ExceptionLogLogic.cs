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
                try
                {
                    _repo.LogException(exception);
                }
                catch (Exception e1)
                {
                    Console.WriteLine(e);
                    Console.WriteLine(exception);
                    Console.WriteLine(e1);
                }
            }
            return _repo.LogException(e);
        }
    }
}
