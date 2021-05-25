using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? MeterReadingDate { get; set; }
        public int? MeterReadValue { get; set; }
    }
}
