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
    public class SuplidoresBLL
    {
        public static bool Existe(int id)
        {
            Contexto context = new Contexto();
            bool found = false;

            try
            {
                found = context.Suplidores.Any(p => p.SuplidorId == id);
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
        public static bool Guardar(Suplidores suplidor)
        {
            if (!Existe(suplidor.SuplidorId))
                return Insertar(suplidor);
            else
                return Modificar(suplidor);
        }
        private static bool Insertar(Suplidores suplidor)
        {
            Contexto context = new Contexto();
            bool found = false;

            try
            {
                context.Suplidores.Add(suplidor);
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
        public static bool Modificar(Suplidores suplidor)
        {
            Contexto context = new Contexto();
            bool found = false;

            try
            {
                context.Entry(suplidor).State = EntityState.Modified;
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
                var suplidor = context.Suplidores.Find(id);

                if (suplidor != null)
                {
                    context.Suplidores.Remove(suplidor);
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


        public static Suplidores Buscar(int id)
        {
            Contexto context = new Contexto();
            Suplidores suplidor;

            try
            {
                suplidor = context.Suplidores
                    .Where(p => p.SuplidorId == id)
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

            return suplidor;
        }

        public static List<Suplidores> GetList(Expression<Func<Suplidores, bool>> criterio)
        {
            List<Suplidores> list = new List<Suplidores>();
            Contexto contexto = new Contexto();

            try
            {
                list = contexto.Suplidores.Where(criterio).AsNoTracking().ToList();
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
