using SSMReservas.Library;
using SSMWarehouseManagement.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSMWarehouseManagement.Repositories
{
    public class ProductoRepository
    {
        Conexion Conexion;
        Conexion.Parametros Parametros;

        public ProductoRepository()
        {
            this.Conexion = new Conexion();
            this.Parametros = new Conexion.Parametros("Aqua");

            this.Conexion.LeerConnect(ref Parametros);
        }
    }
}
