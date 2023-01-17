using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BokningsSystem___Inlämning.Models
{
    public partial class BookedRoom
    {
        public int Id { get; set; }
        public int ConferenceRoomId { get; set; }
        public int? CustomerId { get; set; }
        public int Week { get; set; }
        public int Day { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ConferenceRoom? HotelRoom { get; set; }
    }
}
