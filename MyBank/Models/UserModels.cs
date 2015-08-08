using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBank.Models
{
    public class UserModels
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        //private string authStat = "";
        //public string AuthStat
        //{
        //    get { return AuthStat; }
        //    set { AuthStat = value; }
        //}

        public string AuthStat { get; set; }
    }
}