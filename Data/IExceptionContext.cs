using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IExceptionContext
    {
        bool LogException(Exception e);
    }
}
