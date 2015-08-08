using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBank.Models
{
    public class NewsModels
    {
        [Display(Name = "News")]
        public string NewsInfo { get; set; }
    }
}