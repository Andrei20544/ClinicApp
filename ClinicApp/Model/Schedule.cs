namespace ClinicApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Schedule")]
    public partial class Schedule
    {
        [Key]
        public int id_schedule { get; set; }

        public DateTime? momday { get; set; }

        public DateTime? tuesday { get; set; }

        public DateTime? wednesday { get; set; }

        public DateTime? thursday { get; set; }

        public DateTime? friday { get; set; }

        public DateTime? saturday { get; set; }

        public DateTime? sunday { get; set; }

        public int? id_doctor { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
