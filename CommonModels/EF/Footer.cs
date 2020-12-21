namespace CommonModels.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Footer")]
    public partial class Footer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_footer { get; set; }

        [StringLength(100)]
        public string contact { get; set; }

        [StringLength(200)]
        public string about_us { get; set; }

        public bool? hide { get; set; }
    }
}
