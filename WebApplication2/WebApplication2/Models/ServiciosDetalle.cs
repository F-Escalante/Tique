
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("servicios")]
    public class ServiciosDetalle : Servicios
    {
        

        public String account_image { get; set; }

        public List<Imagenes> atached_image { get; set; }

        public List<Comentarios> comments { get; set; }


    }
}