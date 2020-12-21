namespace CommonModels.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Logo")]
    public partial class Logo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_logo { get; set; }

        [StringLength(150)]
        public string img { get; set; }

        public bool? hide { get; set; }
    }
}
