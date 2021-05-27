using System.Collections.Generic;
using WebAPI.DTOs;

namespace Web.DTOs
{
    public class AccountWithMeterReadingsDto
    {
        public AccountDto Account { get; set; }

        public List<MeterReadingDto> MeterReadings { get; set; }
    }
}
