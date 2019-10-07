
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("servicios")]
    public class Servicios
    {
        [Key, Column("id_servicio")]
        public int code { get; set; }
        [Column("nombre")]
        public String name { get; set; }
        [Column("direccion")]
        public String addres { get; set; }
        /*   [Column("id_categoria_padre")]
           public int id_categoria_padre { get; set; }*/
        [Column("imagen_avatar")]
        public String avatar_image { get; set; }

    }
}