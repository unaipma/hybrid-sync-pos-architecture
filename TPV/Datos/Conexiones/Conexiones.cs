using System;
using System.IO;
using Npgsql;
using System.Data.SQLite;

namespace Datos.Conexiones
{
    public class Conexiones
    {
        private string connectionString;
        private readonly string RutaBD;

        public Conexiones()
        {
            // Configuración de la ruta para SQLite (Base de datos Local)
            RutaBD = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TPV.db"));

            // Configuración de PostgreSQL (Base de datos Nube)
            // BUSCAMOS LA VARIABLE DE ENTORNO PRIMERO (Seguridad Profesional)
            string envConnectionString = Environment.GetEnvironmentVariable("TPV_CONN_STRING");

            if (!string.IsNullOrEmpty(envConnectionString))
            {
                connectionString = envConnectionString;
            }
            else
            {
                // FALLBACK: Si no hay variable de entorno, usamos una cadena por defecto.
                // IMPORTANTE: DEJA ESTO CON "YOUR_PASSWORD_HERE" AL SUBIR A GITHUB.
                // Nunca escribas la contraseña real aquí si vas a hacer commit.
                connectionString = "Host=ep-still-brook-a2m8tgov-pooler.eu-central-1.aws.neon.tech;" +
                                   "Username=tfg_owner;" +
                                   "Password=YOUR_PASSWORD_HERE;" + // <--- EL RECLUTADOR VERÁ ESTO Y SABRÁ QUE ERES CUIDADOSO
                                   "Database=tfg;" +
                                   "SslMode=Require;" +
                                   "Trust Server Certificate=true";
            }
        }

        /// <summary>
        /// Abre y devuelve una conexión activa a la base de datos PostgreSQL (NeonDB).
        /// </summary>
        public NpgsqlConnection ObtenerConexion()
        {
            try
            {
                // Validación de seguridad para evitar errores tontos
                if (connectionString.Contains("YOUR_PASSWORD_HERE"))
                {
                    throw new Exception("ERROR DE SEGURIDAD: No se ha configurado la variable de entorno 'TPV_CONN_STRING' y la contraseña es un placeholder.");
                }

                var conexion = new NpgsqlConnection(connectionString);
                conexion.Open();
                return conexion;
            }
            catch (Exception ex)
            {
                // Lanzamos una excepción clara
                throw new Exception($"Error crítico al conectar con PostgreSQL: {ex.Message}");
            }
        }

        /// <summary>
        /// Abre y devuelve una conexión activa a la base de datos SQLite (local).
        /// </summary>
        public SQLiteConnection ObtenerConexionSql()
        {
            try
            {
                var conexion = new SQLiteConnection($"Data Source={RutaBD};Version=3;");
                conexion.Open();
                return conexion;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error crítico al conectar con SQLite: {ex.Message}");
            }
        }
    }
}