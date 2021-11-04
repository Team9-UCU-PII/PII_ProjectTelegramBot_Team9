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
        private DataAccess da;

        private Busqueda()
        {
            this.da = DataAccess.Instancia;
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
            ///Filtro segun categoria
            Categorias
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
        public List<Publicacion> BuscarPublicaciones(Dictionary<FiltrosPosibles, object> publicacionesASeparar)
        {
            if (publicacionesASeparar == null)
            {
                throw new ArgumentNullException(nameof(publicacionesASeparar), "publicacionesASeparar es null");
            }

            List<Publicacion> result = new List<Publicacion>();
            List<Publicacion> publicacionesNoAptas = new List<Publicacion>();
            List<Publicacion> publicacionesActivas = da.Obtener<Publicacion>()
                                                    .Concat(da.Obtener<PublicacionRecurrente>())
                                                    .Where((Publicacion p) => !p.Comprado).ToList();

            foreach (var filtro in publicacionesASeparar)
            {
                foreach (Publicacion suspect in publicacionesActivas)
                {
                    // Con el transcurso del curso, nos hemos dado cuenta de que este enfoque a través
                    // de un diccionario y un switch es frágil. En retrospectiva, y con miras a implementarlo
                    // para la entrega final, consideramos que podríamos crear varias clases que implementen
                    // una interfaz común IFiltroBusqueda, de forma que cada filtro concreto implemente su
                    // acción de filtro correspondiente.
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
                            if ((int) filtro.Value == 0)
                            {
                                if (suspect is PublicacionRecurrente)
                                {
                                    publicacionesNoAptas.Add(suspect);
                                }
                            }
                            else
                            {
                                if (suspect is not PublicacionRecurrente || (suspect as PublicacionRecurrente).FrecuenciaAnualRestock != (int) filtro.Value)
                                {
                                    publicacionesNoAptas.Add(suspect);
                                }
                            }
                            break;
                        case FiltrosPosibles.Categorias:
                            if (suspect.Residuo.Categoria != filtro.Value)
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