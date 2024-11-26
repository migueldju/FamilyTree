using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FamilyTreeTest
{
    [TestClass]
    public class PersonaTest
    {
        [TestMethod]
        public void TestPersona()
        {
            // Test persona sin padres asignados
            Persona p1 = new Persona(001, "Antonio", "Rodríguez", "López", "10-6-1975",
                "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
            // Comprobación de que se asignan variables
            var result = p1.id;
            Assert.AreEqual(001, result);
            result = p1.getNombre();
            Assert.AreEqual("Antonio", result);
            result = p1.getApellido1();
            Assert.AreEqual("Rodríguez", result);
            result = p1.getApellido2();
            Assert.AreEqual("López", result);
            result = p1.getFechaNacimiento();
            Assert.AreEqual(new DateTime(1975, 10, 6), result);
            result = p1.getCiudadNacimiento();
            Assert.AreEqual("Burgos", result);
            result = p1.getMunicipioNacimiento();
            Assert.AreEqual("Burgos", result);
            result = p1.getRegistro();
            Assert.AreEqual("45", result);
            result = p1.getLibro();
            Assert.AreEqual("Libro8", result);
            result = p1.getPagina();
            Assert.AreEqual("232", result);
            result = p1.getDireccionHospital();
            Assert.AreEqual("Avenida del Cid 96", result);
            result = p1.getPadre();
            Assert.IsNull(result);
            result = p1.getMadre();
            Assert.IsNull(result);
            result = p1.getDescendientes();
            Assert.IsNull(result);

            // Test persona con padres asignados
            Persona p2 = new Persona(002, "Josefina", "González", "Huertas", "3-2-1978",
                   "Burgos", "Burgos", "33", "Libro2", "222", "Avenida del Cid 96");
            Persona p3 = new Persona(003, "Jaime", "Rodríguez", "González", "12-4-2007",
                "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", p1, p2);
            result = p1.getDescendientes();
            Assert.IsNotNull(result);
            result = p1.getDescendientes()[0];
            Assert.AreEqual(p3, result);
            result = p2.getDescendientes();
            Assert.IsNotNull(result);
            result = p2.getDescendientes()[0];
            Assert.AreEqual(p3, result);

            // Test persona mayor que uno de sus padres
            Persona p4 = new Persona(004, "Jaime", "Rodríguez", "González", "12-4-1977",
                "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", p1, p2);
            Assert.IsNull(p4);

            // Test persona con id ya asignado a otra
            Persona p5 = new Persona(002, "Jaime", "Rodríguez", "González", "12-4-1977",
                "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", p1, p2);
            Assert.IsNull(p5);

            // Test persona solo con padre
            Persona p6 = new Persona(006, "Jaime", "Rodríguez", "López", "12-4-1977",
                "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", p1);
            result = p6.getPadre();
            Assert.AreEqual(p1, result);
            result = p6.getMadre();
            Assert.IsNull(result);

            // Test persona solo con madre
            Persona p7 = new Persona(007, "Jaime", "Rodríguez", "López", "12-4-1977",
                "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", p1);
            result = p6.getPadre();
            Assert.AreEqual(p1, result);
            result = p6.getMadre();
            Assert.IsNull(result);

            // Test persona sin segundo apellido
            Persona p8 = new Persona(008, "Michael", "Smith", "13-5-1988",
                "London", "Camden Town", "234", "12", "23", "Health St. 23");
            Assert.IsNotNull(p8);
            result = p8.apellido2;
            Assert.IsNull(result);

            // Test persona añadiendo padres


        }

        [TestMethod]
        public void AsignarPadreTest()
        {
            // Asignación correcta
            Persona hijo = new Persona(011, "Jaime", "Rodríguez", "González", "12-4-2007",
                    "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96");
            Persona padre = new Persona(012, "Antonio", "Rodríguez", "López", "10-6-1975",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
            asignacion = hijo.asignarPadre(padre);
            Assert.IsTrue(asignacion);
            var result = hijo.getPadre();
            Assert.AreEqual(padre, result);
            Assert.IsTrue(padre.GetDescendientes.Contains(hijo));

            // Persona con valor null
            Persona hijo = new Persona(013, "Jaime", "Rodríguez", "González", "12-4-2007",
                    "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96");
            Persona nula = null;
            asignacion = hijo.asignarPadre(nula);
            Assert.IsFalse(asignacion);
            Assert.IsNull(hijo.getPadre());
            Assert.IsFalse(padre.GetDescendientes.Contains(hijo));

            // Asignación de padre menor que el hijo
            Persona padreMenor = new Persona(014, "Antonio", "Rodríguez", "López", "10-6-2012",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
            asignacion = hijo.asignarPadre(padreMenor);
            Assert.IsFalse (asignacion);
            Assert.IsNull (hijo.getPadre());
            Assert.IsFalse(padre.GetDescendientes.Contains(hijo));

            // Asignación de padre demasiado joven
            Persona padreJoven = new Persona(015, "Antonio", "Rodríguez", "López", "10-6-2005",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
            asignacion = hijo.asignarPadre(padreJoven);
            Assert.IsFalse(asignacion);
            Assert.IsNull(hijo.getPadre());
            Assert.IsFalse(padre.GetDescendientes.Contains(hijo));
        }

        [TestMethod]
        public void AsignarMadreTest()
        {
            // Asignación correcta
            Persona hijo = new Persona(021, "Jaime", "Rodríguez", "González", "12-4-2007",
                   "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96");
            Persona madre = new Persona(022, "Manuela", "Álvarez", "Villaconejos", "10-6-1978",
                   "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
            bool asignacion = hijo.asignarMadre(madre);
            Assert.IsTrue(asignacion);
            var result = hijo.getMadre();
            Assert.AreEqual(madre, result);
            Assert.IsTrue(madre.GetDescendientes.Contains(hijo));

            // Persona con valor null
            Persona hijo = new Persona(023, "Jaime", "Rodríguez", "González", "12-4-2007",
                    "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96");
            Persona nula = null;
            bool asignacion = hijo.asignarMadre(nula);
            Assert.IsFalse(asignacion);
            Assert.IsNull(hijo.getMadre());
            Assert.IsFalse(madre.GetDescendientes.Contains(hijo));

            // Asignación de madre menor que el hijo
            Persona madreMenor = new Persona(024, "Lucrecia", "Pérez", "Puertas", "10-6-2012",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
            asignacion = hijo.asignarMadre(madreMenor);
            Assert.IsFalse(asignacion);
            Assert.IsNull(hijo.getPadre());
            Assert.IsFalse(madre.GetDescendientes.Contains(hijo));

            // Asignación de padre demasiado joven
            Persona madreJoven = new Persona(025, "Lucrecia", "Pérez", "Puertas", "10-6-2005",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
            bool asignacion = hijo.asignarMadre(madreJoven);
            Assert.IsFalse(asignacion);
            Assert.IsNull(hijo.getMadre());
            Assert.IsFalse(madre.GetDescendientes.Contains(hijo));
        }

        [TestMethod]
        public void GetPadreTest()
        {
            // Persona sin padre
            Persona hijo = new Persona(031, "Jaime", "Rodríguez", "González", "12-4-2007",
                  "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96");
            bool result = hijo.getPadre();
            Assert.IsNull(result);

            // Padre nulo
            hijo.asignarPadre(null);
            bool result2 = hijo.getPadre();
            Assert.IsNull(result2);

            // Padre menor
            hijo.asignarPadre(new Persona(032, "Antonio", "Rodríguez", "López", "10-6-2012",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
            result = hijo.getPadre();
            Assert.IsNull(result);

            // Padre demasiado joven
            hijo.asignarPadre(new Persona(033, "Antonio", "Rodríguez", "López", "10-6-2005",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
            result = hijo.getPadre();
            Assert.IsNull(result);

            // Padre correcto  - asignación
            Persona padreCorrecto = new Persona(034, "Antonio", "Rodríguez", "López", "10-6-1975",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
            hijo.asignarPadre(padreCorrecto);
            result = hijo.getPadre();
            Assert.AreEqual(padreCorrecto, result);

            // Padre correcto - constructor
            Persona hijo = new Persona(035, "Jaime", "Rodríguez", "González", "12-4-2007",
                  "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", padreCorrecto);
            bool result = hijo.getPadre();
            Assert.AreEqual(padreCorrecto, result);
        }

        [TestMethod]
        public void GetMadreTest()
        {
            // Persona sin madre
            Persona hijo = new Persona(041, "Jaime", "Rodríguez", "González", "12-4-2007",
                  "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96");
            bool result = hijo.getMadre();
            Assert.IsNull(result);

            // Madre nulo
            hijo.asignarMadre(null);
            bool result2 = hijo.getMadre();
            Assert.IsNull(result2);

            // Madre menor
            hijo.asignarMadre(new Persona(042, "María", "Gómez", "López", "10-6-2012",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
            result = hijo.getMadre();
            Assert.IsNull(result);

            // Madre demasiado joven
            hijo.asignarMadre(new Persona(043, "María", "Gargamel", "Cardeña", "10-6-2005",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
            result = hijo.getMadre();
            Assert.IsNull(result);

            // Madre correcta  - asignación
            Persona madreCorrecta = new Persona(044, "Ángela", "Prima", "Renta", "10-6-1975",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
            hijo.asignarPadre(madreCorrecta);
            result = hijo.getMadre();
            Assert.AreEqual(madreCorrecta, result);

            // Madre correcta - constructor
            Persona hijo = new Persona(045, "Sergio", "Maestro", "Justicia", "12-4-2007",
                  "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", madreCorrecta);
            bool result = hijo.getMadre();
            Assert.AreEqual(madreCorrecta, result);
        }

        [TestMethod]
        public void getDescendientesTest()
        {
            // Persona sin descendientes
            Persona base = new Persona(051, "Antonio", "Rodríguez", "López", "10-6-1975",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
            bool result = base.getDescendientes();
            Assert.IsNull (result);

            // Dos hijos correctos
            Persona hijo1 = new Persona(052, "María", "Gómez", "López", "10-6-2012",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
            hijo1.asignarPadre(base);
            Persona hijo2 = new Persona(053, "María", "Gargamel", "Cardeña", "10-6-2005",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
            hijo2.asignarPadre(base);

            // Hijo de su misma edad
            Persona hijo3 = new Persona(054, "Ángela", "Prima", "Renta", "10-6-1975",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
            hijo3.asignarPadre(base);

            // Hijo correcto desde el constructor
            Persona hijo4 = new Persona(055, "María", "Gargamel", "Cardeña", "10-6-2005",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96", base, hijo4);

            // Hijo incorrecto desde el constructor
            Persona hijo5 = new Persona(056, "María", "Gargamel", "Cardeña", "10-6-1965",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96", base, hijo4);

            Assert.IsTrue(base.getDescendientes().Contains(hijo1));
            Assert.IsTrue(base.getDescendientes().Contains(hijo2));
            Assert.IsFalse(base.getDescendientes().Contains(hijo3));
            Assert.IsTrue(base.getDescendientes().Contains(hijo4));
            Assert.IsFalse(base.getDescendientes().Contains(hijo5));
        }

        [TestMethod]
        public void getNombrePadre()
        {
            Persona hijo = new Persona(041, "Jaime", "Rodríguez", "González", "12-4-2007",
                  "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96");
            Assert.IsNull(hijo.getNombrePadre());

            Persona padre = new Persona(034, "Antonio", "Rodríguez", "López", "10-6-1975",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
            hijo.asignarPadre(padre);
            Assert.AreEqual(hijo.getNombrePadre(), padre.getNombre());
        }

        [TestMethod]
        public void getApellidoPadre()
        {
            Persona hijo = new Persona(041, "Jaime", "Rodríguez", "González", "12-4-2007",
                  "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96");
            Assert.IsNull(hijo.getApellidoPadre());

            Persona padre = new Persona(034, "Antonio", "Rodríguez", "López", "10-6-1975",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
            hijo.asignarPadre(padre);
            Assert.AreEqual(hijo.getApellidoPadre(), padre.getApellido1());
        }

        [TestMethod]
        public void getNombreMadre()
        {
            Persona hijo = new Persona(041, "Jaime", "Rodríguez", "González", "12-4-2007",
                  "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96");
            Assert.IsNull(hijo.getNombreMadre());

            Persona madre = new Persona(034, "Paula", "Rodas", "Luis", "10-6-1975",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
            hijo.asignarMadre(madre);
            Assert.AreEqual(hijo.getNombreMadre(), madre.getNombre());
        }

        [TestMethod]
        public void getApellidoMadre()
        {
            Persona hijo = new Persona(041, "Jaime", "Rodríguez", "González", "12-4-2007",
                  "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96");
            Assert.IsNull(hijo.getApellidoMadre());

            Persona madre = new Persona(034, "Antonio", "Rodríguez", "López", "10-6-1975",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
            hijo.asignarPadre(madre);
            Assert.AreEqual(hijo.getApellidoMadre(), madre.getApellido1());
        }
    }
