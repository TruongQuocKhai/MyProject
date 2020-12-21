using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Areas.Admin.Models
{
    public class UserModel
    {
        [Key]
        public long ID { get; set; }

        //[Display(Name = "Tên đăng nhập")]
        //[Required(ErrorMessage = "yêu cầu nhập tên đang nhập")]
        //public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 ký tự.")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Yêu cầu nhập họ tên")]
        public string Name { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Điện thoại")]
        [Required(ErrorMessage = "Yêu cầu nhập số điện thoại")]
        public string Phone { get; set; }
        [Display(Name = "Tình trạng")]
        public bool Status { get; set; }
    }
}