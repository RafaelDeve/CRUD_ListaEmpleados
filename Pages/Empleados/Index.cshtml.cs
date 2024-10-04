using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace ListaEmpleados.Pages.Empleados
{
    public class Index : PageModel
    {
        public List<EmpleadoInfo> ListaEmpleado { get; set; } = new List<EmpleadoInfo>();

        public void OnGet()
        {
            try {

                string connectionString = "Server=localhost,1433;Database=ListaEmpleadosDB;User Id=SA;Password=MyPass@word;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString)){
                    connection.Open();

                    string sql = "SELECT * FROM empleados ORDER BY id DESC";
                    using(SqlCommand command = new SqlCommand(sql, connection)){
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()){
                                
                                EmpleadoInfo empleadoInfo = new EmpleadoInfo {
                                    Id = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Apellido = reader.GetString(2),
                                    CorreoElectronico = reader.GetString(3),
                                    Puesto = reader.GetString(4),
                                    Salario = reader.GetDecimal(5),
                                    FechaDeInicio = reader.GetDateTime(6),
                                    EstadoEmpleo = reader.GetString(7)
                                };

                                ListaEmpleado.Add(empleadoInfo);
                            }
                        }
                    }
                }
            }
            catch(Exception ex) {
                Console.WriteLine("Tenemos un error:" + ex.Message);
            }
        }
    }

    public class EmpleadoInfo
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string CorreoElectronico { get; set; }
        public required string Puesto { get; set; }
        public decimal Salario { get; set; }
        public DateTime FechaDeInicio { get; set; }
        public required string EstadoEmpleo { get; set; }
    }
}
