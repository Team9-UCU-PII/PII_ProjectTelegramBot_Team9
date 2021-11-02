//--------------------------------------------------------------------------------
// <copyright file="DatabaseMemoria.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection;
using ClassLibrary.Publication;
using ClassLibrary.User;
using System;

namespace Importers 
{

    /// <summary>
    /// Esta clase manejara la logica cercana al acceso a la base de datos.
    /// </summary>
    public class DatabaseMemoria : IDatabase 
    {
        private List<Categoria> categorias {get;}
        private List<Publicacion> publicaciones {get;}
        private List<PublicacionRecurrente> publicacionesRecurrentes {get;}
        private List<Residuo> residuos {get;}
        private List<Venta> ventas {get;}
        private List<DatosLogin> datosLogin {get;}
        private List<Emprendedor> emprendedores {get;}
        private List<Empresa> empresas {get;}
        private List<Habilitacion> habilitaciones {get;}
        private static DatabaseMemoria instancia;
        
        /// <summary>
        /// Acceso al singleton.
        /// </summary>
        /// <value><see iref = "IDatabase"/></value>
        public static IDatabase Instancia 
        {
            get {
                if (DatabaseMemoria.instancia == null) 
                {
                    DatabaseMemoria.instancia = new DatabaseMemoria();
                }
                return DatabaseMemoria.instancia;
            }
        }

        private DatabaseMemoria() 
        {
            this.categorias = new List<Categoria>();
            this.publicaciones = new List<Publicacion>();
            this.publicacionesRecurrentes = new List<PublicacionRecurrente>();
            this.residuos = new List<Residuo>();
            this.ventas = new List<Venta>();
            this.datosLogin = new List<DatosLogin>();
            this.emprendedores = new List<Emprendedor>();
            this.empresas = new List<Empresa>();
            this.habilitaciones = new List<Habilitacion>();
        }

        /// <summary>
        /// Guardar un objeto en memoria.
        /// </summary>
        /// <param name="objeto"></param>
        /// <typeparam name="T"></typeparam>
        public void Insertar<T>(T objeto) 
        {
            foreach (PropertyInfo propiedad in this.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)) {
                if (propiedad.PropertyType.Equals(typeof(List<T>))) {
                    List<T> lista = propiedad.GetValue(this) as List<T>;
                    lista.Add(objeto);
                    return;
                }
            }

            throw new Exception("Este tipo de objeto no puede ser persistido.");
        }

        /// <summary>
        /// Actualiza un objeto en memoria.
        /// </summary>
        /// <param name="objetoOriginal"></param>
        /// <param name="objetoModificado"></param>
        /// <typeparam name="T"></typeparam>
        public void Actualizar<T>(T objetoOriginal, T objetoModificado) 
        {
            foreach (PropertyInfo propiedad in this.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)) {
                if (propiedad.PropertyType.Equals(typeof(List<T>))) {
                    List<T> lista = propiedad.GetValue(this) as List<T>;
                    int indice = lista.IndexOf(objetoOriginal);
                    lista[indice] = objetoModificado;
                    return;
                }
            }

            throw new Exception("Este tipo de objeto no puede ser persistido.");
        }
        
        /// <summary>
        /// Retorna instancia/s de la base de datos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns><see langword="List T"/></returns>
        public List<T> Obtener<T>() 
        {
            foreach (PropertyInfo propiedad in this.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)) {
                if (propiedad.PropertyType.Equals(typeof(List<T>))) {
                    return propiedad.GetValue(this) as List<T>;
                }
            }

            throw new Exception("Este tipo de objeto no puede ser persistido.");
        }

        /// <summary>
        /// Borra instancias de la memoria.
        /// </summary>
        /// <param name="objeto"></param>
        /// <typeparam name="T"></typeparam>
        public void Eliminar<T>(T objeto) 
        {
            foreach (PropertyInfo propiedad in this.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)) {
                if (propiedad.PropertyType.Equals(typeof(List<T>))) {
                    List<T> lista = propiedad.GetValue(this) as List<T>;
                    lista.Remove(objeto);
                    return;
                }
            }

            throw new Exception("Este tipo de objeto no puede ser persistido.");
        }
    }
}