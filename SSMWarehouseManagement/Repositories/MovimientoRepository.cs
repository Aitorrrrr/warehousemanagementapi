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
    public class MovimientoRepository
    {
        Conexion Conexion;
        Conexion.Parametros Parametros;

        public MovimientoRepository()
        {
            this.Conexion = new Conexion();
            this.Parametros = new Conexion.Parametros("Aqua");

            this.Conexion.LeerConnect(ref Parametros);
        }

        public List<Movimiento> LineasPdtes(decimal numero)
        {
            List<Movimiento> movimientos = new List<Movimiento>();

            try
            {
                using (SqlConnection connection = new SqlConnection(this.Conexion.ConnectionString(Parametros, Parametros.Data)))
                {
                    connection.Open();
                    string sql = "Select NUMERO, POSICION, CODART, INCORPORAD, UNIDADES, IMPORTE, IVA, DENOMINACI, TARIFA, DESCUENTO, COM, REC, COD_EAN13, M.PARTIDA From DATMO" + Parametros.Empresa +
                                " M inner join DATIN" + Parametros.Empresa +" P on M.CODART = P.CODIGO Where NUMERO = " + numero + " and INCORPORAD < UNIDADES";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Movimiento movAux = new Movimiento();

                        movAux.Numero = reader.GetDecimal(0);
                        movAux.Posicion = reader.GetDecimal(1);
                        movAux.Codart = reader.GetString(2).Trim();
                        movAux.Incorporad = reader.GetDecimal(3);
                        movAux.Unidades = reader.GetDecimal(4);
                        movAux.Importe = reader.GetDecimal(5);
                        movAux.Iva = reader.GetDecimal(6);
                        movAux.Denominaci = reader.GetString(7).Trim();
                        movAux.Tarifa = reader.GetDecimal(8);
                        movAux.Descuento = reader.GetDecimal(9);
                        movAux.Com = reader.GetDecimal(10);
                        movAux.Rec = reader.GetDecimal(11);
                        movAux.Ean13 = reader.GetString(12).Trim();
                        movAux.Partida = reader.GetString(13).Trim();

                        movimientos.Add(movAux);
                    }
                    reader.DisposeAsync();

                    connection.Close();
                }

                return movimientos;
            }
            catch (Exception e)
            {
                DirectLog.Log("Mensaje: " + e.Message + "\\n Traza: " + e.StackTrace, 1);

                return movimientos;
            }
        }
    }
}