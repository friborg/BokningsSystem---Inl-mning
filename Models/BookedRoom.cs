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
        public int HotelRoomId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual HotelRoom? HotelRoom { get; set; }
    }
}
