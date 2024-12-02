using FamilyTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FamilyTreeTest
{
    [TestClass]
    public class PersonaTest
    {
        [TestMethod]
        public void TestPersona()
        {
            // Test persona sin padres asignados
            Persona p1 = new Persona(001, "Antonio", "Rodríguez", "10-06-1975",
                "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96", "López");

            // Comprobación de que se asignan variables
            int id = p1.getId;
            Assert.AreEqual(001, id);

            string result = p1.getNombre;
            Assert.AreEqual("Antonio", result);

            result = p1.getApellido1;
            Assert.AreEqual("Rodríguez", result);

            result = p1.getApellido2;
            Assert.AreEqual("López", result);

            DateTime fecha = p1.getFechaNacimiento;
            Assert.AreEqual(new DateTime(1975, 6, 10), fecha);

            result = p1.getCiudadNacimiento;
            Assert.AreEqual("Burgos", result);

            result = p1.getMunicipioNacimiento;
            Assert.AreEqual("Burgos", result);

            result = p1.getRegistro;
            Assert.AreEqual("45", result);

            result = p1.getLibro;
            Assert.AreEqual("Libro8", result);

            result = p1.getPagina;
            Assert.AreEqual("232", result);

            result = p1.getDireccionHospitalNacimiento;
            Assert.AreEqual("Avenida del Cid 96", result);

            Persona padre = p1.getPadre;
            Assert.IsNull(padre);

            Persona madre = p1.getMadre;
            Assert.IsNull(madre);

            List<Persona> desc = p1.descendientes;
            Assert.AreEqual(0, desc.Count);

            // Test persona con padres asignados
            //padre
            Persona p2 = new Persona(002, "Josefina", "González", "03-02-1978",
                   "Burgos", "Burgos", "33", "Libro2", "222", "Avenida del Cid 96");
            //hijo
            Persona p3 = new Persona(003, "Jaime", "Rodríguez", "12-04-2017",
                "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", "Huertas", p2);
            desc = p2.descendientes;
            Assert.AreEqual(1, desc.Count);

            Persona personaPrueba = p2.descendientes[0];
            Assert.AreEqual(p3, personaPrueba);

            // Test persona mayor que uno de sus padres
            Persona p4 = new Persona(004, "Jaime", "González", "12-04-1977",
                "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", "Rodríguez", p1, p2);
            Assert.IsNull(p4.getNombre);

            // Test persona con id ya asignado a otra
            Persona p5 = new Persona(002, "Jaime", "Rodríguez", "12-04-1977",
                "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", "González", p1, p2);
            Assert.IsNull(p5.getNombre);

            // Test persona solo con padre
            Persona p6 = new Persona(006, "Jaime", "Rodríguez", "12-04-1977",
                "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", "López", p1);
            personaPrueba = p6.getMadre;
            Assert.IsNull(personaPrueba);

            // Test persona solo con madre
            Persona p7 = new Persona(007, "Jaime", "Rodríguez", "12-04-1977",
                "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", "López", null, p2);
            personaPrueba = p7.getPadre;
            Assert.IsNull(personaPrueba);

            // Test persona sin segundo apellido
            Persona p8 = new Persona(008, "Michael", "Smith", "13-05-1988",
                "London", "Camden Town", "234", "12", "23", "Health St. 23");
            Assert.IsNotNull(p8);
            result = p8.getApellido2;
            Assert.IsNull(result);
        }


        [TestMethod]
        public void AsignarPadreTest()
        {
            // Asignación correcta
            Persona padre = new Persona(012, "Antonio", "Rodríguez", "10-06-1975",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96", "López");
            Persona hijo = new Persona(011, "Jaime", "Rodríguez", "12-04-2007",
                    "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", "González", padre);
            var result = hijo.getPadre;
            Assert.AreEqual(padre, result);
            Assert.IsTrue(padre.descendientes.Contains(hijo));

            // Persona con valor null
            hijo = new Persona(013, "Jaime", "Rodríguez", "12-04-2007",
                    "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", "González");
            Persona nula = null;
            bool asignacion = hijo.asignarPadre(nula, "12-04-2007");
            Assert.IsFalse(asignacion);
            Assert.IsNull(hijo.getPadre);
            Assert.IsFalse(padre.descendientes.Contains(hijo));

            // Asignación de padre menor que el hijo
            Persona padreMenor = new Persona(014, "Antonio", "Rodríguez", "10-06-2012",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96", "López");
            asignacion = hijo.asignarPadre(padreMenor, "12-04-2007");
            Assert.IsFalse(asignacion);
            Assert.IsNull(hijo.getPadre);
            Assert.IsFalse(padre.descendientes.Contains(hijo));
        }

        [TestMethod]
        public void AsignarMadreTest()
        {
            // Asignación correcta
            Persona madre = new Persona(022, "Manuela", "Álvarez", "10-06-1978",
                   "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96", "Villaconejos");
            Persona hijo = new Persona(021, "Jaime", "Rodríguez", "12-04-2007",
                   "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", "González", null, madre);
            var result = hijo.getMadre;
            Assert.AreEqual(madre, result);
            Assert.IsTrue(madre.descendientes.Contains(hijo));

            // Persona con valor null
            hijo = new Persona(023, "Jaime", "Rodríguez", "12-04-2007",
                    "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", "González");
            Persona nula = null;
            bool asignacion = hijo.asignarMadre(nula, "12-04-2007");
            Assert.IsFalse(asignacion);
            Assert.IsNull(hijo.getMadre);
            Assert.IsFalse(madre.descendientes.Contains(hijo));

            // Asignación de madre menor que el hijo
            Persona madreMenor = new Persona(024, "Lucrecia", "Pérez", "10-06-2012",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96", "Puertas");
            asignacion = hijo.asignarMadre(madreMenor, "12-04-2007");
            Assert.IsFalse(asignacion);
            Assert.IsNull(hijo.getPadre);
            Assert.IsFalse(madre.descendientes.Contains(hijo));

        }

        [TestMethod]
        public void GetPadreTest()
        {
            // Persona sin padre
            Persona hijo = new Persona(031, "Jaime", "Rodríguez", "12-04-2007",
                  "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", "González");
            Persona result = hijo.getPadre;
            Assert.IsNull(result);

            // Padre nulo
            hijo.asignarPadre(null, "12-04-2007");
            Persona result2 = hijo.getPadre;
            Assert.IsNull(result2);

            // Padre menor
            hijo.asignarPadre(new Persona(032, "Antonio", "Rodríguez", "10-06-2012",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96", "López"), "12-04-2007");
            result = hijo.getPadre;
            Assert.IsNull(result);


            // Padre correcto  - asignación
            Persona padreCorrecto = new Persona(034, "Antonio", "Rodríguez", "10-06-1975",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96", "López");
            hijo.asignarPadre(padreCorrecto, "12-04-2007");
            result = hijo.getPadre;
            Assert.AreEqual(padreCorrecto, result);

            // Padre correcto - constructor
            hijo = new Persona(035, "Jaime", "Rodríguez", "12-04-2007",
                  "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", "González", padreCorrecto);
            result = hijo.getPadre;
            Assert.AreEqual(padreCorrecto, result);
        }

        [TestMethod]
        public void GetMadreTest()
        {
            // Persona sin madre
            Persona hijo = new Persona(041, "Jaime", "Rodríguez", "12-04-2007",
                  "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", "González");
            Persona result = hijo.getMadre;
            Assert.IsNull(result);

            // Madre nulo
            hijo.asignarMadre(null, "12-04-2007");
            Persona result2 = hijo.getMadre;
            Assert.IsNull(result2);

            // Madre menor
            hijo.asignarMadre(new Persona(042, "María", "Gómez", "10-06-2012",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96", "López"), "12-04-2007");
            result = hijo.getMadre;
            Assert.IsNull(result);

            // Madre correcta  - asignación
            Persona madreCorrecta = new Persona(044, "Ángela", "Prima", "10-06-1975",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96", "Renta");
            hijo.asignarMadre(madreCorrecta, "12-04-2007");
            result = hijo.getMadre;
            Assert.AreEqual(madreCorrecta, result);

            // Madre correcta - constructor
            hijo = new Persona(045, "Sergio", "Maestro", "12-04-2007",
                  "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", "Justicia", null, madreCorrecta);
            result = hijo.getMadre;
            Assert.AreEqual(madreCorrecta, result);
        }

        [TestMethod]
        public void getDescendientesTest()
        {
            // Persona sin descendientes
            Persona personaPrueba = new Persona(051, "Antonio", "Rodríguez", "10-06-1975",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96", "López");

            // Dos hijos correctos
            Persona hijo1 = new Persona(052, "María", "Gómez", "10-06-2012",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96", "López");
            hijo1.asignarPadre(personaPrueba, "10-06-2012");
            Persona hijo2 = new Persona(053, "María", "Gargamel", "10-06-2005",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96", "Cardeña");
            hijo2.asignarPadre(personaPrueba, "10-06-2005");

            // Hijo de su misma edad
            Persona hijo3 = new Persona(054, "Ángela", "Prima", "10-06-1975",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96", "Renta");
            hijo3.asignarPadre(personaPrueba, "10-06-1975");

            // Hijo correcto desde el constructor
            Persona hijo4 = new Persona(055, "María", "Gargamel", "10-06-2005",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96", "Cardeña", personaPrueba, hijo3);

            // Hijo incorrecto desde el constructor
            Persona hijo5 = new Persona(056, "María", "Gargamel", "10-06-1965",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96", "Cardeña", personaPrueba, hijo4);

            Assert.IsTrue(personaPrueba.descendientes.Contains(hijo1));
            Assert.IsTrue(personaPrueba.descendientes.Contains(hijo2));
            Assert.IsFalse(personaPrueba.descendientes.Contains(hijo3));
            Assert.IsTrue(personaPrueba.descendientes.Contains(hijo4));
            Assert.IsFalse(personaPrueba.descendientes.Contains(hijo5));
        }

        [TestMethod]
        public void getNombrePadre()
        {
            Persona hijo = new Persona(041, "Jaime", "Rodríguez", "12-04-2007",
                  "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", "González");
            Assert.IsNull(hijo.getNombrePadre());

            Persona padre = new Persona(034, "Antonio", "Rodríguez", "10-06-1975",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96", "López");
            hijo.asignarPadre(padre, "12-04-2007");
            Assert.AreEqual(hijo.getNombrePadre(), padre.getNombre);
        }

        [TestMethod]
        public void getApellidoPadre()
        {
            Persona hijo = new Persona(041, "Jaime", "Rodríguez", "12-04-2007",
                  "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", "González");
            Assert.IsNull(hijo.getApellidoPadre());

            Persona padre = new Persona(034, "Antonio", "Rodríguez", "10-06-1975",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96", "López");
            hijo.asignarPadre(padre, "12-04-2007");
            Assert.AreEqual(hijo.getApellidoPadre(), padre.getApellido1);
        }

        [TestMethod]
        public void getNombreMadre()
        {
            Persona hijo = new Persona(041, "Jaime", "Rodríguez", "12-04-2007",
                  "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", "González");
            Assert.IsNull(hijo.getNombreMadre());

            Persona madre = new Persona(034, "Paula", "Rodas", "10-06-1975",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96", "Luis");
            hijo.asignarMadre(madre, "12-04-2007");
            Assert.AreEqual(hijo.getNombreMadre(), madre.getNombre);
        }

        [TestMethod]
        public void getApellidoMadre()
        {
            Persona hijo = new Persona(041, "Jaime", "Rodríguez", "12-04-2007",
                  "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96", "González");
            Assert.IsNull(hijo.getApellidoMadre());

            Persona madre = new Persona(034, "Antonio", "Rodríguez", "10-06-1975",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96", "López");
            hijo.asignarMadre(madre, "12-04-2007");
            Assert.AreEqual(hijo.getApellidoMadre(), madre.getApellido1);
        }
    }
}
