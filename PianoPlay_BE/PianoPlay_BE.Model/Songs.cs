using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PianoPlay_BE.Model
{
    public class Songs : Auditable
    {
        [Column(TypeName = "VARCHAR")]
        [Required(ErrorMessage ="Tên là bắt buộc!")]
        public string Name { get; set; }
        public long UserId { get; set; }
        public string KeyIds { get; set; }
       
    }
}