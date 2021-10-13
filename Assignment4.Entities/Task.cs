using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Assignment4.Core;

namespace Assignment4.Entities {
    public class Task {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public User AssignedTo { get; set; }

        public string Description { get; set; }

        [Required]
        //[Column(TypeName = "nvarchar(10)")]
        public State State { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}