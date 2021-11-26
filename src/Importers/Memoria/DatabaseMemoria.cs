//--------------------------------------------------------------------------------
// <copyright file="DatabaseMemoria.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Importers.Memoria
{
    /// <summary>
    /// Esta clase manejara la logica cercana al acceso a la base de datos.
    /// </summary>
    internal class DatabaseMemoria : IDatabase
    {
        /// <summary>
        /// Obtiene acceso al singleton.
        /// </summary>
        /// <value><see iref = "IDatabase"/>.</value>
        internal static IDatabase Instancia
        {
            get
            {
                if (DatabaseMemoria.instancia == null)
                {
                    DatabaseMemoria.instancia = new DatabaseMemoria();
                }

                return DatabaseMemoria.instancia;
            }
        }

        private DatabaseMemoria()
        {
            /*
            this.categorias = new List<Categoria>();
            this.publicaciones = new List<Publicacion>();
            this.publicacionesRecurrentes = new List<PublicacionRecurrente>();
            this.residuos = new List<Residuo>();
            this.ventas = new List<Venta>();
            this.datosLogin = new List<DatosLogin>();
            this.emprendedores = new List<Emprendedor>();
            this.empresas = new List<Empresa>();
            this.habilitaciones = new List<Habilitacion>();
            */
        }

        private static DatabaseMemoria instancia { get; set; }

        private Dictionary<Type, List<object>> listas { get; }
        /*
        private List<Categoria> categorias { get; }

        private List<Publicacion> publicaciones { get; }

        private List<PublicacionRecurrente> publicacionesRecurrentes { get; }

        private List<Residuo> residuos { get; }

        private List<Venta> ventas { get; }

        private List<DatosLogin> datosLogin { get; }

        private List<Emprendedor> emprendedores { get; }

        private List<Empresa> empresas { get; }

        private List<Habilitacion> habilitaciones { get; }

        private List<Invitacion> invitaciones { get; }
        */

        /// <summary>
        /// Guardar un objeto en memoria.
        /// </summary>
        /// <param name="objeto">Una instancia sin persistir.</param>
        /// <typeparam name="T">Tipo de la instancia.</typeparam>
        public void Insertar<T>(T objeto) where T : IPersistible
        {
            if (! this.ListExists<T>())
            {
                this.CreateEmptyList<T>();
            }
            this.Add(objeto);
            /*
            foreach (PropertyInfo propiedad in this.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (propiedad.PropertyType.Equals(typeof(List<T>)))
                {
                    List<T> lista = propiedad.GetValue(this) as List<T>;
                    lista.Add(objeto);
                    return;
                }
            }
            */
        }

        /// <summary>
        /// Actualiza un objeto en memoria.
        /// </summary>
        /// <param name="objetoOriginal">Una instancia ya persistida.</param>
        /// <param name="objetoModificado">La instancia a reemplazar la original.</param>
        /// <typeparam name="T">Tipo de la instancia.</typeparam>
        public void Actualizar<T>(T objetoOriginal, T objetoModificado) where T : IPersistible
        {
            if (! this.ListExists<T>())
            {
                throw new ArgumentException("No existe el almacenamiento de este objeto todavía. Persista el objeto original, y luego realice la modificación.");
            }

            int indice = this.ItemIndexInList(objetoOriginal);
            if (indice == -1)
            {
                throw new ArgumentException("No existe el almacenamiento de este objeto todavía. Persista el objeto original, y luego realice la modificación.");
            }

            this.SetItem(indice, objetoModificado);

            /*
            foreach (PropertyInfo propiedad in this.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (propiedad.PropertyType.Equals(typeof(List<T>)))
                {
                    List<T> lista = propiedad.GetValue(this) as List<T>;
                    int indice = lista.IndexOf(objetoOriginal);
                    lista[indice] = objetoModificado;
                    return;
                }
            }

            throw new Exception("Este tipo de objeto no puede ser persistido.");
            */
        }

        /// <summary>
        /// Retorna instancia/s de la base de datos.
        /// </summary>
        /// <typeparam name="T">Tipo de la Instancia.</typeparam>
        /// <returns><see langword="List T"/>.</returns>
        public List<T> Obtener<T>() where T : class, IPersistible
        {
            if (! this.ListExists<T>())
            {
                throw new ArgumentException("No existe el almacenamiento de este objeto todavía.");
            }
            
            return this.GetTypedList<T>();

            /*
            foreach (PropertyInfo propiedad in this.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (propiedad.PropertyType.Equals(typeof(List<T>)))
                {
                    return propiedad.GetValue(this) as List<T>;
                }
            }

            throw new Exception("Este tipo de objeto no puede ser persistido.");
            */
        }

        /// <summary>
        /// Borra instancias de la memoria.
        /// </summary>
        /// <param name="objeto">Instancia a persistirse.</param>
        /// <typeparam name="T">Tipo de la instancia.</typeparam>
        public void Eliminar<T>(T objeto) where T : IPersistible
        {
            if (! this.ListExists<T>())
            {
                throw new ArgumentException("No existe el almacenamiento de este objeto todavía.");
            }

            int indice = this.ItemIndexInList(objeto);
            if (indice == -1)
            {
                throw new ArgumentException("Este objeto no se encuentra persistido.");
            }

            this.DeleteItem(objeto);

            /*
            foreach (PropertyInfo propiedad in this.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (propiedad.PropertyType.Equals(typeof(List<T>)))
                {
                    List<T> lista = propiedad.GetValue(this) as List<T>;
                    lista.Remove(objeto);
                    return;
                }
            }

            throw new Exception("Este tipo de objeto no puede ser persistido.");
            */
        }

        private bool ListExists<T>() where T : IPersistible
        {
            return this.listas.ContainsKey(typeof(T));
        }

        private void CreateEmptyList<T>() where T : IPersistible
        {
            this.listas.Add(typeof(T), new List<object>());
        }

        private void Add<T>(T objeto) where T : IPersistible
        {
            this.listas[typeof(T)].Add(objeto);
        }

        private int ItemIndexInList<T>(T objeto) where T : IPersistible
        {
            return this.listas[typeof(T)].IndexOf(objeto);
        }

        private void SetItem<T>(int index, T objeto) where T : IPersistible
        {
            this.listas[typeof(T)][index] = objeto;
        }

        private List<T> GetTypedList<T>() where T : class, IPersistible
        {
            return this.listas[typeof(T)].Select(x => x as T).ToList();
        }

        private void DeleteItem<T>(T objeto) where T : IPersistible
        {
            this.listas[typeof(T)].Remove(objeto);
        }
    }
}