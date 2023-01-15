using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BokningsSystem___Inlämning.Models
{
    public partial class HotelRoom
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; } // gör till unique?
        public int FloorNumber { get; set; }
        public int Capacity { get; set; }
        public int Cost { get; set; }
    }
}
