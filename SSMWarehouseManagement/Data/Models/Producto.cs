using System;
using System.Collections.Generic;

namespace SSMWarehouseManagement.Data.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Datmo01 = new HashSet<Movimiento>();
        }

        public string Codigo { get; set; }
        public string Descripcio { get; set; }
        public decimal NPartes { get; set; }
        public string CodProv { get; set; }
        public decimal Preciov { get; set; }
        public decimal PrecioTpv { get; set; }
        public decimal Des1 { get; set; }
        public decimal Des2 { get; set; }
        public decimal Des3 { get; set; }
        public decimal Des4 { get; set; }
        public decimal Des5 { get; set; }
        public decimal Des6 { get; set; }
        public string Localizaci { get; set; }
        public decimal Preciocomp { get; set; }
        public decimal Preciocmed { get; set; }
        public string Proveedor { get; set; }
        public string Fabricante { get; set; }
        public decimal Existencia { get; set; }
        public decimal MinimoCob { get; set; }
        public decimal Existmin { get; set; }
        public decimal Existmax { get; set; }
        public bool? Controlado { get; set; }
        public string CodEan13 { get; set; }
        public string Codigosup { get; set; }
        public string Tipoiva { get; set; }
        public string Tiporec { get; set; }
        public decimal Com1 { get; set; }
        public decimal Com2 { get; set; }
        public decimal Com3 { get; set; }
        public decimal Com4 { get; set; }
        public decimal Com5 { get; set; }
        public decimal Com6 { get; set; }
        public decimal Almacenes { get; set; }
        public decimal Ncualidade { get; set; }
        public string Hserie { get; set; }
        public string Serie1 { get; set; }
        public string Serie2 { get; set; }
        public DateTime Fechaalta { get; set; }
        public string Neto { get; set; }
        public string PorNserie { get; set; }
        public decimal Hprecio { get; set; }
        public string Texto { get; set; }
        public decimal Peso { get; set; }
        public decimal Volumen { get; set; }
        public string Imagen { get; set; }
        public decimal Bultos { get; set; }
        public string Descriptec { get; set; }
        public decimal Incremento { get; set; }
        public decimal Hpreciocom { get; set; }
        public decimal Daysstock { get; set; }
        public string Defaultunit { get; set; }
        public decimal Prdamountecotax { get; set; }
        public string Partida { get; set; }
        public decimal Secondstock { get; set; }
        public string Pverde { get; set; }
        public decimal Secondprices { get; set; }
        public bool? Portes { get; set; }
        public decimal Secondconvert { get; set; }
        public decimal Largo { get; set; }
        public decimal Topunits { get; set; }
        public string Topname { get; set; }
        public decimal Pesoneto { get; set; }
        public string Secondname { get; set; }
        public string Mainname { get; set; }
        public decimal Topstock { get; set; }
        public decimal Prdminimaldays { get; set; }
        public bool? Descatalogado { get; set; }
        public bool? Irpf { get; set; }
        public decimal Ancho { get; set; }
        public bool? Planneddelivery { get; set; }
        public string Descripcion2 { get; set; }
        public string Naturalezaprodu { get; set; }
        public string Serialtype { get; set; }
        public string Concepto { get; set; }
        public bool? Tminvestment { get; set; }
        public bool? Ispproduct { get; set; }
        public bool? Familiaweb { get; set; }
        public string Prdcodeintra { get; set; }
        public string Prdcodunidsup { get; set; }
        public decimal Prdfactorunid { get; set; }
        public string Familyn1 { get; set; }
        public decimal Alto { get; set; }
        public string Mainunit { get; set; }
        public string Secondunit { get; set; }
        public decimal Equalunits { get; set; }
        public string Customer { get; set; }
        public string Delegation { get; set; }
        public string Billing { get; set; }
        public string Cusreference { get; set; }
        public string Cusdescrip { get; set; }
        public string Cusrefsa { get; set; }
        public string Project { get; set; }

        public virtual ICollection<Movimiento> Datmo01 { get; set; }
    }
}
