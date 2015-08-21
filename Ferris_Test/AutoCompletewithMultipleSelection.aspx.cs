using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Services;

public partial class AutoCompletewithMultipleSelection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]

    public static List<string> GetAutoCompleteData(string username)
    {
        List<string> result = new List<string>();
        using (SqlConnection con = new SqlConnection("Data Source=SureshDasari;Integrated Security=true;Initial Catalog=MySampleDB"))
        {
            using (SqlCommand cmd = new SqlCommand("select DISTINCT UserName from UserInformation where UserName LIKE '%'+@SearchText+'%'", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@SearchText", username);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result.Add(dr["UserName"].ToString());
                }
                return result;
            }
        }
    }
}
