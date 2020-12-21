namespace CommonModels.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class db_Menu
    {
        [Key]
        public int id_menu { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(50)]
        public string meta { get; set; }

        public bool? hide { get; set; }

        public int? order { get; set; }

        [Column(TypeName = "date")]
        public DateTime? datebegin { get; set; }
    }
}
