using System;
using System.Collections.Generic;

namespace SSMWarehouseManagement.Data.Models
{
    public partial class Almacen
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool? Disponible { get; set; }
        public bool? Deposito { get; set; }
        public string SerieAlb { get; set; }
        public string SerieFac { get; set; }
        public string SeriePed { get; set; }
        public string SeriePre { get; set; }
        public string SerieTra { get; set; }
        public string SeriePped { get; set; }
        public decimal Albaran { get; set; }
        public decimal Factura { get; set; }
        public decimal Pedido { get; set; }
        public decimal Presupuesto { get; set; }
        public decimal Translado { get; set; }
        public decimal Pedidocompra { get; set; }
        public string SerieTpv { get; set; }
        public decimal Tpv { get; set; }
        public string SerieReg { get; set; }
        public decimal Regularizacion { get; set; }
        public string Wrhprovintra { get; set; }
    }
}
