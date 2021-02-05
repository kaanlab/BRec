using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRec.Web.Models
{
    public class FileName
    {
        [Required ]
        [StringLength(20, ErrorMessage = "Это поле обязательно для заполнения, миннимальная длина 3, максимальная 20 символов", MinimumLength = 2)]
        public string Description { get; set; }
    }
}
