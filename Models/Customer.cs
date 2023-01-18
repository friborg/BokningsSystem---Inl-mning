using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BokningsSystem___Inlämning.Models
{
    [Index(nameof(SocialSecurityNumber), IsUnique = true)]
    public partial class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string PhoneNumber { get; set; }
    }
}
