using System;

namespace Data
{
    public class ExceptionLogRepository
    {
        private readonly IExceptionContext _context;

        public ExceptionLogRepository(IExceptionContext context)
        {
            _context = context;
        }

        public bool LogException(Exception e)
        {
            return _context.LogException(e);
        }
    }
}
