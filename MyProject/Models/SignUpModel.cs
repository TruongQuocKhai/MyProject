using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class SignUpModel
    {
        [Key]
        public long ID { set; get; }

        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Vui lòng nhập họ tên!")]
        public string Name { set; get; }


        [Display(Name = "Điện thoại")]
        [Required(ErrorMessage ="Vui lòng nhập số điện thoại!")]
        public string Phone { set; get; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Vui lòng nhập email!")]
        public string Email { set; get; }

        [Display(Name = "Mật khẩu")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 ký tự!")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu!")]
        public string Password { set; get; }

        //[Display(Name = "Xác nhận mật khẩu")]
        //[Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng!")]
        //public string ConfirmPassword { set; get; }

        
    }
}