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
        public static bool Existe(int id)
        {
            Contexto context = new Contexto();
            bool found = false;

            try
            {
                found = context.Productos.Any(p => p.ProductoId == id);

            }
            catch
            {
                throw;

            }
            finally
            {
                context.Dispose();
            }

            return found;
        }
        public static bool Guardar(Productos producto)
        {
            if (!Existe(producto.ProductoId))
                return Insertar(producto);
            else
                return Modificar(producto);
        }
        private static bool Insertar(Productos producto)
        {
            Contexto context = new Contexto();
            bool found = false;

            try
            {
                context.Productos.Add(producto);
                found = context.SaveChanges() > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                context.Dispose();
            }

            return found;
        }
        public static bool Modificar(Productos producto)
        {
            Contexto context = new Contexto();
            bool found = false;

            try
            {
                context.Entry(producto).State = EntityState.Modified;
                found = context.SaveChanges() > 0;

            }
            catch
            {
                throw;

            }
            finally
            {
                context.Dispose();
            }

            return found;
        }
        public static bool Eliminar(int id)
        {
            Contexto context = new Contexto();
            bool found = false;

            try
            {
                var producto = context.Productos.Find(id);

                if (producto != null)
                {
                    context.Productos.Remove(producto);
                    found = context.SaveChanges() > 0;
                }

            }
            catch
            {
                throw;

            }
            finally
            {
                context.Dispose();
            }

            return found;
        }


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
