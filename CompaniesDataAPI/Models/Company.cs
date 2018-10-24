using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompaniesDataAPI.Models
{
    public class Company
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "CompanyName"), Required(ErrorMessage = "This is a required field")]
        [MaxLength(50, ErrorMessage = "'CompanyName' length cannot exceed 50")]
        public string CompanyName { get; set; }

        [Display(Name = "Exchange"), Required(ErrorMessage = "This is a required field")]
        [MaxLength(50, ErrorMessage = "'Exchange' length cannot exceed 50")]
        public string Exchange { get; set; }

        [Display(Name = "Ticker"), Required(ErrorMessage = "This is a required field")]
        [MaxLength(10, ErrorMessage = "'Ticker' length cannot exceed 10")]
        public string Ticker { get; set; }

        [Display(Name = "ISIN"), Required(ErrorMessage = "This is a required field")]
        [MaxLength(12, ErrorMessage = "'ISIN' length cannot exceed 12")]
        public string ISIN { get; set; }

        [Display(Name = "WebsiteURL")]
        [MaxLength(50, ErrorMessage = "'Website URL' length cannot exceed 50")]
        public string WebsiteURL { get; set; }

        [Display(Name = "CreationDate")]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        [Display(Name = "UpdateTime")]
        [DataType(DataType.DateTime)]
        public DateTime UpdateTime { get; set; }
    }
}
