using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace ListaEmpleados.Pages.Empleados
{
    public class EditarEmpleado : PageModel
    {
            [BindProperty]
            public int  Id { get; set; }

            [BindProperty, Required()]
            public required string  Nombre { get; set; }

            [BindProperty, Required, StringLength(100)]
            public required string Apellido { get; set; }

            [BindProperty, Required, EmailAddress]
            public required string CorreoElectronico { get; set; }

            [BindProperty, Required, StringLength(100)]
            public required string Puesto { get; set; }

            [BindProperty, Range(0, double.MaxValue)]
            public decimal Salario { get; set; }

            [BindProperty, DataType(DataType.Date)]
            public DateTime FechaDeInicio { get; set; }

            [BindProperty, Required]
            public required string EstadoEmpleo { get; set; }
        public string ErrorMessage { get; private set; } = "";

        public void OnGet(int id)
        {
            try {
                string connectionString = "Server=localhost,1433;Database=ListaEmpleadosDB;User Id=SA;Password=MyPass@word;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString)){
                        connection.Open();

                string sql = "SELECT * FROM empleados WHERE id=@id";
                using (SqlCommand command = new SqlCommand(sql, connection)) {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command.ExecuteReader()) {
                            if (reader.Read()){
                                {
                                    Id = reader.GetInt32(0);
                                    Nombre = reader.GetString(1);
                                    Apellido = reader.GetString(2);
                                    CorreoElectronico = reader.GetString(3);
                                    Puesto = reader.GetString(4);
                                    Salario = reader.GetDecimal(5);
                                    FechaDeInicio = reader.GetDateTime(6);
                                    EstadoEmpleo = reader.GetString(7);
                                };

                                
                            }
                            else {
                                Response.Redirect("/Empleados/Index");
                            }
                }

            }}}

            catch(Exception ex) {
                ErrorMessage = ex.Message;
            }
        }

        public void OnPost(){
            if (!ModelState.IsValid){
                return;
            }

            try {
                string connectionString = "Server=localhost,1433;Database=ListaEmpleadosDB;User Id=SA;Password=MyPass@word;TrustServerCertificate=True;";

                using (SqlConnection connection = new SqlConnection(connectionString)){
                        connection.Open();

                
                string sql = "UPDATE empleados SET nombre=@nombre, apellido=@apellido, correo_electronico=@correo_electronico, puesto=@puesto, salario=@salario, " 
             + "fecha_de_inicio=@fecha_de_inicio, estado_empleo=@estado_empleo WHERE id=@id;";


                using (SqlCommand command = new SqlCommand(sql, connection)) {
                            command.Parameters.AddWithValue("@nombre", Nombre);
                            command.Parameters.AddWithValue("@apellido", Apellido);
                            command.Parameters.AddWithValue("@correo_electronico", CorreoElectronico);
                            command.Parameters.AddWithValue("@puesto", Puesto);
                            command.Parameters.AddWithValue("@salario", Salario);
                            command.Parameters.AddWithValue("@fecha_de_inicio", FechaDeInicio);
                            command.Parameters.AddWithValue("@estado_empleo", EstadoEmpleo);
                            command.Parameters.AddWithValue("@id", Id);

                            command.ExecuteNonQuery();

                        }
                }

                
            }


            catch(Exception ex){
                ErrorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Empleados/Index");
            
        }
    }
}