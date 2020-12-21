namespace CommonModels.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoginAdm")]
    public partial class LoginAdm
    {
        [Key]
        [StringLength(50)]
        public string Useradm { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string HoTen { get; set; }
    }
}
