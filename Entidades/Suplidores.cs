using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RegistroPedidos.Entidades
{
    public class Suplidores
    {
        [Key]
        public int SuplidorId { get; set; }
        public string Nombres { get; set; }

        [ForeignKey("SuplidorId")]
        public virtual List<Ordenes> Ordenes { get; set; }

        public Suplidores()
        {
            Ordenes = new List<Ordenes>();
        }

        public Suplidores(int id, string nombre)
        {
            SuplidorId = id;
            Nombres = nombre;
            Ordenes = new List<Ordenes>();
        }
    }
}
