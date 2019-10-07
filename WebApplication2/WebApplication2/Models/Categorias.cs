
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("categorias")]
    public class Categorias 
    {

        [Key, Column("id_categoria")]
        public int code { get; set; }
        [Column("nombre")]
        public String description { get; set; }
        [Column("icono")]
        public String icon { get; set; }
     /*   [Column("id_categoria_padre")]
        public int id_categoria_padre { get; set; }*/
        public bool isSon { get; set;}
    }
}