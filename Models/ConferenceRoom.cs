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
    [Index(nameof(Name), IsUnique = true)]
    public partial class ConferenceRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public bool WhiteBoard { get; set; }
        public bool Projector { get; set; }
    }
}
