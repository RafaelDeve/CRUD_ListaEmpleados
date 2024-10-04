using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace ListaEmpleados.Pages.Empleados
{
    public class BorrarEmpleado : PageModel
    {
        

        public void OnGet()
        {
        }

        public void OnPost(int id){
            borrarEmpleado(id);
            Response.Redirect("/Empleados/Index");
        }

        private void borrarEmpleado(int id)
        {
            try {
                string connectionString = "Server=localhost,1433;Database=ListaEmpleadosDB;User Id=SA;Password=MyPass@word;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString)){
                        connection.Open(); 

                        string sql = "DELETE FROM empleados WHERE id=@id";
                        using (SqlCommand command = new SqlCommand(sql, connection)){
                            command.Parameters.AddWithValue("@id", id);
                            command.ExecuteNonQuery();
                        }
                

                }
            }

            catch(Exception ex){
                Console.WriteLine("No se puede borrar empleado: " + ex.Message);
            };
        }

        
    }
}