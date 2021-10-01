using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace Assignment4.Entities
{
    public class Tag
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public List<Task> Tasks { get; set; }
    }
}
