using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RegistroPedidos.Entidades
{
    public class Ordenes
    {
        [Key]
        public int OrdenId { get; set; }
        public DateTime Fecha { get; set; }
        public int SuplidorId { get; set; }
        public decimal Monto { get; set; }

        [ForeignKey("OrdenId")]
        public virtual List<OrdenesDetalle> Detalle { get; set; }

        public Ordenes()
        {
            Fecha = DateTime.Now;
            Detalle = new List<OrdenesDetalle>();
        }
    }

    public class OrdenesDetalle
    {
        [Key]
        public int OrdenDetalleId { get; set; }
        public int OrdenId { get; set; }
        public int ProductoId { get; set; }
        public float Cantidad { get; set; }
        public decimal Costo { get; set; }

        public OrdenesDetalle(int ordenId, int productoId, float cantidad, decimal costo)
        {
            OrdenId = ordenId;
            ProductoId = productoId;
            Cantidad = cantidad;
            Costo = costo;
        }
    
    }
}
