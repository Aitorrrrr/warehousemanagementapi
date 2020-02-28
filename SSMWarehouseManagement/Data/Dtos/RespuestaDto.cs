using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSMWarehouseManagement.Data.Dtos
{
    public class RespuestaDto
    {
        public int Error { get; set; }
        public string Respuesta { get; set; }

        public RespuestaDto(int error, string msg)
        {
            this.Error = error;
            this.Respuesta = msg;
        }
    }
}
