using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BokningsSystem___Inlämning.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
