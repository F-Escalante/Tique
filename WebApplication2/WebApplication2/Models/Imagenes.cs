
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("imagenes")]
    public class Imagenes 
    {

        [Key, Column("id_imagen")]
        public int code { get; set; }
        [Column("imagen")]
        public String image { get; set; }
        
    }
}