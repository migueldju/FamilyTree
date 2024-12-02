using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree
{
    public class Persona
    {
        private int id;
        private string nombre;
        private string apellido1;
        private string apellido2;
        private DateTime fechaNacimiento;
        private string ciudadNacimiento;
        private string municipioNacimiento;
        private string registro;
        private string libro;
        private string pagina;
        private string direccionHospitalNacimiento;
        private Persona padre;
        private Persona madre;
        public List<Persona> descendientes;
        public Persona(int id, string nombre, string apellido1, string fechaNacimiento,
            string ciudadNacimiento, string municipioNacimiento, string registro, string libro, string pagina,
            string direccionHospitalNacimiento, string apellido2 = null, Persona padre = null, Persona madre = null)
        {
            bool condiciones = true;
            if (id < 001) condiciones = false;
            if (padre != null)
            {
                if (asignarPadre(padre))
                    condiciones = false;
            }
            if (madre != null)
            {
                if (!asignarMadre(madre))
                    condiciones = false;
            }
            if (condiciones)
            {

                this.id = id;
                this.nombre = nombre;
                this.apellido1 = apellido1;
                this.fechaNacimiento = DateTime.ParseExact(fechaNacimiento, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture); ;
                this.ciudadNacimiento = ciudadNacimiento;
                this.municipioNacimiento = municipioNacimiento;
                this.registro = registro;
                this.libro = libro;
                this.pagina = pagina;
                this.direccionHospitalNacimiento = direccionHospitalNacimiento;
                this.apellido2 = apellido2;
                this.padre = padre;
                this.madre = madre;
                descendientes = new List<Persona>();
            }
        }

        public int getId => id;
        public string getNombre => nombre;
        public string getApellido1 => apellido1;
        public string getApellido2 => apellido2;
        public DateTime getFechaNacimiento => fechaNacimiento; 
        public string getCiudadNacimiento => ciudadNacimiento;
        public string getMunicipioNacimiento => municipioNacimiento;
        public string getRegistro => registro; 
        public string getLibro => libro; 
        public string getPagina => pagina;
        public string getDireccionHospitalNacimiento => direccionHospitalNacimiento;
        public Persona getPadre => padre; 
        public Persona getMadre => madre;

        public string getNombrePadre()
        {
            return padre.getNombre;
        }

        public string getNombreMadre()
        {
            return madre.getNombre;
        }

        public string getApellidoPadre()
        {
            return padre.getApellido1;
        }

        public string getApellidoMadre()
        {
            return madre.getApellido1;
        }

        public bool asignarPadre(Persona padre)
        {
            if (padre == null) return false;
            if (padre.getFechaNacimiento > fechaNacimiento) return false;
            if (this.padre != null && this.padre.descendientes != null)
            {
                this.padre.descendientes.Remove(this);
            }
            this.padre = padre;
            if (padre.descendientes == null) padre.descendientes = new List<Persona>();
            padre.descendientes.Append(this);
            return true;
        }

        public bool asignarMadre(Persona madre)
        {
            if (madre == null) return false;
            if (madre.getFechaNacimiento > fechaNacimiento) return false;
            if (this.madre != null && this.madre.descendientes != null)
            {
                this.madre.descendientes.Remove(this);
            }
            this.madre = madre;
            if (madre.descendientes == null) madre.descendientes = new List<Persona>();
            madre.descendientes.Add(this);

            return true;
        }




    }
}
