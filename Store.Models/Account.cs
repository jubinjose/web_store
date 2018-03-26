using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Model
{
    public partial class Account
    {
        //Plain text password. Need this on the model, so that validator can validate password being non empty etc prior to hashing
        public string Password { get; set; }

        public Profile GetProfile()
        {
            return this.Profiles.SingleOrDefault();
        }
    }
}
