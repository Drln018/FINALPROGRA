using MySql.Data.MySqlClient;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EXAMEN_PP.clases
{
    class ClsConexion
    {
        SqlConnection conex;

        public ClsConexion(string username)
        {
        }

        public void Conectar()
        {
            conex = new SqlConnection("Data Source=(Localdb)\\Darlin;Initial Catalog=basedatos_final;Integrated Security=True");
            conex.Open();
        }

        public void Desconectar()
        {
            conex.Close();
        }
    }
}


    
