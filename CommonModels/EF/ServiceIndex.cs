namespace CommonModels.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ServiceIndex")]
    public partial class ServiceIndex
    {
        [Key]
        public int id_serviceindex { get; set; }

        public string Terms_Condition { get; set; }

        public string Brief_Introduction { get; set; }

        public string Special_Requirements { get; set; }

        public bool? hide { get; set; }
    }
}
