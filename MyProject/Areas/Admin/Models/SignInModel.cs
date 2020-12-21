using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Areas.Admin.Models
{
    public class SignInModel
    {
        [Display(Name = "Email đăng nhập")]
        [Required(ErrorMessage ="Bạn phải nhập email!")]
        public string Email { get; set; }

        //[Display(Name = "UserName đăng nhập")]
        //[Required(ErrorMessage = "Bạn phải nhập username!")]
        //public string UserName { get; set; }

        [Display(Name ="Mật khẩu")]
        [Required(ErrorMessage ="Nhập mật khẩu!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}