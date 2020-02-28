using System;
using System.Collections.Generic;

namespace SSMWarehouseManagement.Data.Models
{
    public class Operacion
    {
        public decimal Numero { get; set; }
        public decimal Pendientes { get; set; }
        public string NumeroDoc { get; set; }

        public List<Movimiento> MovAlbaran = new List<Movimiento>();
    }
}