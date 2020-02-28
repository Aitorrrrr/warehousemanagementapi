using SSMReservas.Library;
using SSMWarehouseManagement.Data.Dtos;
using SSMWarehouseManagement.Data.Models;
using SSMWarehouseManagement.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SSMWarehouseManagement.Repositories
{
    public class OperacionRepository
    {
        private Conexion Conexion;
        private Conexion.Parametros Parametros;
        private ProveedorRepository provRepo;

        public OperacionRepository(ProveedorRepository proveedorRepository)
        {
            this.Conexion = new Conexion();
            this.Parametros = new Conexion.Parametros("Aqua");

            this.Conexion.LeerConnect(ref Parametros);

            this.provRepo = proveedorRepository;
        }

        public Operacion ComprobarDestino(string numeroDoc, string codProv)
        {
            Operacion operacion = new Operacion();

            try
            {
                using (SqlConnection connection = new SqlConnection(this.Conexion.ConnectionString(Parametros, Parametros.Data)))
                {
                    connection.Open();
                    string sql = "Select NUMERO,NUMERO_DOC,PENDIENTES From DATOP" + Parametros.Empresa +
                                " Where NUMERO_DOC = '" + numeroDoc + "' and TIPOOPER = 'E' and NIF = '" + codProv + "'";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        operacion.Numero = reader.GetDecimal(0);
                        operacion.NumeroDoc= reader.GetString(1).Trim();
                        operacion.Pendientes = reader.GetDecimal(2);
                    }
                    reader.DisposeAsync();

                    connection.Close();
                }

                return operacion;
            }
            catch (Exception e)
            {
                DirectLog.Log("Mensaje: " + e.Message + "\\n Traza: " + e.StackTrace, 1);

                return operacion;
            }
        }

        public Operacion NumeroOP(string numeroDoc)
        {
            Operacion operacion = new Operacion();

            try
            {
                using (SqlConnection connection = new SqlConnection(this.Conexion.ConnectionString(Parametros, Parametros.Data)))
                {
                    connection.Open();
                    string sql = "Select NUMERO From DATOP" + Parametros.Empresa +
                                " Where NUMERO_DOC = '" + numeroDoc + "' ";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        operacion.Numero = reader.GetDecimal(0);
                    }
                    reader.DisposeAsync();

                    connection.Close();
                }

                return operacion;
            }
            catch (Exception e)
            {
                DirectLog.Log("Mensaje: " + e.Message + "\\n Traza: " + e.StackTrace, 1);

                return operacion;
            }
        }

        public RespuestaDto NuevaOperacion(Operacion origen, decimal destino, string numDoc, string contadorAlb, string usuario)
        {
            // Variables grales operación.
            string formap = "";
            decimal dtoTotal = 0;
            decimal dtoPP = 0;
            string nEfecto = "";
            string centro = "";
            decimal euroToDivi = 0;
            decimal euroToMone = 0;
            string moneda = "";
            string formatoDocumento = "";
            string fecha = DateTime.Now.ToString("yyyyMMdd");
            string hora = DateTime.Now.ToString("hh:mm");

            decimal numeroOperacion = 0;
            if (destino != 0)
            {
                numeroOperacion = destino;
            }
            else
            {
                numeroOperacion = GenerarNumero();
            }
            double numeroPosicion = 0;

            // % IVA
            decimal pcIVA1 = 0;
            decimal pcIVA2 = 0;
            decimal pcIVA3 = 0;

            // % Recargo
            decimal pcREC1 = 0;
            decimal pcREC2 = 0;
            decimal pcREC3 = 0;

            // Bases IVA
            decimal baseIVA1 = 0;
            decimal baseIVA2 = 0;
            decimal baseIVA3 = 0;

            // Bases Recargo
            decimal baseREC1 = 0;
            decimal baseREC2 = 0;
            decimal baseREC3 = 0;

            // Totales IVA
            decimal totIVA1 = 0;
            decimal totIVA2 = 0;
            decimal totIVA3 = 0;

            // Totales REC
            decimal totREC1 = 0;
            decimal totREC2 = 0;
            decimal totREC3 = 0;

            // Totales
            decimal TOTAL = 0;
            decimal TOTIVA = 0;
            decimal TOTRECARGO = 0;
            decimal ARTICULOS = 0;
            decimal SERVICIOS = 0;
            decimal UNIDADES = 0;

            // Precisión decimal
            int NUMPRECISI = 0;
            int NUMPREC_LI = 0;

            Proveedor provOP = provRepo.ComprobarOrigen(origen.NumeroDoc);

            try
            {
                using (SqlConnection connection = new SqlConnection(this.Conexion.ConnectionString(Parametros, Parametros.Data)))
                {
                    connection.Open();
                    string sql = "select NUMPRECISI,NUMPREC_LI, F_ALBARAN_COM from CMNEM " +
                                " Where NUMERO = '" + Parametros.Empresa + "' and EJERCICIO = '" + Parametros.Ejercicio + "'";
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        NUMPRECISI = Decimal.ToInt32(reader.GetDecimal(0));
                        NUMPREC_LI = Decimal.ToInt32(reader.GetDecimal(1));
                        formatoDocumento = reader.GetString(2);
                    }
                    reader.DisposeAsync();

                    if (destino == 0)
                    {
                        sql = "select FORMAP,DTOTOTAL,DTO_PP,NEFECTO,CENTRO,EUROTODIVI,EUROTOMONE,MONEDA,PCREC1,PCREC2,PCREC3,PCIVA1,PCIVA2,PCIVA3 from DATOP" + Parametros.Empresa +
                                " Where NUMERO = '" + origen.Numero + "'";
                    }
                    else
                    {
                        sql = "select FORMAP,DTOTOTAL,DTO_PP,NEFECTO,CENTRO,EUROTODIVI,EUROTOMONE,MONEDA,PCREC1,PCREC2,PCREC3,PCIVA1,PCIVA2,PCIVA3 from DATOP" + Parametros.Empresa +
                                " Where NUMERO = '" + destino + "'";
                    }
                    
                    cmd.CommandText = sql;
                    SqlDataReader readerOp = cmd.ExecuteReader();
                    if (readerOp.Read())
                    {
                        formap = readerOp.GetString(0);
                        dtoTotal = readerOp.GetDecimal(1);
                        dtoPP = readerOp.GetDecimal(2);
                        nEfecto = readerOp.GetString(3);
                        centro = readerOp.GetString(4);
                        euroToDivi = readerOp.GetDecimal(5);
                        euroToMone = readerOp.GetDecimal(6);
                        moneda = readerOp.GetString(7);
                        pcREC1 = readerOp.GetDecimal(8);
                        pcREC2 = readerOp.GetDecimal(9);
                        pcREC3 = readerOp.GetDecimal(10);
                        pcIVA1 = readerOp.GetDecimal(11);
                        pcIVA2 = readerOp.GetDecimal(12);
                        pcIVA3 = readerOp.GetDecimal(13);
                    }
                    readerOp.DisposeAsync();

                    if (numDoc == null)
                    {
                        sql = "select SERIE, NUMERO from DATCT" + Parametros.Empresa +
                                " Where CODIGO = '" + contadorAlb + "'";
                        cmd.CommandText = sql;
                        SqlDataReader readerContador = cmd.ExecuteReader();

                        if (readerContador.Read())
                        {
                            string serie = readerContador.GetString(0);
                            decimal numero = readerContador.GetDecimal(1);

                            string numeroAux = string.Concat(Enumerable.Repeat("0", 8 - numero.ToString().Length)) + numero;

                            numDoc = serie + numeroAux;
                        }
                        else
                        {
                            return new RespuestaDto(1, "El código para la serie del documento no es válido.");
                        }
                        readerContador.DisposeAsync();
                    }

                    int lineasInsertadas = 0;
                    foreach (Movimiento mov in origen.MovAlbaran)
                    {
                        decimal totImporteLinea = Math.Round(mov.Importe * mov.Unidades, NUMPREC_LI);
                        decimal totalLinea = Math.Round(totImporteLinea * (1 - (dtoTotal/100)), NUMPREC_LI);
                        totalLinea = Math.Round(totalLinea * (1 - (dtoPP / 100)), NUMPREC_LI);

                        sql = "insert into DATMO" + Parametros.Empresa + 
                                     " (FECHA, NUMERO, POSICION, CODART, INCORPORAD, " +
                                     " UNIDADES, IMPORTE, TOTIMPORTE, TOTAL, IVA, " + 
                                     " DENOMINACI, TARIFA, DESCUENTO, COM, REC, " + 
                                     " PROVIENE, MOPROVIENE) values (" +
                                     " '" + fecha + "', " + numeroOperacion + ", " + GenerarNumero() + ", '" + mov.Codart + "', 0, " +
                                     " " + mov.Unidades.ToString().Replace(",",".") + ",  " + mov.Importe.ToString().Replace(",", ".") + ", " + totImporteLinea.ToString().Replace(",", ".") + ", " + totalLinea.ToString().Replace(",", ".") + ", " + mov.Iva.ToString().Replace(",", ".") + ", " +
                                     " '" + mov.Denominaci.Replace("'", "\"") + "', " + mov.Tarifa.ToString().Replace(",", ".") + ", " + mov.Descuento.ToString().Replace(",", ".") + ", " + mov.Com.ToString().Replace(",", ".") + ", " + mov.Rec.ToString().Replace(",", ".") + ", " + 
                                     " '" + origen.NumeroDoc + "', '" + mov.Moproviene + "')";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        if (mov.Moproviene.Trim() != "")
                        {
                            sql = "update DATMO" + Parametros.Empresa + " set INCORPORAD = INCORPORAD + " + mov.Unidades.ToString().Replace(",", ".") + " where NUMERO = '" + mov.Numero + "' and POSICION = '" + mov.Posicion + "'";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                        }

                        lineasInsertadas++;
                    }

                    sql = "select isnull(sum(TOTAL),0) as TOTAL, isnull(sum(UNIDADES),0) as UNIDADES from DATMO" + Parametros.Empresa + " where NUMERO = " + numeroOperacion;
                    cmd.CommandText = sql;
                    SqlDataReader readerTotales = cmd.ExecuteReader();
                    if (readerTotales.Read())
                    {
                        TOTAL = readerTotales.GetDecimal(0);
                        TOTAL = Math.Round(TOTAL, NUMPRECISI);
                        UNIDADES = readerTotales.GetDecimal(1);
                    }
                    readerTotales.Close();

                    sql = "select isnull(sum(TOTAL),0),isnull(sum(TOTAL)*" + (pcIVA1/100).ToString().Replace(",", ".") + ",0) from DATMO" + Parametros.Empresa + " where NUMERO = " + numeroOperacion + " and IVA = " + pcIVA1.ToString().Replace(",", ".");
                    cmd.CommandText = sql;
                    readerTotales = cmd.ExecuteReader();
                    if (readerTotales.Read())
                    {
                        baseIVA1 = readerTotales.GetDecimal(0);
                        totIVA1 = readerTotales.GetDecimal(1);
                    }
                    readerTotales.Close();

                    sql = "select isnull(sum(TOTAL),0),isnull(sum(TOTAL)*" + (pcIVA2 / 100).ToString().Replace(",", ".") + ",0) from DATMO" + Parametros.Empresa + " where NUMERO = " + numeroOperacion + " and IVA = " + pcIVA2.ToString().Replace(",", ".");
                    cmd.CommandText = sql;
                    readerTotales = cmd.ExecuteReader();
                    if (readerTotales.Read())
                    {
                        baseIVA2 = readerTotales.GetDecimal(0);
                        totIVA2 = readerTotales.GetDecimal(1);
                    }
                    readerTotales.Close();

                    sql = "select isnull(sum(TOTAL),0),isnull(sum(TOTAL)*" + (pcIVA3 / 100).ToString().Replace(",", ".") + ",0) from DATMO" + Parametros.Empresa + " where NUMERO = " + numeroOperacion + " and IVA = " + pcIVA3.ToString().Replace(",", ".");
                    cmd.CommandText = sql;
                    readerTotales = cmd.ExecuteReader();
                    if (readerTotales.Read())
                    {
                        baseIVA3 = readerTotales.GetDecimal(0);
                        totIVA3 = readerTotales.GetDecimal(1);
                    }
                    readerTotales.Close();

                    sql = "select isnull(sum(TOTAL),0),isnull(sum(TOTAL)*" + (pcREC1 / 100).ToString().Replace(",", ".") + ",0) from DATMO" + Parametros.Empresa + " where NUMERO = " + numeroOperacion + " and REC = " + pcREC1.ToString().Replace(",", ".");
                    cmd.CommandText = sql;
                    readerTotales = cmd.ExecuteReader();
                    if (readerTotales.Read())
                    {
                        baseREC1 = readerTotales.GetDecimal(0);
                        totREC1 = readerTotales.GetDecimal(1);
                    }
                    readerTotales.Close();

                    sql = "select isnull(sum(TOTAL),0),isnull(sum(TOTAL)*" + (pcREC2 / 100).ToString().Replace(",", ".") + ",0) from DATMO" + Parametros.Empresa + " where NUMERO = " + numeroOperacion + " and REC = " + pcREC2.ToString().Replace(",", ".");
                    cmd.CommandText = sql;
                    readerTotales = cmd.ExecuteReader();
                    if (readerTotales.Read())
                    {
                        baseREC2 = readerTotales.GetDecimal(0);
                        totREC2 = readerTotales.GetDecimal(1);
                    }
                    readerTotales.Close();

                    sql = "select isnull(sum(TOTAL),0),isnull(sum(TOTAL)*" + (pcREC3 / 100).ToString().Replace(",", ".") + ",0) from DATMO" + Parametros.Empresa + " where NUMERO = " + numeroOperacion + " and REC = " + pcREC3.ToString().Replace(",", ".");
                    cmd.CommandText = sql;
                    readerTotales = cmd.ExecuteReader();
                    if (readerTotales.Read())
                    {
                        baseREC3 = readerTotales.GetDecimal(0);
                        totREC3 = readerTotales.GetDecimal(1);
                    }
                    readerTotales.Close();

                    sql = "select isnull(sum(case DATIN01.NETO when 'S' then TOTAL else 0 end),0), isnull(sum(case DATIN01.NETO when 'N' then TOTAL else 0 end),0) from DATMO" + Parametros.Empresa + " inner join DATIN" + Parametros.Empresa + " on CODART = CODIGO  where NUMERO = " + numeroOperacion;
                    cmd.CommandText = sql;
                    readerTotales = cmd.ExecuteReader();
                    if (readerTotales.Read())
                    {
                        SERVICIOS = readerTotales.GetDecimal(0);
                        ARTICULOS = readerTotales.GetDecimal(1);
                    }
                    readerTotales.Close();

                    if (provOP.ExentoIva == "S")
                    {
                        baseIVA1 = 0;
                        baseIVA2 = 0;
                        baseIVA3 = 0;
                        totIVA1 = 0;
                        totIVA2 = 0;
                        totIVA3 = 0;

                        baseREC1 = 0;
                        baseREC2 = 0;
                        baseREC3 = 0;
                        totREC1 = 0;
                        totREC2 = 0;
                        totREC3 = 0;
                    }

                    totIVA1 = Math.Round(totIVA1, NUMPRECISI);
                    totIVA2 = Math.Round(totIVA2, NUMPRECISI);
                    totIVA3 = Math.Round(totIVA3, NUMPRECISI);
                    TOTIVA = totIVA1 + totIVA2 + totIVA3;

                    totREC1 = Math.Round(totREC1, NUMPRECISI);
                    totREC2 = Math.Round(totREC2, NUMPRECISI);
                    totREC3 = Math.Round(totREC3, NUMPRECISI);
                    TOTRECARGO = totREC1 + totREC2 + totREC3;

                    int exento;
                    if (provOP.ExentoIva == "S")
                    {
                        exento = 1;
                    }
                    else
                    {
                        exento = 0;
                    }

                    if (destino == 0)
                    {
                        sql = "insert into DATOP" + Parametros.Empresa +
                              " (NUMERO, PENDIENTES, FECHA, NUMERO_DOC, FORMAP, " + //
                              " TIPOOPER, TOTAL, TOTALNETO, TOTIVA, ARTICULOS, " + //
                              " NEFECTO, NIF, TOTRECARGO, DTO_PP, DTOTOTAL, " + //
                              " USUARIO, BASEIVA1, BASEIVA2, BASEIVA3, BASEREC1, BASEREC2, BASEREC3, PCIVA1, " + //
                              " PCIVA2, PCIVA3, PCREC1, PCREC2, PCREC3, " + //
                              " CENTRO, UNIDADES, DIARIO, MONEDA, EXENTOIVA, " + //
                              " SERVICIOS, TOTALIRPF, TOTIVAIRPF, TOTIVAIRPF1, TOTIVAIRPF2, TOTIVAIRPF3, " + //
                              " TOTRECARGOIRPF, TOTRECARGOIRPF1, TOTRECARGOIRPF2, TOTRECARGOIRPF3, TOTALDECIRPF, " +
                              " FORMATO, HORA, NUMPRECISI, NUMPREC_LI, EUROTODIVI, EUROTOMONE, T_TARIFA, TOTALDEC) values (" +
                              " " + numeroOperacion + ", " + lineasInsertadas + ", '" + fecha + "', '" + numDoc + "', '" + formap + "', " + 
                              " 'E', " + TOTAL.ToString().Replace(",", ".") + ", " + SERVICIOS.ToString().Replace(",", ".") + ", " + TOTIVA.ToString().Replace(",", ".") + ", " + ARTICULOS.ToString().Replace(",", ".") + ", " + 
                              " '" + nEfecto + "', '" + provOP.Nif + "', " + TOTRECARGO.ToString().Replace(",", ".") + ", " + dtoPP.ToString().Replace(",", ".") + ", " + dtoTotal.ToString().Replace(",", ".") + ", " +
                              " '" + usuario + "', " + baseIVA1.ToString().Replace(",", ".") + ", " + baseIVA2.ToString().Replace(",", ".") + ", " + baseIVA3.ToString().Replace(",", ".") + ", " + baseREC1.ToString().Replace(",", ".") + ", " + baseREC2.ToString().Replace(",", ".") + ", " + baseREC3.ToString().Replace(",", ".") + ", " + pcIVA1.ToString().Replace(",", ".") + ", " +
                              " " + pcIVA2.ToString().Replace(",", ".") + ", " + pcIVA3.ToString().Replace(",", ".") + ", " + pcREC1.ToString().Replace(",", ".") + ", " + pcREC2.ToString().Replace(",", ".") + ", " + pcREC3.ToString().Replace(",", ".") + ", " +
                              " '" + centro + "', " + UNIDADES.ToString().Replace(",", ".") + ", 0, '" + moneda + "', " + exento + ", " +
                              " " + SERVICIOS.ToString().Replace(",", ".") + ", " + TOTAL.ToString().Replace(",", ".") + ", " + TOTIVA.ToString().Replace(",", ".") + ", " + baseIVA1.ToString().Replace(",", ".") + ", " + baseIVA2.ToString().Replace(",", ".") + ", " + baseIVA3.ToString().Replace(",", ".") + ", " +
                              " " + TOTRECARGO.ToString().Replace(",", ".") + ", " + baseREC1.ToString().Replace(",", ".") + ", " + baseREC2.ToString().Replace(",", ".") + ", " + baseREC3.ToString().Replace(",", ".") + ", " + TOTAL.ToString().Replace(",", ".") + ", " +
                              " '" + formatoDocumento + "', '" + hora + "', " + NUMPRECISI + ", " + NUMPREC_LI + ", " + euroToDivi.ToString().Replace(",", ".") + ", " + euroToMone.ToString().Replace(",", ".") + ", " + TOTAL.ToString().Replace(",", ".") + ", " + TOTAL.ToString().Replace(",", ".") + ")";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        sql = "update DATOP" + Parametros.Empresa + " set " +
                              " PENDIENTES = " + lineasInsertadas + ", TOTAL = " + TOTAL.ToString().Replace(",", ".") + ", TOTALNETO = " + SERVICIOS.ToString().Replace(",", ".") + ", TOTIVA = " + TOTIVA.ToString().Replace(",", ".") + ", ARTICULOS = " + ARTICULOS.ToString().Replace(",", ".") + ", " +
                              " TOTRECARGO = " + TOTRECARGO.ToString().Replace(",", ".") + ", BASEIVA1 = " + baseIVA1.ToString().Replace(",", ".") + ", BASEIVA2 = " + baseIVA2.ToString().Replace(",", ".") + ", BASEIVA3 = " + baseIVA3.ToString().Replace(",", ".") + ", BASEREC1 = " + baseREC1.ToString().Replace(",", ".") + ", BASEREC2 = " + baseREC2.ToString().Replace(",", ".") + ", BASEREC3 = " + baseREC3.ToString().Replace(",", ".") + ", " +
                              " UNIDADES = " + UNIDADES.ToString().Replace(",", ".") + ", SERVICIOS = " + SERVICIOS.ToString().Replace(",", ".") + ", TOTALIRPF = " + TOTAL.ToString().Replace(",", ".") + ", TOTIVAIRPF = " + TOTIVA.ToString().Replace(",", ".") + ", TOTIVAIRPF1 = " + baseIVA1.ToString().Replace(",", ".") + ", TOTIVAIRPF2 = " + baseIVA2.ToString().Replace(",", ".") + ", TOTIVAIRPF3 = " + baseIVA3.ToString().Replace(",", ".") + ", TOTALDECIRPF = " + TOTAL.ToString().Replace(",", ".") + ", " +
                              " TOTRECARGOIRPF = " + TOTRECARGO.ToString().Replace(",", ".") + ", TOTRECARGOIRPF1 = " + baseREC1.ToString().Replace(",", ".") + ", TOTRECARGOIRPF2 = " + baseREC2.ToString().Replace(",", ".") + ", TOTRECARGOIRPF3 = " + baseREC3.ToString().Replace(",", ".") + ", " +
                              " T_TARIFA = " + TOTAL.ToString().Replace(",", ".") + ", TOTALDEC = " + TOTAL.ToString().Replace(",", ".") + " " +
                              " where NUMERO = " + destino;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }

                    sql = "update op set op.PENDIENTES = (select count(*) from DATMO01 where UNIDADES > INCORPORAD and NUMERO = op.NUMERO) from DATOP01 op inner join DATMO01 mo on op.NUMERO = mo.NUMERO where op.NUMERO = " + origen.Numero;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    connection.Close();

                    return new RespuestaDto(0, "Operación creada corrrectamente.");
                }
            }
            catch (Exception e)
            {
                DirectLog.Log("Mensaje: " + e.Message + "\\n Traza: " + e.StackTrace, 1);
                return new RespuestaDto(1, "Fallo en la transacción sql.");
            }
        }

        public decimal GenerarNumero()
        {
            decimal numero = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(this.Conexion.ConnectionString(Parametros, Parametros.Data)))
                {
                    connection.Open();
                    SqlTransaction transaction;
                    transaction = connection.BeginTransaction("InternalCounterTrans");

                    string sql = "Select NUMERO_OP From DATINTERNALCOUNTER " +
                                " Where COMPANY = '" + Parametros.Empresa + "' and EXERCISE = '" + Parametros.Ejercicio + "'";
                    SqlCommand cmd = new SqlCommand(sql, connection, transaction);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        numero = reader.GetDecimal(0);
                    }
                    reader.DisposeAsync();

                    sql = "Update DATINTERNALCOUNTER Set NUMERO_OP = NUMERO_OP+1 " +
                            " WHERE COMPANY ='" + Parametros.Empresa + "' " +
                            " AND EXERCISE = '" + Parametros.Ejercicio + "'";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    connection.Close();
                }

                return numero;
            }
            catch (Exception e)
            {
                DirectLog.Log("Mensaje: " + e.Message + "\\n Traza: " + e.StackTrace, 1);

                return numero;
            }
        }

        // bloquear = 0 desbloquear operación
        // bloquear = 1 bloquear operación
        public RespuestaDto BloquearOperacion(int bloquear, string usuario, decimal numero)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.Conexion.ConnectionString(Parametros, Parametros.Data)))
                {
                    connection.Open();
                    string sql = "";
                    if (bloquear == 1)
                    {
                        sql = "Update DATOP" + Parametros.Empresa + " set ISREADYUSER = '" + usuario.ToUpper().Trim() + "', USERQUERY = '" + usuario.ToUpper().Trim() + "'  where NUMERO = " + numero;
                    }
                    else
                    {
                        sql = "Update DATOP" + Parametros.Empresa + " set ISREADYUSER = '', USERQUERY = ''  where NUMERO = " + numero;
                    }
                        
                    SqlCommand cmd = new SqlCommand(sql, connection);

                    cmd.ExecuteNonQuery();

                    connection.Close();
                }

                return new RespuestaDto(0, "Operación bloqueada.");
            }
            catch (Exception e)
            {
                DirectLog.Log("Mensaje: " + e.Message + "\\n Traza: " + e.StackTrace, 1);

                return new RespuestaDto(1, "Fallo al bloquear la operación.");
            }
        }
    }
}