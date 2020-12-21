namespace CommonModels.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_Contact { get; set; }

        [StringLength(150)]
        public string address { get; set; }

        [StringLength(150)]
        public string callcenter { get; set; }

        [StringLength(150)]
        public string electronicsupport { get; set; }

        public bool? hide { get; set; }
    }
}
