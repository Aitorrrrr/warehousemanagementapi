using SSMReservas.Library;
using SSMWarehouseManagement.Data.Models;
using SSMWarehouseManagement.Helpers;
using System;
using System.Data.SqlClient;

namespace SSMWarehouseManagement.Repositories
{
    public class AuthenticationRepository
    {
        Conexion Conexion;
        Conexion.Parametros Parametros;

        public AuthenticationRepository()
        {
            this.Conexion = new Conexion();
            this.Parametros = new Conexion.Parametros("Aqua");

            this.Conexion.LeerConnect(ref Parametros);
        }

        public Usuario AutenticarUsuario(string user, string pw)
        {
            Usuario devuelto = new Usuario();

            try
            {
                using (SqlConnection connection = new SqlConnection(this.Conexion.ConnectionString(Parametros, Parametros.Data)))
                {
                    connection.Open();
                    string sql = "Select CODE From CMNENV " +
                                " Where PWDPDA = dbo.ssm_cryASC_2015('" + pw.ToUpper() + "','" + user.ToUpper() + "') " +
                                " And CODE = '" + user.ToUpper() + "'";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        devuelto.User = reader.GetString(0).Trim();
                    }
                    reader.DisposeAsync();

                    connection.Close();
                }

                return devuelto;
            }
            catch (Exception e)
            {
                DirectLog.Log("Mensaje: " + e.Message + "\\n Traza: " + e.StackTrace, 1);

                return devuelto;
            }
        }
    }
}