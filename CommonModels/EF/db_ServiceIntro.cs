namespace CommonModels.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class db_ServiceIntro
    {
        [Key]
        public int id_serviceintro { get; set; }

        public string service { get; set; }

        public string intro { get; set; }

        public bool? hide { get; set; }

        [Column(TypeName = "date")]
        public DateTime? datebegin { get; set; }
    }
}
