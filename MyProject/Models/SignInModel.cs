using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class SignInModel
    {
        [Display(Name = "Email đăng nhập")]
        [Required(ErrorMessage = "Bạn phải nhập email hoặc sđt!")]
        public string Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Bạn phải nhập mật khẩu!")]
        public string Password { get; set; }
    }
}