using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APINancy_APIREST
{
    public partial class User
    {
        public Guid UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
