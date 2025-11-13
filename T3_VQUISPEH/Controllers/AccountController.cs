using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using T3_VQUISPEH.Models;

namespace T3_VQUISPEH.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string contraseña)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(contraseña))
            {
                ViewBag.Error = "Por favor ingrese email y contraseña";
                return View();
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Usuarios WHERE Email = @Email AND Contraseña = @Contraseña";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Contraseña", contraseña);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Crear claims del usuario
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, reader["Nombre"].ToString()),
                        new Claim(ClaimTypes.Email, email),
                        new Claim("UsuarioId", reader["Id"].ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    // Login exitoso - crear cookie de autenticación
                    await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);
                    
                    HttpContext.Session.SetString("UsuarioNombre", reader["Nombre"].ToString());
                    HttpContext.Session.SetString("UsuarioEmail", email);
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Email o contraseña incorrectos";
                    return View();
                }
            }
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public IActionResult Register(string nombre, string email, string contraseña)
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(contraseña))
            {
                ViewBag.Error = "Todos los campos son obligatorios";
                return View();
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Verificar si el email ya existe
                string checkQuery = "SELECT COUNT(*) FROM Usuarios WHERE Email = @Email";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@Email", email);

                conn.Open();
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    ViewBag.Error = "El email ya está registrado";
                    return View();
                }

                // Insertar nuevo usuario
                string insertQuery = "INSERT INTO Usuarios (Nombre, Email, Contraseña) VALUES (@Nombre, @Email, @Contraseña)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@Nombre", nombre);
                insertCmd.Parameters.AddWithValue("@Email", email);
                insertCmd.Parameters.AddWithValue("@Contraseña", contraseña);

                insertCmd.ExecuteNonQuery();
                ViewBag.Success = "Registro exitoso. Ahora puedes iniciar sesión.";
            }

            return View();
        }

        // GET: Account/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
