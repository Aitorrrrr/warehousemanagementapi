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
    public class ProveedorController
    {
        private ProveedorRepository _proveedorRepository;

        public ProveedorController(ProveedorRepository proveedorRepository)
        {
            this._proveedorRepository = proveedorRepository;
        }

        [Route("[action]")]
        [HttpGet]
        public Proveedor ComprobarOrigen(string numeroDoc)
        {
            return this._proveedorRepository.ComprobarOrigen(numeroDoc);
        }
    }
}
