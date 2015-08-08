using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBank.Models
{
    public class TransferModels
    {
        [Required]
        [Display(Name = "Checking Account")]
        public string CheckingAccountNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Saving Account")]
        public string SavingAccountNumber { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Transfer Date")]
        public DateTime TransferDate { get; set; }

        [Display(Name = "Checking Balance")]
        public string CheckingBalance { get; set; }

        [Display(Name = "Saving Balance")]
        public string SavingBalance { get; set; }

        public List<TransferHistory> TransferHisList { get; set; }

        public string TransferStat { get; set; }
    }
}