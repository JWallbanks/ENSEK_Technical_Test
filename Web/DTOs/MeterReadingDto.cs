using System;

namespace Web.DTOs
{
    public class MeterReadingDto
    {
        public int MeterReadingId { get; set; }
        public DateTime MeterReadingDate { get; set; }
        public int MeterReadingValue { get; set; }
    }
}
