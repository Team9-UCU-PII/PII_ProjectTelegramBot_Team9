//--------------------------------------------------------------------------------
// <copyright file="Busqueda.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using System.Collections.Generic;
using Importers;

namespace BotCore.Publication
{
    /// <summary>
    /// Un service provider, que filtra las publicaciones a partir de ciertas condiciones dadas.
    /// </summary>
    public class Busqueda
    {
        private static Busqueda instancia{get;set;}
        /// <summary>
        /// Da acceso al singelton de la Busqueda.
        /// </summary>
        /// <value>Instancia de Busqueda</value>
        public static Busqueda Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Busqueda();
                }
                return instancia;
            }
        }

        private Busqueda(){}
/// <summary>
/// Los filtros competentes para las busquedas, corresponden con propiedades
/// principales de <see cref ="Publicacion"/> y su subclase <see cref ="PublicacionRecurrente"/>.
/// </summary>
        public enum FiltrosPosibles
        {
            ///FIltro de tipo empresa.
            Empresa,
            ///FIltro de tipo residuo.
            Residuo,
            ///FIltro de donde se debe retirar.
            LugarRetiro,
            ///FIltro del precio maximo dispuesto a pagar.
            PrecioMaximo,
            ///FIltro de de restock para publicaciones recurrentes.
            FrecuenciaRestock
        }
        /// <summary>
        /// Servicio principal de la busqueda.
        /// </summary>
        /// <param name="PublicacionesASeparar">Un diccionario de clave un miembro del enum de FiltrosPosibles y valor la especificacion deseada (string o int)</param>
        /// <returns>Una List de publicaciones que cumplen las condiciones de PublicacionesASeparar.</returns>
        public List<Publicacion> BuscarPublicaciones(Dictionary<FiltrosPosibles, object> PublicacionesASeparar)
        {
            List<Publicacion> result = new List<Publicacion>();
            List<Publicacion> publicacionesNoAptas = new List<Publicacion>();
            List<Publicacion> publicacionesActivas = new List<Publicacion>(DataAccess.Instancia.Obtener<Publicacion>());
            
            foreach (var Filtro in PublicacionesASeparar)
            {
                foreach (Publicacion suspect in publicacionesActivas)
                {
                    switch (Filtro.Key)
                    {
                        case FiltrosPosibles.Empresa:
                            if (suspect.Vendedor.Equals(Filtro.Key))
                            {
                                publicacionesNoAptas.Add(suspect);
                            }
                            break;
                        case FiltrosPosibles.Residuo:
                            if (suspect.Residuo.Equals(Filtro.Key))
                            {
                                publicacionesNoAptas.Add(suspect);
                            }
                            break;
                        case FiltrosPosibles.LugarRetiro:
                            if (suspect.LugarRetiro.Equals(Filtro.Key))
                            {
                                publicacionesNoAptas.Add(suspect);
                            }
                            break;
                        case FiltrosPosibles.PrecioMaximo:
                            if (suspect.Precio.Equals(Filtro.Key))
                            {
                                publicacionesNoAptas.Add(suspect);
                            }
                            break;
                        case FiltrosPosibles.FrecuenciaRestock:
                            if (suspect is not PublicacionRecurrente && (suspect as PublicacionRecurrente).FrecuenciaAnualRestock.Equals(Filtro.Key))
                            {
                                publicacionesNoAptas.Add(suspect);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            foreach (Publicacion publicacion in publicacionesActivas)
            {
                if (!publicacionesNoAptas.Contains(publicacion))
                {
                    result.Add(publicacion);
                }
            }
            return result;
        }
    }
}