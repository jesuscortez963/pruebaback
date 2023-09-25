using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;
using CrudTest.model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CrudTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly string _connectionString = "Server=DESKTOP-J7SFMOH\\SQLEXPRESS;Database=test;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=false;";

        [HttpPost("InsertarProducto")]
        public IActionResult InsertarProducto(ProductoDTO productoDTO)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Productos_Crear", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@nombre_producto", productoDTO.NombreProducto));
                    cmd.Parameters.Add(new SqlParameter("@descripcion_producto", productoDTO.DescripcionProducto));
                    cmd.Parameters.Add(new SqlParameter("@precio", productoDTO.Precio));
                    cmd.Parameters.Add(new SqlParameter("@existencia", productoDTO.Existencia));
                    cmd.Parameters.Add(new SqlParameter("@tipo_producto_id", productoDTO.TipoProductoId));
                    cmd.Parameters.Add(new SqlParameter("@fechas_registro", DateTime.Now));

                    con.Open();

                    Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");
                    Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                    Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return Ok(new { message = "Producto Insertado Correctamente" });
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new { message = ex.Message });
                    }
                }
            }
        }

        [HttpDelete("EliminarProducto/{id}")]
        public IActionResult EliminarProducto(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Productos_Eliminar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    con.Open();

                    Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");
                    Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                    Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return Ok(new { message = "Producto Eliminado Correctamente" });
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new { message = ex.Message });
                    }
                }
            }
        }

        [HttpPut("EditarProducto/{id}")]
        public IActionResult EditarProducto(int id, ProductoDTO productoDTO)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_EditarProducto", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.Parameters.Add(new SqlParameter("@nombre_producto", productoDTO.NombreProducto));
                    cmd.Parameters.Add(new SqlParameter("@descripcion_producto", productoDTO.DescripcionProducto));
                    cmd.Parameters.Add(new SqlParameter("@precio", productoDTO.Precio));
                    cmd.Parameters.Add(new SqlParameter("@existencia", productoDTO.Existencia));
                    cmd.Parameters.Add(new SqlParameter("@tipo_producto_id", productoDTO.TipoProductoId));

                    con.Open();

                    Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");
                    Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                    Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return Ok(new { message = "Producto Editado Correctamente" });
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new { message = ex.Message });
                    }
                }
            }
        }

        [HttpGet("ConsultarProducto/{id}")]
        public IActionResult ConsultarProducto(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ConsultarProducto", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    con.Open();

                    Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");
                    Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                    Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var producto = new ProductoDTO
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                NombreProducto = reader["nombre_producto"].ToString(),
                                DescripcionProducto = reader["descripcion_producto"].ToString(),
                                Precio = Convert.ToDecimal(reader["precio"]),
                                Existencia = Convert.ToDecimal(reader["existencia"]),
                                TipoProductoId = Convert.ToInt32(reader["tipo_producto_id"]),
                            };

                            return Ok(producto);
                        }
                        else
                        {
                            return NotFound(new { message = "Producto no encontrado" });
                        }
                    }
                }
            }
        }

        [HttpGet("ConsultarTodosProductos")]
        public IActionResult ConsultarTodosProductos()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ConsultarTodoProductos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();

                    Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");
                    Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                    Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");

                    List<ProductoDTO> productos = new List<ProductoDTO>();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var producto = new ProductoDTO
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                NombreProducto = reader["nombre_producto"].ToString(),
                                DescripcionProducto = reader["descripcion_producto"].ToString(),
                                Precio = Convert.ToDecimal(reader["precio"]),
                                Existencia = Convert.ToDecimal(reader["existencia"]),
                                TipoProductoId = Convert.ToInt32(reader["tipo_producto_id"]),
                            };

                            productos.Add(producto);
                        }
                    }

                    if (productos.Count > 0)
                    {
                        return Ok(productos);
                    }
                    else
                    {
                        return NotFound(new { message = "Productos no encontrados" });
                    }
                }
            }
        }
    }
}
