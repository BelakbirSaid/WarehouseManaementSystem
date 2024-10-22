﻿using System;
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

        public string[] levels = { "1"};


        public string[] coul1 = { "05", "02", "01", "03", "06", "04", "07", "08", "09", "10", "11","13" };
       // public string[] coul2 = { };



        DataTable Picking  = new DataTable();
   

        // init Picing 
        


        public DataTable SitAP;



        private void InitForm_Load(object sender, EventArgs e)
        {
            this.button2.Visible = false;
            this.panel2.Visible = false; 
           


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




                            decimal NumbinPall;
                            DataTable dtba = new DataTable();

                            // separation des cathégories 
                            dtba = dtbl6.Copy() ;





                        //this.dataGridView1.DataSource = dtbl6;


                        for (int i = dtbl6.Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow dr = dtbl6.Rows[i];

                            if (Convert.ToDecimal(dr["TPALL"].ToString()) > 8 ) 
                                dr.Delete();
                            dtbl6.AcceptChanges();
                        }


                        for (int i = dtba.Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow dr = dtba.Rows[i];

                            if (Convert.ToDecimal(dr["TPALL"].ToString()) <8 ) 
                                dr.Delete();
                            dtba.AcceptChanges();
                        }




                        //

                        superC = dtba.Clone();
                        
                        //

                       
                           //


                        //up piv

                        
                        
                        for (int p = 0; p < dtba.Rows.Count; p++)
                        {
                            string connectionString2 = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = EmpOptimisation;";
                            using (SqlConnection connection3 = new SqlConnection(connectionString2))
                            {
                                string reference = dtba.Rows[p]["reference"].ToString();  
                                
                                string EmplacementAct = dtba.Rows[p]["EmpAct"].ToString();
                
                                string TPALL = dtba.Rows[p]["TPALL"].ToString();
                                string Stock_actuel = dtba.Rows[p]["Stock_actuel"].ToString();
                                string QT3Mois = dtba.Rows[p]["QT3Mois"].ToString();

                                string Quantity = dtba.Rows[p]["Quantity"].ToString();
                                string Prix_Unitaire = dtba.Rows[p]["Prix_Unitaire"].ToString();
                                string Nombre_de_Factures = dtba.Rows[p]["Nombre_de_Factures"].ToString();
                                string Famille = dtba.Rows[p]["Famille"].ToString();
                                string CM = dtba.Rows[p]["CM"].ToString();



                                string sql = "insert into Table_3 values (@1,@2,@3,@4,@5,@6,@7,@8,@9,@10)";
                                connection3.Open();
                                using (SqlCommand cmd = new SqlCommand(sql, connection3))
                                {
                                    cmd.Parameters.AddWithValue("@1", reference);
      
                                    cmd.Parameters.AddWithValue("@2", EmplacementAct);
                                    cmd.Parameters.AddWithValue("@3", TPALL);
                                    cmd.Parameters.AddWithValue("@4", Stock_actuel);
                                    cmd.Parameters.AddWithValue("@5", QT3Mois);
                                    cmd.Parameters.AddWithValue("@6", Quantity);
                                    cmd.Parameters.AddWithValue("@7", Prix_Unitaire);
                                    cmd.Parameters.AddWithValue("@8", Nombre_de_Factures);
                                    cmd.Parameters.AddWithValue("@9", Famille);
                                    cmd.Parameters.AddWithValue("@10", CM);




                                    // assign value to parameter 
                                    cmd.ExecuteNonQuery();
                                }
                            }

                        }
                        
                        
                        



                        dtbl6.DefaultView.Sort = "VarCom Desc";
                        dtbl6 = dtbl6.DefaultView.ToTable(true);
                            
                        

                            using (SqlConnection connection3 = new SqlConnection(connectionString1))
                            {
                                string sqQueryEmpl = "select [Emp] ,[MAG] ,[A] ,[C] ,[H] ,[Index] AS indice,[Distance],[Hauteur],[Longueur],[Profondeur],[Classe] from  siteAP where MAG like 'MAG" + M+ "%' and A like 'A" +A+ "       %' order by [Index]";
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

                                largempl =(decimal)2250 ;
                                hautEmp = (decimal) 650;
                                ProfEmp = (decimal)500;
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


                        // if occ  >  2250


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

                        //
                        //
                        //
                    }



                    }


               


            }
            catch (Exception ex)


            {
                MessageBox.Show(ex.ToString());
            }


        } //A with 0   app to coul 1 9 
        public void UpdateAisle1(string A, string M)
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
                        



                        // Picking.AcceptChanges();

                        //this.dataGridView2.DataSource =superC ;

                        dtbl6.DefaultView.Sort = "VarCom Desc";
                        dtbl6 = dtbl6.DefaultView.ToTable(true);

                        using (SqlConnection connection3 = new SqlConnection(connectionString1))
                        {
                            string sqQueryEmpl = "select [Emp] ,[MAG] ,[A] ,[C] ,[H] ,[Index] AS indice,[Distance],[Hauteur],[Longueur],[Profondeur],[Classe] from  siteAP where MAG like 'MAG" + M + "%' and A like 'A" + A + "       %' order by [Index]";
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

                            largempl = (decimal)2250;
                            hautEmp = (decimal)650;
                            ProfEmp = (decimal)500;
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


        } // A no 0    app to 10 to 13

        public void UpdateAisle2(string A, string M)
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




                        decimal NumbinPall;
                        DataTable dtba = new DataTable();
                        // separation des cathégories 
                        dtba = dtbl6.Copy();


                        //this.dataGridView1.DataSource = dtbl6;


                        for (int i = dtbl6.Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow dr = dtbl6.Rows[i];

                            if (Convert.ToDecimal(dr["TPALL"].ToString()) > 8)
                                dr.Delete();
                            dtbl6.AcceptChanges();
                        }


                        for (int i = dtba.Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow dr = dtba.Rows[i];

                            if (Convert.ToDecimal(dr["TPALL"].ToString()) < 8)
                                dr.Delete();
                            dtba.AcceptChanges();
                        }




                        //

                        superC = dtba.Clone();

                        //

                        //


                        //upload picking area information 

                       /*
                        
                        for (int p = 0; p < dtba.Rows.Count; p++)
                        {
                            string connectionString2 = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = EmpOptimisation;";
                            using (SqlConnection connection3 = new SqlConnection(connectionString2))
                            {
                                string reference = dtba.Rows[p]["reference"].ToString();

                                string EmplacementAct = dtba.Rows[p]["EmpAct"].ToString();

                                string TPALL = dtba.Rows[p]["TPALL"].ToString();
                                string Stock_actuel = dtba.Rows[p]["Stock_actuel"].ToString();
                                string QT3Mois = dtba.Rows[p]["QT3Mois"].ToString();

                                string Quantity = dtba.Rows[p]["Quantity"].ToString();
                                string Prix_Unitaire = dtba.Rows[p]["Prix_Unitaire"].ToString();
                                string Nombre_de_Factures = dtba.Rows[p]["Nombre_de_Factures"].ToString();
                                string Famille = dtba.Rows[p]["Famille"].ToString();
                                string CM = dtba.Rows[p]["CM"].ToString();


                                
                                string sql = "Update  Table_3  values (@1,@2,@3,@4,@5,@6,@7,@8,@9,@10)";
                                connection3.Open();
                                using (SqlCommand cmd = new SqlCommand(sql, connection3))
                                {
                                    cmd.Parameters.AddWithValue("@1", reference);

                                    cmd.Parameters.AddWithValue("@2", EmplacementAct);
                                    cmd.Parameters.AddWithValue("@3", TPALL);
                                    cmd.Parameters.AddWithValue("@4", Stock_actuel);
                                    cmd.Parameters.AddWithValue("@5", QT3Mois);
                                    cmd.Parameters.AddWithValue("@6", Quantity);
                                    cmd.Parameters.AddWithValue("@7", Prix_Unitaire);
                                    cmd.Parameters.AddWithValue("@8", Nombre_de_Factures);
                                    cmd.Parameters.AddWithValue("@9", Famille);
                                    cmd.Parameters.AddWithValue("@10",
                                    // assign value to parameter 
                                    cmd.ExecuteNonQuery();
                                }
                            }

                        }
                        */


                        dtbl6.DefaultView.Sort = "VarCom Desc";
                        dtbl6 = dtbl6.DefaultView.ToTable(true);



                        using (SqlConnection connection3 = new SqlConnection(connectionString1))
                        {
                            string sqQueryEmpl = "select [Emp] ,[MAG] ,[A] ,[C] ,[H] ,[Index] AS indice,[Distance],[Hauteur],[Longueur],[Profondeur],[Classe] from  siteAP where MAG like 'MAG" + M + "%' and A like 'A" + A + "       %' order by [Index]";
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

                            largempl = (decimal)2250;
                            hautEmp = (decimal)650;
                            ProfEmp = (decimal)500;
                            largref = 0;
                            while (refe < dtbl6.Rows.Count) // condition
                            {


                                lar1 = Convert.ToDecimal(dtbl6.Rows[refe]["lar"].ToString());
                                prof1 = Convert.ToDecimal(dtbl6.Rows[refe]["prof"].ToString());
                                haut1 = Convert.ToDecimal(dtbl6.Rows[refe]["haut"].ToString());
                                TMothsv = Convert.ToDecimal(dtbl6.Rows[refe]["QT3Mois"].ToString());

                                // calcu de l'espace didié à la référence largref signifie la largeur occ par une référence 

                                largref = (decimal)((decimal)0.75 * lar1 * TMothsv / (((decimal)hautEmp / haut1) * ((decimal)ProfEmp / prof1)));
                                dtbl6.Rows[refe]["Laroccu"] = (decimal)largref;
                                refe++;

                              

                            }
                            emp++; // increment
                        }


                        // if occ  >  2250


                        // algo affectation 


                        dtbl6.Columns.Add("EmplOptimal", typeof(string));
                        dtbl6.Columns.Add("classe", typeof(string));
                        dtbl6.DefaultView.Sort = "VarCom Desc";

                        dtbl6 = dtbl6.DefaultView.ToTable(true);
                        // dataGridView1.DataSource = Empldtbl;

                        dtbl6Copy = dtbl6.Copy();

                        int j = 0, s = 0, t = 0;

                        while (j < Empldtbl.Rows.Count)//loop on emp
                        {
                            // 


                            



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









                        //delete before Upload 


                        string connectionString3 = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = EmpOptimisation;";
                        using (SqlConnection connection4 = new SqlConnection(connectionString3))
                        {
                            string ale = A.ToString(); 
                            string mag = M.ToString();
                            string Empplacemnt = "MAG" +mag+ "-A" +ale+"%";
                           


                            string sql = "DELETE FROM Table_1 WHERE EmplacementAct like @name";
                            connection4.Open();
                            using (SqlCommand cmd = new SqlCommand(sql, connection4))
                            {
                                cmd.Parameters.AddWithValue("@name", Empplacemnt);
                               


                                // assign value to parameter 
                                cmd.ExecuteNonQuery();
                            }
                        }

                        //upload 



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
                MessageBox.Show("Erreur d'execution dans : "+ex.ToString());
            }


        }



        private void button2_Click(object sender, EventArgs e)
        {

           
            for (int j = 0; j < levels.Length; j++)
            {


                Parallel.For(0, coul1.Length, i =>
                {

                    UpdateAisle2(coul1[i], levels[j]);
                    //mettre à jour tous les allées dans le magasin 
                   

                });
            
            }


            

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {



            this.button2.Visible = true;

            this.panel2.Visible = false; 
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            this.button2.Visible = false;
            this.panel2.Visible = true; 



        }

        private void button3_Click(object sender, EventArgs e)
        {


            new Form4().Show();
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            String mag = this.comboBox1.Text;
            string All = this.comboBox2.Text;

            UpdateAisle2(All.ToString(), mag.ToString()); 



        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
