
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace kumail.Models
{
    public class CoreModel
    {

        public SqlConnection CN = new SqlConnection();
        public string SQL_Str = Convert.ToString(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"]);
        public void CnStr()
        {
            if (CN.State == ConnectionState.Open)
                CN.Close();
            CN.ConnectionString = SQL_Str;
        }

        public string Data2Json(string str)
        {
            try
            {
                DataTable dt = FillDSet(str).Tables[0];
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                serializer.MaxJsonLength = int.MaxValue;
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
                return serializer.Serialize(rows);
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }
        public string Exec(string str)
        {
            string Out;
            try
            {
                CnStr();
                CN.Open();
                SqlCommand cmd = new SqlCommand(str, CN);
                cmd.CommandTimeout = 0;
                Out = cmd.ExecuteNonQuery().ToString();
                CN.Close();
            }
            catch (System.Exception ex)
            {
                Out = ex.Message;
            }
            return Out;
        }


        public DataSet FillDSet(string cmd)
        {
            DataSet MyDataSet = new DataSet();
            try
            {
                System.Data.SqlClient.SqlDataAdapter MyDataAdapter = new System.Data.SqlClient.SqlDataAdapter(cmd, SQL_Str);

                MyDataAdapter.SelectCommand.CommandTimeout = 1000;

                MyDataAdapter.Fill(MyDataSet);

            }
            catch (Exception ex)
            {
                string s = ex.Message;

            }
            return MyDataSet;
        }
        public string HTMLTableWithID_TR_Tag(string SQL, string tblId)
        {
            DataSet rlst = FillDSet(SQL);
            string outstring = "<table id='" + tblId + "' class='table table-striped table-bordered table-hover'><thead><tr style='font-weight: bold;'>";
            int colCount = rlst.Tables[0].Columns.Count;
            for (int i = 1; i < colCount; i++)
            {
                outstring = outstring + "<th>" + rlst.Tables[0].Columns[i].ColumnName + "</th>";
            }
            outstring = outstring + "</tr></thead><tbody>";
            for (int i = 0; i < rlst.Tables[0].Rows.Count; i++)
            {
                outstring = outstring + "<tr tag='" + rlst.Tables[0].Rows[i][0] + "'>";
                for (int j = 1; j < colCount; j++)
                {
                    outstring = outstring + "<td>" + rlst.Tables[0].Rows[i][j] + "</td>";
                }
                outstring = outstring + "</tr>";
            }

            string ssssssssssssss = outstring + "</tbody>";
            return outstring + "</tbody></table>";
        }



        public string Exec_Outval(string str)
        {
            string Out;
            try
            {
                CnStr();
                CN.Open();
                SqlCommand cmd = new SqlCommand(str, CN);
                cmd.CommandTimeout = 0;
                Out = cmd.ExecuteScalar() == null ? "" : cmd.ExecuteScalar().ToString();
                CN.Close();
            }
            catch (System.Exception ex)
            {
                Out = ex.Message;
            }
            return Out;
        }
    }
}