//--------------------------------------------------------------------------------
// <copyright file="TestExample.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------

using ClassLibrary.Publication;
using ClassLibrary.User;
using Importers;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class DataAccessTests
    {
        [Test]
        public void GenericsTest()
        {
            List<Habilitacion> habilitaciones = new List<Habilitacion> {new Habilitacion("Hab1"), new Habilitacion("Hab2")};
            Categoria categoria = new Categoria("CAT");
            Residuo residuo = new Residuo(categoria, "bla", "m/s", habilitaciones);
            Empresa empresa = new Empresa("SEMM", "Carrasco", "Medicina", "desc", "099123456");
            Publicacion p = new Publicacion(residuo, 50, "$", 10, "Buceo", empresa, "desc publicación");

            DataAccess da = DataAccess.Instancia;
            habilitaciones.ForEach(x => da.Insertar(x));
            da.Insertar(categoria);
            da.Insertar(residuo);
            da.Insertar(empresa);
            da.Insertar(p);

            Console.WriteLine(da.Obtener<Publicacion>()[0].GetTextToPrint());
            Console.WriteLine(da.Obtener<Categoria>()[0].Nombre);
            Console.WriteLine(da.Obtener<Residuo>()[0].GetTextToPrint());
            Console.WriteLine(da.Obtener<Empresa>()[0].Nombre);
            Console.WriteLine(string.Join(", ", da.Obtener<Habilitacion>().Select(x => x.Nombre)));
        }

        [Test]
        public void TestEmpresa()
        {
            Empresa empresa = new Empresa("ContruYeso", "Montevideo", "Construcción", "Empresa de construcción en yeso", "099123456");
            DataAccess dataEmpresa = DataAccess.Instancia;
            dataEmpresa.Insertar(empresa);

            Console.WriteLine(dataEmpresa.Obtener<Empresa>());
        }

        [Test]
        public void TestEmprendedor()
        {
            List<Habilitacion> habilitaciones = new List<Habilitacion>;
            Emprendedor emprendedor = new Emprendedor("El reciclador", "Canelones", "Construcción", "Acero galvanizado", habilitaciones);
            DataAccess dataEmprendedor = DataAccess.Instancia;
            dataEmprendedor.Insertar(emprendedor);

            Console.WriteLine(dataEmprendedor.Obtener<Emprendedor>());
        }
    }
}