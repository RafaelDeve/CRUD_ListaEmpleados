using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ListaEmpleados.Pages.Empleados
{
    public class CrearEmpleado : PageModel
    {   
        public class Crear: PageModel{

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
    

            public void OnGet()
            {
            }
    }
}
}