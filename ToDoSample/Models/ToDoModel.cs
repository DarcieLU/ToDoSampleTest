using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoSample.Models
{
    public class ToDoModel
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
    }
}