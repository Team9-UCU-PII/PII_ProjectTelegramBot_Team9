//--------------------------------------------------------------------------------
// <copyright file="Busqueda.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//
// Patrón utilizado: Singleton.
// Esta clase utiliza este patrón porque solo se necesita una instancia y almacena un estado. Además funciona como un Service Provider.
//--------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using ClassLibrary.Publication;
using Importers;

namespace BotCore.Publication
{
    /// <summary>
    /// Un service provider, que filtra las publicaciones a partir de ciertas condiciones dadas.
    /// </summary>
    public class Busqueda
    {
        private Busqueda()
        {
        }
        
        /// <summary>
        /// Los filtros competentes para las busquedas, corresponden con propiedades
        /// principales de <see cref ="Publicacion"/> y su subclase <see cref ="PublicacionRecurrente"/>.
        /// </summary>
        public enum FiltrosPosibles
        {
            /// Filtro de tipo empresa.
            Empresa,

            /// Filtro de tipo residuo.
            Residuo,

            /// Filtro de donde se debe retirar.
            LugarRetiro,

            /// Filtro del precio maximo dispuesto a pagar.
            PrecioMaximo,

            /// Filtro de de restock para publicaciones recurrentes.
            FrecuenciaRestock,
        }

        /// <summary>
        /// Da acceso al singelton de la Busqueda.
        /// </summary>
        /// <value>Instancia de Busqueda.</value>
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

        private static Busqueda instancia { get; set; }

        /// <summary>
        /// Servicio principal de la busqueda.
        /// </summary>
        /// <param name="publicacionesASeparar">Un diccionario de clave un miembro del enum de FiltrosPosibles y valor la especificacion deseada (string o int).</param>
        /// <returns>Una List de publicaciones que cumplen las condiciones de PublicacionesASeparar.</returns>
        public static List<Publicacion> BuscarPublicaciones(Dictionary<FiltrosPosibles, object> publicacionesASeparar)
        {
            if (publicacionesASeparar == null)
            {
                throw new ArgumentNullException(nameof(publicacionesASeparar), "publicacionesASeparar es null");
            }

            List<Publicacion> result = new List<Publicacion>();
            List<Publicacion> publicacionesNoAptas = new List<Publicacion>();
            List<Publicacion> publicacionesActivas = DataAccess.Instancia.Obtener<Publicacion>().Where((Publicacion p) => !p.Comprado).ToList();

            foreach (var filtro in publicacionesASeparar)
            {
                foreach (Publicacion suspect in publicacionesActivas)
                {
                    switch (filtro.Key)
                    {
                        case FiltrosPosibles.Empresa:
                            if (!suspect.Vendedor.Equals(filtro.Value))
                            {
                                publicacionesNoAptas.Add(suspect);
                            }

                            break;
                        case FiltrosPosibles.Residuo:
                            if (!suspect.Residuo.Equals(filtro.Value))
                            {
                                publicacionesNoAptas.Add(suspect);
                            }

                            break;
                        case FiltrosPosibles.LugarRetiro:
                            if (!suspect.LugarRetiro.Equals(filtro.Value))
                            {
                                publicacionesNoAptas.Add(suspect);
                            }

                            break;
                        case FiltrosPosibles.PrecioMaximo:
                            if (suspect.PrecioUnitario > (double)filtro.Value)
                            {
                                publicacionesNoAptas.Add(suspect);
                            }

                            break;
                        case FiltrosPosibles.FrecuenciaRestock:
                            if (suspect is not PublicacionRecurrente || !(suspect as PublicacionRecurrente).FrecuenciaAnualRestock.Equals(filtro.Value))
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