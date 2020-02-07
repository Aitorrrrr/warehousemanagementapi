using System;
using System.Collections.Generic;

namespace SSMWarehouseManagement.Data.Models
{
    public partial class Proveedor
    {
        public string Numero { get; set; }
        public string Mercado { get; set; }
        public string ExentoIva { get; set; }
        public DateTime Fecha { get; set; }
        public string Comercial { get; set; }
        public string Nombre { get; set; }
        public string Nif { get; set; }
        public string Cif { get; set; }
        public string Direccion { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public string Cpostal { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Fax { get; set; }
        public decimal Categoria { get; set; }
        public string Observacio { get; set; }
        public bool? Conrecargo { get; set; }
        public string Lugarpago { get; set; }
        public string Transporte { get; set; }
        public string Entfpago { get; set; }
        public string Agente { get; set; }
        public decimal Diapago1 { get; set; }
        public decimal Diapago2 { get; set; }
        public decimal Diapago3 { get; set; }
        public decimal Riesgo { get; set; }
        public string Fechasrec { get; set; }
        public decimal DtoTt { get; set; }
        public string Texto { get; set; }
        public bool? Excluiraut { get; set; }
        public string Moneda { get; set; }
        public decimal Fcoste { get; set; }
        public string Pais { get; set; }
        public string Descascada { get; set; }
        public bool? Agrupacion { get; set; }
        public decimal Deuda { get; set; }
        public bool? Confirmfac { get; set; }
        public decimal DeudaAlb { get; set; }
        public string Desfamilia { get; set; }
        public string Causaaprob { get; set; }
        public string Claveaprob { get; set; }
        public DateTime Fechaaprob { get; set; }
        public bool? Opdivide { get; set; }
        public string Internet { get; set; }
        public DateTime Fecharevis { get; set; }
        public string Tipoaprob { get; set; }
        public string Juegoiva { get; set; }
        public string Codprovcli { get; set; }
        public decimal Mesnopago { get; set; }
        public decimal Irpf1 { get; set; }
        public decimal Irpf2 { get; set; }
        public bool? Cee { get; set; }
        public bool? Euro { get; set; }
        public string CodEan13 { get; set; }
        public decimal Suppercefore { get; set; }
        public bool? Generico { get; set; }
        public decimal Servicedays { get; set; }
        public bool? Descatalogado { get; set; }
        public bool? Mod347exclude { get; set; }
        public bool? Mod347include { get; set; }
        public bool? Mod347services { get; set; }
        public string Corporateweb { get; set; }
        public bool? Txmlease { get; set; }
        public string Sepaiso { get; set; }
        public string Sepadc { get; set; }
        public string Sepacode { get; set; }
        public string Sepaid { get; set; }
        public string Comunidad { get; set; }
        public string Acceffectpre { get; set; }
        public string Cifinter { get; set; }
        public bool? Ispoperation { get; set; }
        public string Bcoterceros { get; set; }
        public bool? Vatcash { get; set; }
        public bool? Vatcashentry { get; set; }
        public string Supestadointra { get; set; }
        public string Pagaredireccion { get; set; }
        public string Pagarelocalidad { get; set; }
        public string Pagarecpostal { get; set; }
        public string Pagareprovincia { get; set; }
        public string Pagarecomunidad { get; set; }
        public string Pagarepais { get; set; }
        public bool? Enviopagare { get; set; }
    }
}
