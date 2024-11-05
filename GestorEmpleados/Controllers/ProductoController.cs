

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;
using MiWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ProductoData _productoData;
        public ProductosController(ProductoData productoData)
        {
            _productoData = productoData;
        }

       
        [HttpPost]
        [Route("GetProductos")]
        public async Task<IActionResult> Lista([FromBody] string filtro)
        {
            List<Producto> lista = await _productoData.GetProductos(filtro);
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        
        [HttpPost]
        [Route("AddProducto")]
        public async Task<IActionResult> AddProducto([FromBody] Producto objeto)
        {
            var respuesta = await _productoData.AddProducto(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpPost]
        [Route("UpdateProducto")]
        public async Task<IActionResult> UpdateProducto([FromBody] Producto objeto)
        {
            var respuesta = await _productoData.UpdateProducto(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        
        [HttpPost]
        [Route("DeleteProducto")]
        public async Task<IActionResult> DeleteProducto([FromBody] int Id)
        {
            var respuesta = await _productoData.DeleteProducto(Id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}
