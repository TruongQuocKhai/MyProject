namespace CommonModels.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Social_Network
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_Social_Network { get; set; }

        [Column("Social_Network")]
        [StringLength(150)]
        public string Social_Network1 { get; set; }

        [StringLength(50)]
        public string icon { get; set; }

        public bool? hide { get; set; }

        public int? order { get; set; }
    }
}
