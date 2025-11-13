using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using T3_VQUISPEH.Models;

namespace T3_VQUISPEH.Controllers
{
    [Authorize]
    public class LibroController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public LibroController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // GET: Libro
        public IActionResult Index()
        {
            List<Libro> libros = new List<Libro>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Libros ORDER BY Titulo";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    libros.Add(new Libro
                    {
                        Id = (int)reader["Id"],
                        Titulo = reader["Titulo"].ToString(),
                        Autor = reader["Autor"].ToString(),
                        Tema = reader["Tema"].ToString(),
                        Editorial = reader["Editorial"].ToString(),
                        AnioPublicacion = (int)reader["AnioPublicacion"],
                        Paginas = (int)reader["Paginas"],
                        Categoria = reader["Categoria"].ToString(),
                        Material = reader["Material"].ToString(),
                        Copias = (int)reader["Copias"]
                    });
                }
            }

            return View(libros);
        }

        // GET: Libro/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Libro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                return View(libro);
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Libros (Titulo, Autor, Tema, Editorial, AnioPublicacion, Paginas, Categoria, Material, Copias) 
                               VALUES (@Titulo, @Autor, @Tema, @Editorial, @AnioPublicacion, @Paginas, @Categoria, @Material, @Copias)";
                
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
                cmd.Parameters.AddWithValue("@Autor", libro.Autor);
                cmd.Parameters.AddWithValue("@Tema", libro.Tema);
                cmd.Parameters.AddWithValue("@Editorial", libro.Editorial);
                cmd.Parameters.AddWithValue("@AnioPublicacion", libro.AnioPublicacion);
                cmd.Parameters.AddWithValue("@Paginas", libro.Paginas);
                cmd.Parameters.AddWithValue("@Categoria", libro.Categoria);
                cmd.Parameters.AddWithValue("@Material", libro.Material);
                cmd.Parameters.AddWithValue("@Copias", libro.Copias);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: Libro/Edit/5
        public IActionResult Edit(int id)
        {
            Libro libro = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Libros WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    libro = new Libro
                    {
                        Id = (int)reader["Id"],
                        Titulo = reader["Titulo"].ToString(),
                        Autor = reader["Autor"].ToString(),
                        Tema = reader["Tema"].ToString(),
                        Editorial = reader["Editorial"].ToString(),
                        AnioPublicacion = (int)reader["AnioPublicacion"],
                        Paginas = (int)reader["Paginas"],
                        Categoria = reader["Categoria"].ToString(),
                        Material = reader["Material"].ToString(),
                        Copias = (int)reader["Copias"]
                    };
                }
            }

            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                return View(libro);
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Libros SET Titulo = @Titulo, Autor = @Autor, Tema = @Tema, 
                                Editorial = @Editorial, AnioPublicacion = @AnioPublicacion, Paginas = @Paginas, 
                                Categoria = @Categoria, Material = @Material, Copias = @Copias 
                                WHERE Id = @Id";
                
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", libro.Id);
                cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
                cmd.Parameters.AddWithValue("@Autor", libro.Autor);
                cmd.Parameters.AddWithValue("@Tema", libro.Tema);
                cmd.Parameters.AddWithValue("@Editorial", libro.Editorial);
                cmd.Parameters.AddWithValue("@AnioPublicacion", libro.AnioPublicacion);
                cmd.Parameters.AddWithValue("@Paginas", libro.Paginas);
                cmd.Parameters.AddWithValue("@Categoria", libro.Categoria);
                cmd.Parameters.AddWithValue("@Material", libro.Material);
                cmd.Parameters.AddWithValue("@Copias", libro.Copias);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: Libro/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Libros WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }
    }
}
