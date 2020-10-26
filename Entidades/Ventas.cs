using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RegistroPedidos.Entidades
{
    public class Ventas
    {
        [Key]
        public int VentaId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Monto { get; set; }

        [ForeignKey("VentaId")]
        public virtual List<VentasDetalle> Detalle { get; set; } = new List<VentasDetalle>();
    }

    public class VentasDetalle
    {
        [Key]
        public int VentaDetalleId { get; set; }
        public int VentaId { get; set; }
        public string Servicio { get; set; }
        public decimal Precio { get; set; }
    }
}
