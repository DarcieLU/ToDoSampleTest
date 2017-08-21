using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ToDoSample.Models
{
    public class ToDoContext : DbContext
    {
        public DbSet<ToDoModel> ToDoModels { get; set; }

        public ToDoContext()
            : base("name=DefaultConnection")
        {

        }
    }
}