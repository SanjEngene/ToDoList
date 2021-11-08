using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoproj.ViewModels.Home
{
    public class ImageUploadModel
    {
        [Required(ErrorMessage = "UserId doesn't indicated")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "You don't select file")]
        public IFormFile File { get; set; }
    }
}
