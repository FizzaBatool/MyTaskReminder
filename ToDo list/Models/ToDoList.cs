using System.ComponentModel.DataAnnotations;

namespace ToDo_list.Models
{
    public class ToDoList
    {
        public int Id { get; set; } 
        [Required]
        public string Detail { get; set; }    
    }
}
