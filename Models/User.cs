using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        internal string Mail { get; set; }
        internal string UserName { get; }
        internal string PasswordMD5 { get; }

        public User()
        {
            UserName = Mail;
        }
    }
}
