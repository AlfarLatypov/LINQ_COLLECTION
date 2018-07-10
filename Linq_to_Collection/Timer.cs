namespace Linq_to_Collection
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Timer")]
    public partial class Timer
    {
        public int TimerId { get; set; }

        public int? UserId { get; set; }

        public int? AreaId { get; set; }

        public int? DocumentId { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateFinish { get; set; }

        public int? DurationInSeconds { get; set; }

        public virtual Area Area { get; set; }

        public virtual Document Document { get; set; }
    }
}
