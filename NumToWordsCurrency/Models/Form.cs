using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NumToWordsCurrency.Models
{
    public class Form
    {
        [Required(AllowEmptyStrings =false, ErrorMessage ="Input not set")]
        public string Input { get; set; }

        public string Output { get; set; }

        public string ErrorMessage { get; set; }
    }
}
