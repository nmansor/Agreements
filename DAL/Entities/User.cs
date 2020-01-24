using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;


namespace DAL.Entities
{

    // change IdentityUser Primary key to integer
    public class User: IdentityUser<int>
    {
     
    }
}
