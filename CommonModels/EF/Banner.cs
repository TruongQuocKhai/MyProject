namespace CommonModels.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Banner")]
    public partial class Banner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_banner { get; set; }

        [StringLength(350)]
        public string mota { get; set; }

        [StringLength(150)]
        public string img { get; set; }

        public bool? hide { get; set; }

        public int? order { get; set; }
    }
}
