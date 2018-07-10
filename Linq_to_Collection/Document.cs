namespace Linq_to_Collection
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Document")]
    public partial class Document
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Document()
        {
            Timer = new HashSet<Timer>();
            TimerArchive = new HashSet<TimerArchive>();
            TimerInactivity = new HashSet<TimerInactivity>();
            TimerInactivityArchive = new HashSet<TimerInactivityArchive>();
        }

        public int DocumentId { get; set; }

        [StringLength(50)]
        public string DocumentNumber { get; set; }

        public DateTime? DocumentCreateDate { get; set; }

        public DateTime? DocumentCloseDate { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }

        public int? DocumentTypeId { get; set; }

        public int? CustomerId { get; set; }

        public int? ModelId { get; set; }

        public int? DepartmentId { get; set; }

        public int? ProducerId { get; set; }

        public int? StockId { get; set; }

        [StringLength(50)]
        public string MachinesSN { get; set; }

        public decimal? HoursMachines { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(50)]
        public string SmcsCode { get; set; }

        [StringLength(50)]
        public string ComponentSN { get; set; }

        public DateTime? ComponentDismantlingDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? PartsCost { get; set; }

        [Column(TypeName = "money")]
        public decimal? WorkCost { get; set; }

        [Column(TypeName = "money")]
        public decimal? ConsumablesCost { get; set; }

        public bool? ApprovedServiceEngineer { get; set; }

        public int? EngineModelId { get; set; }

        [Column(TypeName = "money")]
        public decimal? ComponentHours { get; set; }

        public int? RepairTypeId { get; set; }

        public DateTime? ArrivalMonth { get; set; }

        public DateTime? DispatchDate { get; set; }

        public int? DeliveryMethodId { get; set; }

        public bool? ToPlan { get; set; }

        public int? CabinetSE { get; set; }

        public bool? IsArrived { get; set; }

        public int? ParentId { get; set; }

        public int? StatusId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Timer> Timer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimerArchive> TimerArchive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimerInactivity> TimerInactivity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimerInactivityArchive> TimerInactivityArchive { get; set; }
    }
}
