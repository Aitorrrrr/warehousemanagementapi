using Microsoft.AspNetCore.Mvc;
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
    public class MovimientoController
    {
        private MovimientoRepository _movimientoRepository;

        public MovimientoController(MovimientoRepository movimientoRepository)
        {
            _movimientoRepository = movimientoRepository;
        }

        [Route("[action]")]
        [HttpGet]
        public List<Movimiento> LineasPdtes(decimal numero)
        {
            return this._movimientoRepository.LineasPdtes(numero);
        }
    }
}
