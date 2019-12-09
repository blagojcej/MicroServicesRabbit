using System;
using System.Collections.Generic;
using System.Text;

namespace MicroServicesRabbit.Banking.App.ViewModels
{
    public class AccountTransfer
    {
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public decimal TransferAmount { get; set; }
    }
}
