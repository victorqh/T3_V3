using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using T3_VQUISPEH.Models;

namespace T3_VQUISPEH.Controllers
{
    [Authorize]
    public class CategoriaController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public CategoriaController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // GET: Categoria
        public IActionResult Index()
        {
            List<Categoria> categorias = new List<Categoria>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Categorias ORDER BY Orden";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    categorias.Add(new Categoria
                    {
                        Id = (int)reader["Id"],
                        NombreCategoria = reader["NombreCategoria"].ToString(),
                        Orden = (int)reader["Orden"],
                        Descripcion = reader["Descripcion"].ToString()
                    });
                }
            }

            return View(categorias);
        }

        // GET: Categoria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {
            if (string.IsNullOrEmpty(categoria.NombreCategoria))
            {
                ViewBag.Error = "El nombre de la categoría es obligatorio";
                return View(categoria);
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Categorias (NombreCategoria, Orden, Descripcion) VALUES (@Nombre, @Orden, @Descripcion)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", categoria.NombreCategoria);
                cmd.Parameters.AddWithValue("@Orden", categoria.Orden);
                cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion ?? "");

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: Categoria/Edit/5
        public IActionResult Edit(int id)
        {
            Categoria categoria = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Categorias WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    categoria = new Categoria
                    {
                        Id = (int)reader["Id"],
                        NombreCategoria = reader["NombreCategoria"].ToString(),
                        Orden = (int)reader["Orden"],
                        Descripcion = reader["Descripcion"].ToString()
                    };
                }
            }

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categoria/Edit/5
        [HttpPost]
        public IActionResult Edit(Categoria categoria)
        {
            if (string.IsNullOrEmpty(categoria.NombreCategoria))
            {
                ViewBag.Error = "El nombre de la categoría es obligatorio";
                return View(categoria);
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Categorias SET NombreCategoria = @Nombre, Orden = @Orden, Descripcion = @Descripcion WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", categoria.Id);
                cmd.Parameters.AddWithValue("@Nombre", categoria.NombreCategoria);
                cmd.Parameters.AddWithValue("@Orden", categoria.Orden);
                cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion ?? "");

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: Categoria/Delete/5
        public IActionResult Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Categorias WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }
    }
}
