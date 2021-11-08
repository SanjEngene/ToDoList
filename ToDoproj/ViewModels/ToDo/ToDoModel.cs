using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoproj.ViewModels.ToDo
{
    public class ToDoModel
    {
        [Required(ErrorMessage = "Should be filled")]
        [StringLength(9, ErrorMessage = "String length are not in needed range")]
        [DataType(DataType.Text)]
        public string DayOfWeek { get; set; }
        [Required(ErrorMessage = "Should be filled")]
        [DataType(DataType.Text)]
        public string Header { get; set; }
        [Required(ErrorMessage = "Should be filled")]
        [DataType(DataType.Text)]
        public string Content { get; set; }
        public int UserId { get; set; }
    }
}
