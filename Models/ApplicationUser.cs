using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Task4_UserAuth.Models;

public class ApplicationUser : IdentityUser
{
    public DateTime LastLogin { get; set; }
    public DateTime RegistrationDate { get; set; }
}

