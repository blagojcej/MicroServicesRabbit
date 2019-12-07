using System;
using System.Collections.Generic;
using System.Text;

namespace MicroServicesRabbit.Banking.Domain.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountType { get; set; }
        public decimal AcoountBalance { get; set; }
    }
}
