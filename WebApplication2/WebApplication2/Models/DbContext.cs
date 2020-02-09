using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WebApplication2.Models;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Collections;


namespace WebApplication2.Models
{
    public class Database
    {
        // Declaración de instancias.
        private string connectionString;

        /// <summary>
        /// Método constructor sin parámetros.
        /// </summary>
        public Database()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString; // Se obtiene la cadena de conexión a la base de datos.
        }//Fin del método constructor.

        public static List<Categorias> getCategorias()
        {
            DataTable dataTable = new DataTable();
            List<Categorias> categoriasList = new List<Categorias>();
            //A TRAVEZ DE LA CADENA DE CONEXION DEL WEBCONFIG Y LA OBTENEMOS  
            //CON EL CONFIGURATIONMANAGER 
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        //SE ABRE LA CONEXION 
                        con.Open();

                        //SE UTILIZA PARA LLENAR UN DATASET CON LOS ELEMENTOS NECESARIOS  
                        //COMO UNA CONEXION DE BASE DE DATOS 
                        SqlCommand command = new SqlCommand("sp_get_categorias @p_id_categoria_padre = @id", con);
                        command.Parameters.Add("@id", SqlDbType.Int);
                        command.Parameters["@id"].Value = 0;
                        dataTable.Load(command.ExecuteReader());

                        categoriasList = SerializarCategorias(dataTable);

                    }
                    catch (Exception x)
                    {

                    }
                    finally
                    {

                        con.Close();

                    }

                }
            }
            //REGRESAMOS LOS DATOS COMO DATOS EN MEMORIA                                     

            return categoriasList;
        }

        public static List<Categorias> getCategorias(int id)
        {
            //EL DataSet REPRESENTA UNA MEMORIA CACHÉ DE DATOS EN MEMORIA 
            DataTable dataTable = new DataTable();
            List<Categorias> categoriasList = new List<Categorias>();

            //A TRAVEZ DE LA CADENA DE CONEXION DEL WEBCONFIG Y LA OBTENEMOS  
            //CON EL CONFIGURATIONMANAGER 
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {
                try
                {
                    //SE ABRE LA CONEXION 
                    con.Open();

                    //SE UTILIZA PARA LLENAR UN DATASET CON LOS ELEMENTOS NECESARIOS  
                    //COMO UNA CONEXION DE BASE DE DATOS 
                    //   using (SqlDataAdapter sqlAdapter = new SqlDataAdapter())

                    SqlCommand command = new SqlCommand("sp_get_categorias @p_id_categoria_padre = @id", con);
                    command.Parameters.Add("@id", SqlDbType.Int);
                    command.Parameters["@id"].Value = id;

                    dataTable.Load(command.ExecuteReader());
                    categoriasList = SerializarCategorias(dataTable);


                }
                catch (Exception x)
                {

                }
                finally
                {

                    con.Close();

                }


            }
            //REGRESAMOS LOS DATOS COMO DATOS EN MEMORIA 

            return categoriasList;
        }

        public static List<Categorias> SerializarCategorias(DataTable dataTable)
        {


            List<Categorias> categoriasList = new List<Categorias>();
            //List<Object> categoriasList = new List<System.Object>();
            int i = 0;
            foreach (DataRow fila in dataTable.Rows)
            {

                Categorias cat = new Categorias();

                cat.code = Convert.ToInt16(dataTable.Rows[i]["id_categoria"]);
                cat.description = dataTable.Rows[i]["nombre"].ToString();

                //cat.id_categoria_padre = Convert.ToInt16(dataTable.Rows[i]["id_categoria_padre"]);
                if (Convert.ToInt16(dataTable.Rows[i]["id_categoria_padre"]) == 0)
                {

                    cat.isSon = false;
                }
                else
                {

                    cat.isSon = true;

                }

                cat.icon = dataTable.Rows[i]["icono"].ToString().Trim();

                i++;
                categoriasList.Add(cat);

            }

            return categoriasList;
        }

        public static List<Servicios> getServicios(int id_categoria)
        {
            DataTable dataTable = new DataTable();
            List<Servicios> serviciosList = new List<Servicios>();
            //A TRAVEZ DE LA CADENA DE CONEXION DEL WEBCONFIG Y LA OBTENEMOS  
            //CON EL CONFIGURATIONMANAGER 
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        //SE ABRE LA CONEXION 
                        con.Open();

                        //SE UTILIZA PARA LLENAR UN DATASET CON LOS ELEMENTOS NECESARIOS  
                        //COMO UNA CONEXION DE BASE DE DATOS 
                        SqlCommand command = new SqlCommand("sp_get_servicios @p_id_categoria = @id", con);
                        command.Parameters.Add("@id", SqlDbType.Int);
                        command.Parameters["@id"].Value = id_categoria;
                        dataTable.Load(command.ExecuteReader());

                        int i = 0;
                        foreach (DataRow fila in dataTable.Rows)
                        {

                            Servicios servicio = new Servicios();

                            servicio.code = Convert.ToInt16(dataTable.Rows[i]["id_servicio"]);
                            servicio.name = dataTable.Rows[i]["nombre"].ToString();
                            servicio.addres = dataTable.Rows[i]["direccion"].ToString();

                            servicio.avatar_image = dataTable.Rows[i]["imagen"].ToString();


                            servicio.avatar_image = dataTable.Rows[i]["imagen_avatar"].ToString();
                                                        

                            i++;
                            serviciosList.Add(servicio);

                        }

                        return serviciosList;

                    }
                    catch (Exception x)
                    {

                    }
                    finally
                    {

                        con.Close();

                    }

                }
            }
            //REGRESAMOS LOS DATOS COMO DATOS EN MEMORIA                                     

            return serviciosList;
        }

        public static ServiciosDetalle getServiciosDetalle(int id_servicio)
        {
            DataTable dataTable = new DataTable();
            DataTable imagen_account = new DataTable();
            DataTable imagen_atached = new DataTable();
            DataTable comentarios = new DataTable();
            ServiciosDetalle servicioDetalle = new ServiciosDetalle();
           // ServiciosDetalle serviciosDetalleList = new ServiciosDetalle();
            //A TRAVEZ DE LA CADENA DE CONEXION DEL WEBCONFIG Y LA OBTENEMOS  
            //CON EL CONFIGURATIONMANAGER 
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        //SE ABRE LA CONEXION 
                        con.Open();

                        //SE UTILIZA PARA LLENAR UN DATASET CON LOS ELEMENTOS NECESARIOS  
                        //COMO UNA CONEXION DE BASE DE DATOS 
                        SqlCommand command = new SqlCommand("sp_get_servicio_detalle @p_id_servicio = @id", con);
                        command.Parameters.Add("@id", SqlDbType.Int);
                        command.Parameters["@id"].Value = id_servicio;
                        dataTable.Load(command.ExecuteReader());

                        int i = 0;
                        foreach (DataRow fila in dataTable.Rows)
                        {

                            

                            servicioDetalle.code = Convert.ToInt16(dataTable.Rows[i]["id_servicio"]);
                            servicioDetalle.name = dataTable.Rows[i]["nombre"].ToString();
                            servicioDetalle.addres = dataTable.Rows[i]["direccion"].ToString();
                            servicioDetalle.avatar_image = dataTable.Rows[i]["imagen_avatar"].ToString();
                            servicioDetalle.account_image = dataTable.Rows[i]["imagen_account"].ToString();


                            SqlCommand command4 = new SqlCommand("sp_get_imagenes @p_id_servicio = @id", con);
                            command4.Parameters.Add("@id", SqlDbType.Int);
                            command4.Parameters["@id"].Value = id_servicio;
                            imagen_atached.Load(command4.ExecuteReader());

                            servicioDetalle.description = dataTable.Rows[i]["descripcion"].ToString();                            
                            SqlCommand command3 = new SqlCommand("sp_get_imagenes @p_id_servicio = @id", con);
                            command3.Parameters.Add("@id", SqlDbType.Int);
                            command3.Parameters["@id"].Value = id_servicio;
                            imagen_atached.Load(command3.ExecuteReader());

                            int j = 0;
                            List<Imagenes> imagenes = new List<Imagenes>();

                            foreach (DataRow fila2 in imagen_atached.Rows)
                            {
                                Imagenes imagenes_atached = new Imagenes();
                                imagenes_atached.code = Convert.ToInt16(imagen_atached.Rows[j]["id_imagen"]);
                                imagenes_atached.image = imagen_atached.Rows[j]["imagen"].ToString();
                                imagenes.Add(imagenes_atached);
                                j++;
                            }

                            servicioDetalle.atached_image = imagenes;

                            SqlCommand command5 = new SqlCommand("sp_get_comentarios @p_id_servicio = @id", con);
                            command5.Parameters.Add("@id", SqlDbType.Int);
                            command5.Parameters["@id"].Value = id_servicio;
                            comentarios.Load(command5.ExecuteReader());
                            int k = 0;
                            List<Comentarios> comentario = new List<Comentarios>();

                            foreach (DataRow fila2 in comentarios.Rows)
                            {
                                Comentarios comments = new Comentarios();
                                comments.user_name = comentarios.Rows[k]["usuario"].ToString();
                                comments.comment = comentarios.Rows[k]["comentario"].ToString();
                                comments.stars = Convert.ToInt16(comentarios.Rows[k]["puntuacion"]);
                                comments.date = Convert.ToDateTime(comentarios.Rows[k]["fecha"]);
                                comentario.Add(comments);
                                k++;
                            }

                            servicioDetalle.comments = comentario;

                            i++;
                           // serviciosDetalleList.Add(servicioDetalle);

                        }

                         return servicioDetalle;

                    }
                    catch (Exception x)
                    {

                    }
                    finally
                    {

                        con.Close();

                    }

                }
            }
            //REGRESAMOS LOS DATOS COMO DATOS EN MEMORIA                                     

            return servicioDetalle;
        }
        public static LoginSalida getLogin(string email, string nombre, string telefono, string device_id)
        {
            DataTable dataTable = new DataTable();
            LoginSalida login = new LoginSalida();


            List<Roles> listRoles = new List<Roles>();
            //A TRAVEZ DE LA CADENA DE CONEXION DEL WEBCONFIG Y LA OBTENEMOS  
            //CON EL CONFIGURATIONMANAGER 
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        
                        if (existeUsuario(email) == true)
                        {
                            SqlCommand command2 = new SqlCommand("sp_ins_usuario", con);
                            command2.CommandType = CommandType.StoredProcedure;
                            command2.Parameters.AddWithValue("@nombre", nombre);
                            command2.Parameters.AddWithValue("@telefono", telefono);
                            command2.Parameters.AddWithValue("@email", email);
                            con.Open();
                            command2.ExecuteNonQuery();
                        }


                        if (existeDispositivo(email, device_id) == true)
                        {
                            //con.Close();
                            agregarDispositivo(email, device_id);

                        }


                        SqlCommand command4 = new SqlCommand("sp_get_roles @email = @email", con);
                        command4.Parameters.Add("@email", SqlDbType.VarChar);
                        command4.Parameters["@email"].Value = email;
                        // if (conection.State != ConnectionState.Open)
                        if (con.State != ConnectionState.Open) 
                        {
                            con.Open();

                        }
                        dataTable.Load(command4.ExecuteReader());
                        
                        int i = 0;
                        foreach (DataRow fila in dataTable.Rows)
                        {
                            Roles roles = new Roles();
                            roles.rol_name = dataTable.Rows[i]["nombre"].ToString();
                            roles.value = true;
                            listRoles.Add(roles);
                            i++;
                        }

                        login.roles = listRoles;

                    }
                    catch (Exception x)
                    {

                    }
                    finally
                    {

                        con.Close();

                    }

                }
            }
            //REGRESAMOS LOS DATOS COMO DATOS EN MEMORIA                                     

            return login;

        }

        public static void agregarDispositivo(string email, string device_id)
        {
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {   
                            SqlCommand command5 = new SqlCommand("sp_ins_dispositivo", con);
                            command5.CommandType = CommandType.StoredProcedure;
                            command5.Parameters.AddWithValue("@device_id", device_id);
                            command5.Parameters.AddWithValue("@email", email);
                            con.Open();
                            command5.ExecuteNonQuery();
                        
                    }
                    catch (Exception x)
                    {

                    }
                    finally
                    {

                        con.Close();

                    }

                }
            }
        }

        public static bool existeUsuario(string email)
        {
            //A TRAVEZ DE LA CADENA DE CONEXION DEL WEBCONFIG Y LA OBTENEMOS  
            //CON EL CONFIGURATIONMANAGER 
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        //SE ABRE LA CONEXION 
                        con.Open();
                        SqlCommand command = new SqlCommand("Select * from Buscador_Servicios.dbo.usuarios where email = @email", con);
                        command.Parameters.AddWithValue("@email", email);
                        SqlDataReader dataReader = command.ExecuteReader();
                        if (!dataReader.Read())
                        {
                            return true;
                        }
                    }
                    catch (Exception x)
                    {

                    }
                    finally
                    {

                        con.Close();

                    }
                }
            }
            return false;
        }

        public static bool existeDispositivo(string email, string device_id)
        {
            //A TRAVEZ DE LA CADENA DE CONEXION DEL WEBCONFIG Y LA OBTENEMOS  
            //CON EL CONFIGURATIONMANAGER 
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        //SE ABRE LA CONEXION 
                        con.Open();
                        SqlCommand command3 = new SqlCommand("Select * from Buscador_Servicios.dbo.dispositivos where email = @email and device_id = @device_id", con);
                        command3.Parameters.AddWithValue("@email", email);
                        command3.Parameters.AddWithValue("@device_id", device_id);

                        //con.Open();
                        SqlDataReader dataReader2 = command3.ExecuteReader();

                        if (!dataReader2.Read())
                        {
                            return true;
                        }
                    }
                    catch (Exception x)
                    {

                    }
                    finally
                    {

                        con.Close();

                    }
                }
            }
            return false;
        }

        public static bool desloguear(string email, string device_id) 
        {
            //A TRAVEZ DE LA CADENA DE CONEXION DEL WEBCONFIG Y LA OBTENEMOS  
            //CON EL CONFIGURATIONMANAGER 
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cs"]
                .ConnectionString))
            {

                {
                    try
                    {
                        //SE ABRE LA CONEXION 
                        con.Open();
                        SqlCommand command = new SqlCommand("UPDATE Buscador_Servicios.dbo.dispositivos set activo = 0 where email = @email and device_id = @device_id", con);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@device_id", device_id);
                        command.ExecuteNonQuery();
                        
                    }
                    catch (SqlException x)
                    {
                        Console.WriteLine(x);
                        return false;
                    }
                    finally
                    {

                        con.Close();

                    }
                }
            }
            return true;
        }

    }// Fin de la clase.
}
