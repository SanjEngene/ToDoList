using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoproj.ViewModels.Message
{
    public class UpdateModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [MinLength(2)]
        public string Content { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
