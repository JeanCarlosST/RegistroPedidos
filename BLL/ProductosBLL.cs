using Microsoft.EntityFrameworkCore;
using RegistroPedidos.DAL;
using RegistroPedidos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace RegistroPedidos.BLL
{
    public class ProductosBLL
    {
        public static Productos Buscar(int id)
        {
            Contexto context = new Contexto();
            Productos producto;

            try
            {
                producto = context.Productos
                    .Where(p => p.ProductoId == id)
                    .SingleOrDefault();
            }
            catch
            {
                throw;
            }
            finally
            {
                context.Dispose();
            }

            return producto;
        }

        public static List<Productos> GetList(Expression<Func<Productos, bool>> criterio)
        {
            List<Productos> list = new List<Productos>();
            Contexto contexto = new Contexto();

            try
            {
                list = contexto.Productos.Where(criterio).AsNoTracking().ToList<Productos>();
            }
            catch
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return list;
        }
    }
}
