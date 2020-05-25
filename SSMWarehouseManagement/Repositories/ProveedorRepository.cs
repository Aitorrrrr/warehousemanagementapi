using SSMReservas.Library;
using SSMWarehouseManagement.Data.Models;
using SSMWarehouseManagement.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SSMWarehouseManagement.Repositories
{
    public class ProveedorRepository
    {
        Conexion Conexion;
        Conexion.Parametros Parametros;

        public ProveedorRepository()
        {
            this.Conexion = new Conexion();
            this.Parametros = new Conexion.Parametros("Aqua");

            this.Conexion.LeerConnect(ref Parametros);
        }

        public Proveedor ComprobarOrigen(string numeroDoc)
        {
            Proveedor proveedor = new Proveedor();

            try
            {
                using (SqlConnection connection = new SqlConnection(this.Conexion.ConnectionString(Parametros, Parametros.Data)))
                {
                    connection.Open();
                    string sql = "Select O.NIF,P.NOMBRE,P.EXENTO_IVA From DATOP" + Parametros.Empresa +
                                " O inner join DATPROV" + Parametros.Empresa + " P on O.NIF = P.NIF  Where NUMERO_DOC = '" + numeroDoc + "' " +
                                " And TIPOOPER = 'P' And PENDIENTES > 0 And USERQUERY = '' And ISREADYUSER = ''";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        proveedor.Nif = reader.GetString(0).Trim();
                        proveedor.Nombre = reader.GetString(1).Trim();
                        proveedor.ExentoIva = reader.GetString(2).Trim();
                    }
                    reader.DisposeAsync();

                    connection.Close();
                }

                return proveedor;
            }
            catch (Exception e)
            {
                DirectLog.Log("Mensaje: " + e.Message + "\\n Traza: " + e.StackTrace, 1);

                return proveedor;
            }
        }
    }
}