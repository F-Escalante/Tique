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

        public static List<Categorias> SerializarCategorias(DataTable dataTable) { 
        
            
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
                else {

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

                            SqlCommand command4 = new SqlCommand("sp_get_comentarios @p_id_servicio = @id", con);
                            command4.Parameters.Add("@id", SqlDbType.Int);
                            command4.Parameters["@id"].Value = id_servicio;
                            comentarios.Load(command4.ExecuteReader());
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



    }// Fin de la clase.

}