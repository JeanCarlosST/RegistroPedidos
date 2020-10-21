using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroPedidos.DAL;
using RegistroPedidos.Entidades;

namespace RegistroPedidos.BLL
{
    public class OrdenesBLL
    {
        public static bool Guardar(Ordenes orden)
        {
            if(!Existe(orden.OrdenId))
                return Insertar(orden); 
            else    
                return Modificar(orden);
        }

        private static bool Insertar(Ordenes orden)
        {
            Contexto context = new Contexto();
            bool found = false;

            try
            {
                context.Ordenes.Add(orden);
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

        public static bool Modificar(Ordenes orden)
        {
            Contexto context = new Contexto();
            bool found = false;

            try
            {
                context.Database.ExecuteSqlRaw($"delete from OrdenesDetalle where OrdenId = {orden.OrdenId}");
                foreach(var anterior in orden.Detalle)
                {
                    context.Entry(anterior).State = EntityState.Added;
                }

                context.Entry(orden).State = EntityState.Modified;
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
                var orden = context.Ordenes.Find(id);

                if(orden != null)
                {
                    context.Ordenes.Remove(orden);
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

        public static Ordenes Buscar(int id)
        {
            Contexto context = new Contexto();
            Ordenes orden;

            try{
                orden = context.Ordenes
                    .Include(o => o.Detalle)
                    .Where(o => o.OrdenId == id)
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

            return orden;
        }

        public static bool Existe(int id)
        {
            Contexto context = new Contexto();
            bool found = false;

            try
            {
                found = context.Ordenes.Any(p => p.OrdenId == id);
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

        public static List<Ordenes> GetList(Expression<Func<Ordenes, bool>> criterio)
        {
            List<Ordenes> list = new List<Ordenes>();
            Contexto context = new Contexto();

            try 
            {
                list = context.Ordenes.Where(criterio).AsNoTracking().ToList<Ordenes>();
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