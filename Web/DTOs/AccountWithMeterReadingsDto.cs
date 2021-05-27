using System.Collections.Generic;

namespace Web.DTOs
{
    public class AccountWithMeterReadingsDto
    {
        public AccountDto Account { get; set; }

        public List<MeterReadingDto> MeterReadings { get; set; }
    }
}
