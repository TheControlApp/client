using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlApp.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public UserAccount Account { get; set; }
    }
}
