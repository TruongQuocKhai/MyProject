namespace CommonModels.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class db_Review
    {
        [Key]
        public int id_review { get; set; }

        [Required]
        [StringLength(50)]
        public string name_review { get; set; }

        [Required]
        [StringLength(100)]
        public string title_review { get; set; }

        [Required]
        [StringLength(50)]
        public string mail_review { get; set; }

        [Required]
        [StringLength(50)]
        public string phone_review { get; set; }

        public int id_star { get; set; }

        public string mess_review { get; set; }

        public bool? hide { get; set; }

        public int? order { get; set; }

        [Column(TypeName = "date")]
        public DateTime? datebegin { get; set; }

        public virtual Star Star { get; set; }
    }
}
