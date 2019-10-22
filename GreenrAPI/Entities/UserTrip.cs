using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GreenrAPI.Entities
{
    [Table("tblUserTrips")]
    public class UserTrip
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        public int TripId { get; set; }

        public Trip Trip { get; set; }
    }
}
