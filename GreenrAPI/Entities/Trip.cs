using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GreenrAPI.Entities
{
    public class Trip
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //distance in miles
        public int Distance { get; set; }

        public int StartLat { get; set; }
        public int StartLon { get; set; }

        public int FinishLat { get; set; }
        public int FinishLon { get; set; }


        public User User { get; set; }

    }
}
