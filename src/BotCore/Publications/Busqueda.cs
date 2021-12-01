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
using BotCore.Publication.Filters;

namespace BotCore.Publication
{
    /// <summary>
    /// Un service provider, que filtra las publicaciones a partir de ciertas condiciones dadas.
    /// </summary>
    public class Busqueda
    {
        private DataAccess da;

        private Busqueda()
        {
            this.da = DataAccess.Instancia;
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
        /// Servicio principal de la busqueda
        /// </summary>
        /// <param name="cadenaFilters">Una cadena de filtros armada.</param>
        /// <returns>Una List de publicaciones que cumplen las condiciones de PublicacionesASeparar.</returns>
        public List<Publicacion> BuscarPublicaciones(IFiltroBusqueda cadenaFilters)
        {
            if (cadenaFilters == null)
            {
                List<Publicacion> todasLasPublicaciones = da.Obtener<Publicacion>();
                return todasLasPublicaciones;
            }
            List<Publicacion> publicacionesActivas = da.Obtener<Publicacion>().Concat(da.Obtener<PublicacionRecurrente>())
                                                    .Where((Publicacion p) => !p.Comprado).ToList();

            return cadenaFilters.Filtrar(publicacionesActivas);
        }
    }
}