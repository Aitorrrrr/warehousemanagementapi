using Microsoft.AspNetCore.Mvc;
using SSMWarehouseManagement.Data.Dtos;
using SSMWarehouseManagement.Data.Models;
using SSMWarehouseManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSMWarehouseManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperacionController
    {
        private OperacionRepository _operacionRepository;

        public OperacionController(OperacionRepository operacionRepository)
        {
            this._operacionRepository = operacionRepository;
        }

        [Route("[action]")]
        [HttpGet]
        public Operacion ComprobarDestino(string numeroDoc, string codProv)
        {
            return this._operacionRepository.ComprobarDestino(numeroDoc, codProv);
        }

        [Route("[action]")]
        [HttpGet]
        public Operacion NumeroOP(string numeroDoc)
        {
            return this._operacionRepository.NumeroOP(numeroDoc);
        }

        [Route("[action]")]
        [HttpPost]
        public RespuestaDto NuevaOperacion([FromBody] Operacion origen, decimal destino, string numDoc, string contadorAlb, string usuario)
        {
            return this._operacionRepository.NuevaOperacion(origen, destino, numDoc, contadorAlb, usuario);
        }

        [Route("[action]")]
        [HttpPut]
        public RespuestaDto BloquearOperacion(int bloquear, string usuario, decimal numero)
        {
            return this._operacionRepository.BloquearOperacion(bloquear, usuario, numero);
        }
    }
}