using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class MeterReading
    {
        [Key]
        public int MeterReadingId { get; set; }
        public DateTime MeterReadingDate { get; set; }
        public int MeterReadValue { get; set; }

        public Account Account { get; set; }
    }
}
