namespace Iteration_2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AgeGroup")]
    public partial class AgeGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Column("ageGroup")]
        [Required]
        [StringLength(59)]
        public string ageGroup1 { get; set; }
    }
}
