//--------------------------------------------------------------------------------
// <copyright file="DataAccess.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System;
using System.Text;
using System.Collections.Generic;

namespace Importers 
{
    /// <summary>
    /// Clase que utilizará el bot para acceder a la base de datos.
    /// </summary>
    public class DataAccess
    {
        private IDatabase db;

        private static DataAccess instancia;
        /// <summary>
        /// Accesso al singleton.
        /// </summary>
        /// <value><see cref = "DataAccess"/></value>
        public static DataAccess Instancia 
        {
            get {
                if (DataAccess.instancia == null) {
                    DataAccess.instancia = new DataAccess(DatabaseMemoria.Instancia);
                }
                return DataAccess.instancia;
            }
        }

        private DataAccess(IDatabase db) 
        {
            this.db = db;
        }
/// <summary>
/// Almacena una nueva instancia en la base de datos.
/// </summary>
/// <param name="objeto"></param>
/// <typeparam name="T"></typeparam>
        public void Insertar<T>(T objeto) 
        {
            this.db.Insertar(objeto);
        }
/// <summary>
/// Update a un objeto ya existente en la base de datos.
/// </summary>
/// <param name="objeto"></param>
/// <typeparam name="T"></typeparam>
        public void Actualizar<T>(T objeto)
        {
            this.db.Actualizar(objeto); 
        }
/// <summary>
/// Recupera instancia/s desde la base de datos.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <returns></returns>
        public List<T> Obtener<T>() 
        {
            return this.db.Obtener<T>();
        }
/// <summary>
/// Borra elementos de la base de datos.
/// </summary>
/// <param name="objeto"></param>
/// <typeparam name="T"></typeparam>
        public void Eliminar<T>(T objeto) 
        {
            this.db.Eliminar(objeto);
        }
    }
}