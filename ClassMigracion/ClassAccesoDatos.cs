using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;


namespace ClassMigracion
{
    public class ClassAccesoDatos
    {
        private MySqlConnection ConnMysqlConnection = null;
        private SqlConnection SqlServerConnection = null;

        public ClassAccesoDatos()
        {
            string Mysql = ConfigurationManager.ConnectionStrings["Conn_Mysql"].ConnectionString;
            string SqlServer = ConfigurationManager.ConnectionStrings["Conn_SqlServer"].ConnectionString;

            ConnMysqlConnection = new MySqlConnection(Mysql);
            SqlServerConnection = new SqlConnection(SqlServer);
        }
        public MySqlDataReader ConsultasMysql(string query)
        {
            MySqlCommand command = null; // pasar como parametro la consulta y la conexión abierta
            MySqlDataReader reader = null;
            try
            {
                ConnMysqlConnection.Open();
                command = new MySqlCommand(query, ConnMysqlConnection);
                reader = command.ExecuteReader();
            }
            catch
            {
                reader = null;
            }
            finally
            {
                command.Dispose();
            }
            return reader;
        }


        public bool InsertarSqlServer(string query)
        {
            SqlCommand comando = null;
            try
            {
                SqlServerConnection.Open();
                comando = new SqlCommand(query, SqlServerConnection);
                comando.ExecuteNonQuery();
                SqlServerConnection.Close();
                //return "Insertción Realizada";
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
