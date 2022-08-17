using Desafío1;
using PrimeraEntregaProyectoFinal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validación_de_usuario_.ADO.NET
{
    public class UsuarioHandler : DbHandler 
    {
        public UsuarioHandler GetByName(string nombreUsuario)
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.Connection.Open();
                    sqlCommand.CommandText = "select * from Usuario where NombreUsuario = @nombreUsuario;";

                    sqlCommand.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter();
                    dataAdapter.SelectCommand = sqlCommand;
                    DataTable table = new DataTable();
                    dataAdapter.Fill(table);
                    sqlCommand.Connection.Close();
                    foreach (DataRow row in table.Rows)
                    {
                        Usuario usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(row["Id"]);
                        usuario.Nombre = row["Nombre"]?.ToString();
                        usuario.Apellido = row["Apellido"]?.ToString();
                        usuario.NombreUsuario = row["NombreUsuario"]?.ToString();
                        usuario.Mail = row["Mail"]?.ToString();
                        usuario.Contraseña = row["Contraseña"]?.ToString();

                        usuario.Add(usuario);

                    }
                }
            }

            return usuarios?.FirstOrDefault();
        }
        public List<Usuario> GetUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(
                    "SELECT * FROM Usuario", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        // Me aseguro que haya filas que leer
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Usuario usuario = new Usuario();

                                usuario.Id = Convert.ToInt32(dataReader["Id"]);
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario.Nombre = dataReader["Nombre"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.Contraseña = dataReader["Contraseña"].ToString();
                                usuario.Mail = dataReader["Mail"].ToString();
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }

            return usuarios;
        }

        public void Delete(Usuario usuario)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryDelete = "DELETE FROM Usuario WHERE Id = @idUsuario";

                    SqlParameter parametro = new SqlParameter();
                    parametro.ParameterName = "idUsuario";
                    parametro.SqlDbType = System.Data.SqlDbType.BigInt;
                    parametro.Value = usuario.Id;

                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(parametro);
                        sqlCommand.ExecuteNonQuery(); // Se ejecuta el delete
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateContraseña(Usuario usuario)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryUpdate = "UPDATE [SistemaGestion].[dbo].[Usuario] SET Contraseña = @nuevaContraseña WHERE Id = @idUsuario;";

                    SqlParameter parametroNuevaContraseña = new SqlParameter();
                    parametroNuevaContraseña.ParameterName = "nuevaContraseña";
                    parametroNuevaContraseña.SqlDbType = System.Data.SqlDbType.VarChar;
                    parametroNuevaContraseña.Value = usuario.Contraseña;

                    SqlParameter parametroUsuarioId = new SqlParameter();
                    parametroUsuarioId.ParameterName = "idUsuario";
                    parametroUsuarioId.SqlDbType = System.Data.SqlDbType.BigInt;
                    parametroUsuarioId.Value = usuario.Id;

                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(parametroUsuarioId);
                        sqlCommand.Parameters.Add(parametroNuevaContraseña);
                        sqlCommand.ExecuteNonQuery(); // Se ejecuta el update
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Comunmente llamado "Add"
        public void Insert(Usuario usuario)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[Usuario] " +
                        "(Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES " +
                        "('Rodrigo', 'Perez', 'rperez', 'ContraseñaDePerez', 'RPerez@gmail.com');";

                    SqlParameter nombreParameter = new SqlParameter("Nombre", SqlDbType.VarChar) { Value = usuario.Nombre };
                    SqlParameter apellidoParameter = new SqlParameter("Apellido", SqlDbType.VarChar) { Value = usuario.Apellido };
                    SqlParameter nombreUsuarioParameter = new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                    SqlParameter contraseñaParameter = new SqlParameter("Contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña };
                    SqlParameter mailParameter = new SqlParameter("Mail", SqlDbType.VarChar) { Value = usuario.Mail };


                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(nombreParameter);
                        sqlCommand.Parameters.Add(apellidoParameter);
                        sqlCommand.Parameters.Add(nombreUsuarioParameter);
                        sqlCommand.Parameters.Add(contraseñaParameter);
                        sqlCommand.Parameters.Add(mailParameter);

                        sqlCommand.ExecuteScalar(); // Se ejecuta la sentencia sql
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
