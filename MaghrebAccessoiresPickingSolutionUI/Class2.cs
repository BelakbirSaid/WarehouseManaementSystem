using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;

namespace MaghrebAccessoiresPickingSolutionUI
{
    public class Class2
    {

        public void Normalize(DataTable dtbl3)
        {
            

            dtbl3.Columns.Add("produ", typeof(decimal), "Quantity*Prix_Unitaire");
            decimal minPum = Convert.ToDecimal(dtbl3.Compute("min([produ])", string.Empty));
            decimal maxPum = Convert.ToDecimal(dtbl3.Compute("max([produ])", string.Empty));
            decimal minNB = Convert.ToDecimal(dtbl3.Compute("min([Nombre_de_Factures])", string.Empty));
            decimal maxNB = Convert.ToDecimal(dtbl3.Compute("max([Nombre_de_Factures])", string.Empty));



            // normalisaton des variables 
            dtbl3.Columns.Add("produN", typeof(decimal));
            decimal val1;

            for (int j1 = 0; j1 < dtbl3.Rows.Count; j1++)
            {
                val1 = Convert.ToDecimal(dtbl3.Rows[j1]["produ"].ToString());
                if (dtbl3.Rows.Count <= 1 || maxPum == minPum) {
                    dtbl3.Rows[j1]["produN"] = (decimal)1; }
                else { 

                dtbl3.Rows[j1]["produN"] = (decimal)((val1 - minPum) / (maxPum - minPum));
                }
            }
            //

            dtbl3.Columns.Add("NBN", typeof(decimal));
            decimal val2;

            for (int j2 = 0; j2 < dtbl3.Rows.Count; j2++)
            {
                val2 = Convert.ToDecimal(dtbl3.Rows[j2]["Nombre_de_Factures"].ToString());
                if (dtbl3.Rows.Count <= 1 || minNB == maxNB) { dtbl3.Rows[j2]["NBN"] = 1; }
                else { 
                    dtbl3.Rows[j2]["NBN"] = (decimal)((val2 - minNB) / (maxNB - minNB));
                }
            }

            

        }

        public void Combine(DataTable table)

        {

            table.Columns.Add("VarCom", typeof(decimal), "200*produN  + 100*NBN");

        }
        public void Rank(DataTable table)
        {
            try
            {

                table.DefaultView.Sort = "VarCom Desc";
                table = table.DefaultView.ToTable(true);
                /*
                table.Columns.Add("Rank1", typeof(int));
                for (int j3 = 0; j3 < table.Rows.Count; j3++)
                {

                    table.Rows[j3]["Rank1"] = j3;
                }*/


            }catch ( Exception e)
            {
                MessageBox.Show("ER",e.Message);
            }
            
        }


       

    }
}
