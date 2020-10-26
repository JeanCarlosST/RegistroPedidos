using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroPedidos.DAL;
using RegistroPedidos.Entidades;

namespace RegistroPedidos.BLL
{
    public class VentasBLL
    {
        public static bool Guardar(Ventas venta)
        {
            if(!Existe(venta.VentaId))
                return Insertar(venta); 
            else    
                return Modificar(venta);
        }

        private static bool Insertar(Ventas venta)
        {
            Contexto context = new Contexto();
            bool found = false;

            try
            {
                context.Ventas.Add(venta);
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

        public static bool Modificar(Ventas venta)
        {
            Contexto context = new Contexto();
            bool found = false;

            try
            {
                context.Database.ExecuteSqlRaw($"delete from VentasDetalle where VentaId = {venta.VentaId}");
                foreach(var anterior in venta.Detalle)
                {
                    context.Entry(anterior).State = EntityState.Added;
                }

                context.Entry(venta).State = EntityState.Modified;
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
                Ventas venta = Buscar(id);

                if (venta != null)
                {
                    context.Ventas.Remove(venta);
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

        public static Ventas Buscar(int id)
        {
            Contexto context = new Contexto();
            Ventas venta;

            try{
                venta = context.Ventas
                    .Include(o => o.Detalle)
                    .Where(o => o.VentaId == id)
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

            return venta;
        }

        public static bool Existe(int id)
        {
            Contexto context = new Contexto();
            bool found = false;

            try
            {
                found = context.Ventas.Any(p => p.VentaId == id);
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

        public static List<Ventas> GetList(Expression<Func<Ventas, bool>> criterio)
        {
            List<Ventas> list = new List<Ventas>();
            Contexto context = new Contexto();

            try 
            {
                list = context.Ventas.Where(criterio).AsNoTracking().ToList<Ventas>();
            } 
            catch  
            {
                throw;
            } 
            finally 
            {
                context.Dispose();
            }

            return list;
        }
    }
}