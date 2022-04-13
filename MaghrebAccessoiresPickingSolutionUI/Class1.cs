using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MaghrebAccessoiresPickingSolutionUI
{
    public class Alass1
    {
        SqlConnection cn; 

        public Alass1(String  connectionString)
        {
            cn = new SqlConnection(connectionString);

        } 

        public bool Isconnection
        {
            get
            {
                if (cn.State == System.Data.ConnectionState.Closed)
                    cn.Open();
                return true; 
            }
        }

    }
    





    
}
