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
    /// Esta clase es una implementación de IDatabase, y define todos los métodos necesarios para 
    /// implementar todas las responsabilidades definidas en la interfaz. El código cliente depende de
    /// esta forma de los métodos definidos en la interfaz, y no de los detalles de cada implementación.
    /// </summary>
    internal sealed class DatabaseMemoria : IDatabase
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
            this.listas = new Dictionary<Type, List<object>>();
        }

        private static DatabaseMemoria instancia { get; set; }

        private Dictionary<Type, List<object>> listas { get; }

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
                this.CreateEmptyList<T>();
            }
            
            return this.GetTypedList<T>();
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