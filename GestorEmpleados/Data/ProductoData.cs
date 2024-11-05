using GestorEmpleados.API.Models;
using MiWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace MiWebAPI.Data
{
    public class ProductoData
    {
        private readonly string conexion;
        public ProductoData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        /// <summary>
        /// Consulta lista de productos con filtro en la descripción
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public async Task<List<Producto>> GetProductos(string filtro)
        {
            List<Producto> lista = new List<Producto>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_producto_selecciona", con);
                cmd.Parameters.AddWithValue("@filtro", filtro);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Producto
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            Stock = Convert.ToInt32(reader["Stock"]),
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            Categoria = reader["Categoria"].ToString(),
                            Color = reader["Color"].ToString(),
                            FechaUltimaModificacion = Convert.ToDateTime(reader["FechaUltimaModificacion"])
                        });
                    }
                }
            }
            return lista;
        }

        /// <summary>
        /// Agrega un producto
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        public async Task<RespuestaDB> AddProducto(Producto objeto)
        {
            var resultado = new RespuestaDB();

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_producto_agrega", con);
                cmd.Parameters.AddWithValue("@descripcion", objeto.Descripcion);
                cmd.Parameters.AddWithValue("@stock", objeto.Stock);
                cmd.Parameters.AddWithValue("@precio", objeto.Precio);
                cmd.Parameters.AddWithValue("@categoria", objeto.Categoria);
                cmd.Parameters.AddWithValue("@color", objeto.Color);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        resultado.Tipo_error = Convert.ToInt32(reader["TipoError"]);
                        resultado.Mensaje = reader["Mensaje"].ToString();
                    }
                }
            }
            return resultado;
        }

        /// <summary>
        /// Actualiza un producto
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        public async Task<RespuestaDB> UpdateProducto(Producto objeto)
        {
            var resultado = new RespuestaDB();

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_producto_actualizar", con);
                cmd.Parameters.AddWithValue("@id", objeto.Id);
                cmd.Parameters.AddWithValue("@descripcion", objeto.Descripcion);
                cmd.Parameters.AddWithValue("@stock", objeto.Stock);
                cmd.Parameters.AddWithValue("@precio", objeto.Precio);
                cmd.Parameters.AddWithValue("@categoria", objeto.Categoria);
                cmd.Parameters.AddWithValue("@color", objeto.Color);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        resultado.Tipo_error = Convert.ToInt32(reader["TipoError"]);
                        resultado.Mensaje = reader["Mensaje"].ToString();
                    }
                }
            }
            return resultado;
        }

        /// <summary>
        /// Elimina un producto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RespuestaDB> DeleteProducto(int Id)
        {
            var resultado = new RespuestaDB();

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_producto_eliminar", con);
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        resultado.Tipo_error = Convert.ToInt32(reader["TipoError"]);
                        resultado.Mensaje = reader["Mensaje"].ToString();
                    }
                }
            }
            return resultado;
        }
    }
}
