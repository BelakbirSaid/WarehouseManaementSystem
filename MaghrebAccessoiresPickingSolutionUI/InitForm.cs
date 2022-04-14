using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; 
using System.Threading;

namespace MaghrebAccessoiresPickingSolutionUI
{
    public partial class InitForm : Form
    {
        public InitForm()
        {
            InitializeComponent();
        }

        // public List<playes> myListOfPlayers = new List<players>();
        //public string[] levels1 = {"2","1","3"};
        public string[] levels = { "1"};
        

        public string[] coul1 = { "5", "2", "1", "3", "6", "4", "7", "8", "9", "10", "11", "12", "13" };


        public DataTable Picking;

            
        public DataTable SitAP;



        private void InitForm_Load(object sender, EventArgs e)
        {
            //
            this.button1.Visible = false; 

           // this.textBox1.Text = levels[0];
           

        }


        public static decimal Sum(DataTable dtbl,int i)
        {
            decimal S = 0;
            for (int j1 = 0; j1 < i ; j1++)
            {
                S+= Convert.ToDecimal(dtbl.Rows[j1]["Laroccu"].ToString()); 
            }
            return S; 

        }
        public static DataTable Copyfromi(DataTable dtbl , int i  )
        {
            DataTable dtbl1 = new DataTable();

            dtbl1 = dtbl.AsEnumerable().Skip(i).Take(dtbl.Rows.Count - i ).CopyToDataTable();

            return dtbl1; 
        }

      
        public void UpdateAisle(string A, string M )
        {
        
            try
            {
                    DataTable dtbl2; // premier tableau avant normalisation 
                    DataTable dtbl4; // output normilasation
                    DataTable dtbl5;//output combinaison ( rank ) 
                    DataTable dtbl6;
                    DataTable dtbl6Copy;//ranked no picking 
                    DataTable superC; // classe picking 
                    DataTable Empldtbl; //Table des emplacement pour chaque allée
                    DataTable buf;

                    string connectionString = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = copydb;";
                    string connectionString1 = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = EmpOptimisation;";

                    // normalisation
                    using (SqlConnection connection2 = new SqlConnection(connectionString))
                    {
                        string sqQueryC = "SELECT  t1.ItemCode AS reference ,t2.U_emp AS EmpAct , SUM(t3.Quantity)/(Count(t3.DocEntry)+1) AS TPALL , t1.OnHand AS Stock_actuel,Sum(t3.Quantity)*4/12 AS QT3Mois,sum(t3.Quantity) AS Quantity, AVG(t3.PriceBefDi) Prix_Unitaire , count(t3.ItemCode) AS Nombre_de_Factures, t1.U_Largeur AS lar , t1.U_Longueur AS prof , t1.U_Hauteur AS haut , t1.U_F_Art AS Famille,t1.U_CodeMarque AS CM,t1.ItemName AS Dsc from oitm t1 join oitw t2 on t1.ItemCode = t2.ItemCode right join inv1 t3 on t1.ItemCode = t3.ItemCode where t2.WhsCode = '01' and t2.U_emp like 'MAG" + M + "-A" + A + "-%' and  t3.DocDate between '2021' and '2022' group by  t1.ItemCode , t1.OnHand ,t1.U_Largeur , t1.U_Longueur , t1.U_Hauteur , t1.U_F_Art ,t2.U_emp  , t1.U_CodeMarque,t1.ItemName";
                        SqlCommand command3 = new SqlCommand(sqQueryC, connection2);
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command3);
                        // DataTable dtbl3 = new DataTable();
                        DataTable tableP = new DataTable();
                        dataAdapter.Fill(tableP);
                        dtbl2 = tableP;

                        if (dtbl2.Rows.Count == 0)
                        {
                        //generate etat

                        }
                        else
                        {


                            DataTable dtbl0N = new DataTable();
                            dtbl0N = dtbl2;
                            Class2 meth = new Class2();
                            meth.Normalize(dtbl0N);

                            dtbl4 = dtbl0N;

                            Class2 mCom = new Class2();
                            mCom.Combine(dtbl4);
                            dtbl5 = dtbl4;

                            Class2 mCom1 = new Class2();
                            mCom1.Rank(dtbl5);

                            dtbl6 = dtbl5;


                            dtbl6.Columns.Add("Laroccu", typeof(decimal));


                            dtbl6.DefaultView.Sort = "VarCom Desc";
                            dtbl6 = dtbl6.DefaultView.ToTable(true);


                            decimal NumbinPall;
                            DataTable dtba = new DataTable();
                            // separation des cathégories 
                            dtba = dtbl6.Copy();

                            for (int j4 = 0; j4 < dtbl6.Rows.Count; j4++)
                            {
                                NumbinPall = Convert.ToDecimal(dtbl6.Rows[j4]["TPALL"].ToString());
                                DataRow recordItem = dtbl6.Rows[j4];
                                // DataRow recLow = dtba.Rows[j4];



                                if (NumbinPall >= 8)
                                {

                                    recordItem.Delete();

                                }

                            }
                            dtbl6.AcceptChanges();

                            for (int j4 = 0; j4 < dtba.Rows.Count; j4++)
                            {
                                NumbinPall = Convert.ToDecimal(dtba.Rows[j4]["TPALL"].ToString());
                                DataRow recordItem1 = dtba.Rows[j4];
    
                                if (NumbinPall < 8)
                                {

                                recordItem1.Delete();
                                }

                            }

                            dtba.AcceptChanges();
                            superC = dtba;
                            Picking = superC;

                           // Picking.AcceptChanges();

                            //this.dataGridView2.DataSource =superC ;

                            dtbl6.DefaultView.Sort = "VarCom Desc";
                            dtbl6 = dtbl6.DefaultView.ToTable(true);

                            using (SqlConnection connection3 = new SqlConnection(connectionString1))
                            {
                                string sqQueryEmpl = "select [Emp] ,[MAG] ,[A] ,[C] ,[H] ,[Index] AS indice,[Distance],[Hauteur],[Longueur],[Profondeur],[Classe] from  siteAP where MAG like 'MAG" + M+ "%' and A like 'A0" +A+ "       %' order by [Index]";
                                SqlCommand command4 = new SqlCommand(sqQueryEmpl, connection3);
                                SqlDataAdapter dataAdapter4 = new SqlDataAdapter(command4);
                   
                                DataTable tableEMP = new DataTable();
                                dataAdapter4.Fill(tableEMP);
                                Empldtbl = tableEMP;

                            }


                            Empldtbl.DefaultView.Sort = "indice";
                            Empldtbl = Empldtbl.DefaultView.ToTable(true);
                         

                            int emp = 0; // initialization 
                            int refe = 0;
                            decimal largempl;
                            decimal hautEmp;
                            decimal ProfEmp;
                            decimal lar1;
                            decimal haut1;
                            decimal prof1;
                            decimal TMothsv;//QT3Mois
                            decimal largref;

                            while (emp < Empldtbl.Rows.Count) // condition
                            {

                                largempl = Convert.ToDecimal(Empldtbl.Rows[emp]["Longueur"].ToString());
                                hautEmp = Convert.ToDecimal(Empldtbl.Rows[emp]["Hauteur"].ToString());
                                ProfEmp = Convert.ToDecimal(Empldtbl.Rows[emp]["Profondeur"].ToString());
                                largref = 0;
                                while (refe < dtbl6.Rows.Count) // condition
                                {


                                    lar1 = Convert.ToDecimal(dtbl6.Rows[refe]["lar"].ToString());
                                    prof1 = Convert.ToDecimal(dtbl6.Rows[refe]["prof"].ToString());
                                    haut1 = Convert.ToDecimal(dtbl6.Rows[refe]["haut"].ToString());
                                    TMothsv = Convert.ToDecimal(dtbl6.Rows[refe]["QT3Mois"].ToString());


                                    largref = (decimal)((decimal)0.75 * lar1 * TMothsv / (((decimal)hautEmp / haut1) * ((decimal)ProfEmp / prof1)));
                                    dtbl6.Rows[refe]["Laroccu"] = (decimal)largref;
                                    refe++;


                                    //dtbl6.Rows[refe]["EmplOptimal"] = Empldtbl.Rows[emp]["Emplacement"].ToString();

                                }
                                emp++; // increment
                            }


                            // affectation ep ca
                            dtbl6.Columns.Add("EmplOptimal", typeof(string));
                            dtbl6.Columns.Add("classe", typeof(string));



                            dtbl6.DefaultView.Sort = "VarCom Desc";

                            dtbl6 = dtbl6.DefaultView.ToTable(true);
                            // dataGridView1.DataSource = Empldtbl;

                            dtbl6Copy = dtbl6.Copy();

                            int j = 0, s = 0, t = 0;

                            while (j < Empldtbl.Rows.Count)//loop on emp
                            {
                                for (int i = 0; i < dtbl6Copy.Rows.Count; i++) //loop ref 
                                {
                                    if (Sum(dtbl6Copy, i + 1) > 2250)
                                    {
                                        for (int k = s; k < s + i; k++)
                                        {
                                            dtbl6.Rows[k]["EmplOptimal"] = Empldtbl.Rows[t]["Emp"].ToString();
                                            dtbl6.Rows[k]["classe"] = Empldtbl.Rows[t]["Classe"].ToString();

                                        }
                                        s += i;
                                        t += 1;
                                        buf = Copyfromi(dtbl6Copy, i);

                                        dtbl6Copy = buf;
                                        dtbl6Copy.DefaultView.Sort = "VarCom Desc";
                                        dtbl6Copy = dtbl6Copy.DefaultView.ToTable(true);

                                        break;
                                    }
                                    if (Sum(dtbl6Copy, dtbl6Copy.Rows.Count) <= 2250 || i == dtbl6Copy.Rows.Count - 1)
                                    {
                                        for (int k = s; k < dtbl6.Rows.Count; k++)
                                        {
                                            dtbl6.Rows[k]["EmplOptimal"] = Empldtbl.Rows[t]["Emp"].ToString();
                                            dtbl6.Rows[k]["classe"] = Empldtbl.Rows[t]["Classe"].ToString();

                                        }
                                    }

                                }
                                j += 1;


                            }

                        SitAP = dtbl6.Clone();

                    //upoa
                        for (int p = 0; p < dtbl6.Rows.Count; p++)
                        {
                            string connectionString2 = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = EmpOptimisation;";
                            using (SqlConnection connection3 = new SqlConnection(connectionString2))
                            {
                                string reference = dtbl6.Rows[p]["reference"].ToString();
                                string emplacementop = dtbl6.Rows[p]["EmplOptimal"].ToString();
                                string EmplacementAct = dtbl6.Rows[p]["EmpAct"].ToString();
                                string Classe = dtbl6.Rows[p]["classe"].ToString();
                                string qtystock = dtbl6.Rows[p]["Stock_actuel"].ToString();
                                string CM = dtbl6.Rows[p]["CM"].ToString();
                                string Fam = dtbl6.Rows[p]["Famille"].ToString();
                                string desc = dtbl6.Rows[p]["Dsc"].ToString();
                                string blocs = dtbl6.Rows[p]["QT3Mois"].ToString();


                                string sql = "insert into Table_1 values (@name,@emp,@empac,@classe,@stk,@CM,@Famille,@dsc,@boc)";
                                connection3.Open();
                                using (SqlCommand cmd = new SqlCommand(sql, connection3))
                                {
                                    cmd.Parameters.AddWithValue("@name", reference);
                                    cmd.Parameters.AddWithValue("@emp", emplacementop);
                                    cmd.Parameters.AddWithValue("@empac", EmplacementAct);
                                    cmd.Parameters.AddWithValue("@classe", Classe);
                                    cmd.Parameters.AddWithValue("@stk", qtystock);
                                    cmd.Parameters.AddWithValue("@CM", CM);
                                    cmd.Parameters.AddWithValue("@Famille", Fam);
                                    cmd.Parameters.AddWithValue("@dsc", desc);
                                    cmd.Parameters.AddWithValue("@boc", blocs);

                                    // assign value to parameter 
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }



                    }





                    


            }
            catch (Exception ex)


            {
                MessageBox.Show(ex.ToString());
            }


        }
        



        private void button2_Click(object sender, EventArgs e)
        {

            
                Parallel.For(0, coul1.Length, i => {
              

                    UpdateAisle(coul1[i], levels[0]);
                   
                
                });
            this.label1.Text = "done";





        }
    }
}
