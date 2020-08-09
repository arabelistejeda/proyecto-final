using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Net.Security;


namespace Sistema_de_Autenticacion
{
    public class Autenticar
    {
        class Program
        {
            static void Main(string[] args)
            {
                Autenticar auth = new Autenticar();
                bool seguir = true;
                //Se repetira mientras seguir sea true...
                while (seguir)
                {
                    string continuar = "Si";

                    while (continuar != "No" && continuar != "no")
                    {
                        Console.WriteLine("\t\t\t\t[---------------Sistema de Identificación---------------]\n");
                        Console.WriteLine("Ingrese el Usuario (Cedula): ");
                        string usuario = Console.ReadLine();
                        Console.WriteLine("Ingrese la Clave: ");
                        string clave = Console.ReadLine();
                        string[] result = Usuario.ValidarUsuario(usuario, clave);
                        Console.WriteLine(result[0]);
                        Console.WriteLine("Desea revisar otro usuario? (Si o No)");
                        continuar = Console.ReadLine();
                        if (continuar == "No" || continuar == "no" || result[1] == "1")
                        {
                            seguir = false;
                        }
                        else
                        {
                            Console.Clear();
                        }
                    }
                }
            }
        }
    }
    public class Usuario
    {
        public string Nombre { get; set; }
        public string username { get; set; }
        public string clave { get; set; }
        public string rol { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int estado { get; set; }
        public List<Usuario> ListadoUsuario { get; set; }

        public Usuario()
        {
            Nombre = string.Empty;
            username = string.Empty;
            clave = string.Empty;
            rol = string.Empty;
            fecha_creacion = DateTime.Now;
            estado = 0;
            ListadoUsuario = new List<Usuario>();
        }


        public static List<Usuario> CargarInformacion()
        {
            //Listado auto generado de los registros, aqui puedes modificar lo que quieras para validar los usuarios
            Usuario usuario = new Usuario();
            usuario.ListadoUsuario.Add(new Usuario() { Nombre = "juan perez", username = "323228285", clave = "282220", estado = 1, rol = "Supervisor", fecha_creacion = DateTime.Now });
            usuario.ListadoUsuario.Add(new Usuario() { Nombre = "Maria cepeza", username = "4028585", clave = "323288", estado = 1, rol = "Administrador", fecha_creacion = DateTime.Now });
            usuario.ListadoUsuario.Add(new Usuario() { Nombre = "hector fermandez", username = "40200000003", clave = "12345", estado = 0, rol = "Vendedor", fecha_creacion = DateTime.Now });
            return usuario.ListadoUsuario;

        }

        public static string[] ValidarUsuario(string username, string clave)
        {
            string estado;
            //En este metodo validas si las credenciales que manda el usuario por pantalla estan en el listado que se genera en el metodo superior CargarInformacion
            string[] result = new string[2];
            //Cargamos aqui el listado de los registros
            var listado = CargarInformacion();

            //Aqui validamos el user name para ver si esta en caso de que no exista retornara el mensaje de else mas el estado que es 0 el cual indica que no esta validado.
            //Para un usuario estar validado debe ser 1
            var auth = listado.Find(x => x.username == username);

            if (auth != null)
            {
                if (auth.clave != clave)
                {
                    result[0] = "Contraseña Incorrecta";
                    result[1] = "0";
                }

                if (auth.estado != 1)
                {
                    result[0] = "El usuario " + auth.Nombre + " esta inactivo ";
                    result[1] = "0";
                }

                if (auth.estado.Equals(1) && auth.clave.Equals(clave))
                {
                    if (auth.estado == 1)
                    {
                        estado = "Activo";
                    }
                    else
                    {
                        estado = "Inactivo";
                    }
                    result[0] = "Bienvenido " + auth.Nombre + ", ha ingresado con el numero de usuario [" + auth.username + "] con el Rol de [" + auth.rol + "] \n" + "Fecha de creación: " + auth.fecha_creacion + "\nEstado: " + estado;
                    result[1] = "1";
                }
            }
            else
            {
                result[0] = "Este usuario no existe";
                result[1] = "0";
            }

            return result;
        }
    }
} 
