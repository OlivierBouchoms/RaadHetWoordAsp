using System;

namespace Data
{
    public interface IExceptionContext
    {
        bool LogException(Exception e);
    }
}
