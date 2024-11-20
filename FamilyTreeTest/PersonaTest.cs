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
            result = p1.nombre;
            Assert.AreEqual("Antonio", result);
            result = p1.apellido1;
            Assert.AreEqual("Rodríguez", result);
            result = p1.apellido2;
            Assert.AreEqual("López", result);
            result = p1.fechaNacimiento;
            Assert.AreEqual(new DateTime(1975, 10, 6), result);
            result = p1.ciudadNacimiento;
            Assert.AreEqual("Burgos", result);
            result = p1.municipioNacimiento;
            Assert.AreEqual("Burgos", result);
            result = p1.registro;
            Assert.AreEqual("45", result);
            result = p1.libro;
            Assert.AreEqual("Libro8", result);
            result = p1.pagina;
            Assert.AreEqual("232", result);
            result = p1.direccionHospital;
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

        }

        [TestMethod]
        public void AsignarPadreTest()
        {
             // Asignación correcta
             Persona hijo = new Persona(003, "Jaime", "Rodríguez", "González", "12-4-2007",
                    "Burgos", "Burgos", "43", "Libro3", "12", "Avenida del Cid 96");
             Persona padre = new Persona(001, "Antonio", "Rodríguez", "López", "10-6-1975",
                    "Burgos", "Burgos", "45", "Libro8", "232", "Avenida del Cid 96");
             bool asignacion = hijo.asignarPadre(padre);
             Assert.IsTrue(asignacion);
             var result = hijo.getPadre();
             Assert.AreEqual(padre, result);

            // Persona con valor null
            Persona nula = null;
            bool asignacion = hijo.asignarPadre(nula);
            Assert.IsFalse(asignacion);
            Assert.IsNull(hijo.getPadre());






        }
}
