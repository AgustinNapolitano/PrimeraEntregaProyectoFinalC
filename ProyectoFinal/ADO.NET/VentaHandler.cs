using Desafío1;
using PrimeraEntregaProyectoFinal;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validación_de_usuario_.ADO.NET
{
    public class VentaHandler : DbHandler
    {
        public Venta GetVentasByIdUsuario(int IdUsuario)
        {
            List<Venta> venta = new List<Venta>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Connection.Open();
                    sqlCommand.CommandText = "select * from Venta where IdUsuario = @IdUsuario;";

                    sqlCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = sqlCommand;
                    DataTable table = new DataTable();
                    dataAdapter.Fill(table);
                    sqlCommand.Connection.Close();

                    foreach (DataRow row in table.Rows)
                    {
                        Venta venta = new Venta();
                        venta.Id = Convert.ToInt32(row["Id"]);
                        venta.Comentarios = row["Comentarios"]?.ToString();

                        venta.Add(venta);
                    }
                }
            }
        }
        return Venta?.FirstOrDefault();
    }
}
