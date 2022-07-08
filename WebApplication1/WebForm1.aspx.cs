using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassMigracion;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        ClassAccesoDatos Datos = new ClassAccesoDatos();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            MySqlDataReader reader = null;
            string query = "SELECT " +
                "docente.id AS ID_Profe, " +
                "empleados.id AS RegistroEmpleado, " +
                "empleados.Nombre AS Nombre, " +
                "empleados.apaterno AS Ap_pat, " +
                "empleados.amaterno AS Ap_Mat, " +
                "IF(empleados.sexo = 1, 'Hombre', 'Mujer' ) AS Genero,  " +
                "docente.puesto AS Categoria, " +
                "empleados.correo AS Correo, " +
                "empleados.telefono AS Celular, " +
                "empleados.estadocivil AS F_EdoCivil " +
                "FROM empleados " +
                "INNER JOIN docente ON(docente.nempleado = empleados.id)";

            reader = Datos.ConsultasMysql(query);

            if (reader != null)
            {
                int contador = 0;
                Boolean bandera = false;
                while (reader.Read())
                {
                    string datos = @"SET IDENTITY_INSERT Profesor ON; 
                    INSERT INTO Profesor(ID_Profe, 
                    RegistroEmpleado, 
                    Nombre, 
                    Ap_pat, 
                    Ap_Mat, 
                    Genero, 
                    Categoria, 
                    Correo, 
                    Celular, 
                    F_EdoCivil) 
                    VALUES (" + reader["ID_Profe"] + ", " + reader["RegistroEmpleado"] + ", '" + reader["Nombre"] + "', '" + reader["Ap_pat"] + "',  '" + reader["Ap_Mat"] + "', '" + reader["Genero"] + "', '" + reader["Categoria"] + "', '" + reader["Correo"] + "', NULL, " + reader["F_EdoCivil"] + ");";
                    bandera = Datos.InsertarSqlServer(datos);
                    if (!bandera)
                        break;
                    contador++;
                }
                if (bandera)
                    Label1.Text = "Se insertaron " + contador + " filas";
                else
                    Label1.Text = "Se interrumpio por que la migración se hizo con anterioridad";
            }
            else
                Label1.Text = "Consulta incorrecta";



        }
    }
}