using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    class AuthenticationException:Exception
    {
        String _login;

        public AuthenticationException(String login)
        {
            _login = login;
        }

        public String Login
        {
            get
            {
                return _login;
            }

            set
            {
                _login = value;
            }
        }
    }

    class UserUnknownException : AuthenticationException
    {
        public UserUnknownException(String login)  : base(login)
        {
          
        }
    }

    class UserExistsException : AuthenticationException
    {
        public UserExistsException(String login) : base(login)
        {
         
        }

    }

    class WrongPasswordException  : AuthenticationException
    {
        public WrongPasswordException(String login) : base(login)
        {
        
        }

    }


}
