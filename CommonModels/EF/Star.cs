namespace CommonModels.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Star")]
    public partial class Star
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Star()
        {
            db_Review = new HashSet<db_Review>();
        }

        [Key]
        public int id_star { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string StarReview { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<db_Review> db_Review { get; set; }
    }
}
