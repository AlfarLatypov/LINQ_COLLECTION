namespace Linq_to_Collection
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comments
    {
        [Key]
        public int CommentId { get; set; }

        public int DocumentId { get; set; }

        [Required]
        public string CommentText { get; set; }

        [Required]
        [StringLength(50)]
        public string UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        public DateTime? CommentDate { get; set; }

        public int? TypeId { get; set; }
    }
}
