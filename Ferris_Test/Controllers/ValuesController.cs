using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Ferris_Test.Models;
using System.Xml;
using System.Web.Script.Serialization;
using System.Text;
using System.Web.Http.Cors;

namespace Ferris_Test
{
    public class Records
    {
        public List<Dictionary<string, object>> fieldsjsn { get; set; }
    }
    
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        // GET api/<controller>
        [Route("City2")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> cities = new List<string>();
            cities.Add("atlanta");
            cities.Add("alpharetta");
            cities.Add("macon");

            return cities;
            //return new string[] { "value1", "value2" };

        }

        //http://localhost:64805/api/values/City3?st1=GA&st2=par
        [EnableCors(origins: "http://localhost:56376", headers: "*", methods: "*")]
        [Route("City3")]
        [HttpGet]
        public HttpResponseMessage GetWithUri2([FromUri] ParamsObject paramsObject)
        {
            List<string> comp = new List<string>();
            try
            {
               
                 var connection = ConfigurationManager.ConnectionStrings["datalister_database"].ConnectionString;
                 using (SqlConnection con = new SqlConnection(connection))
                 {
                     using (SqlCommand cmd = new SqlCommand("proc_Auto_CompanyName", con))
                     {
                         cmd.CommandType = CommandType.StoredProcedure;
                         cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = paramsObject.st1.Trim();
                         cmd.Parameters.Add("@ParamName", SqlDbType.VarChar).Value = paramsObject.st2.Trim();
                         con.Open();
                         SqlDataReader reader = cmd.ExecuteReader();

                         if (reader.HasRows)
                         {
                             while (reader.Read())
                             {
                                 comp.Add(reader.GetString(0));
                             }
                         }



                     }

                 }

            }
            catch (Exception x)
            {

            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(comp);

            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json.ToString(), Encoding.UTF8, "text/html");


            return res; //sreturn.Replace(@"\", "");
           

        }



        //http://localhost:64805/api/values/City?st1=ga
        [EnableCors(origins: "http://localhost:56376", headers: "*", methods: "*")] 
        [Route("City")]
        [HttpGet]
        public HttpResponseMessage GetWithUri([FromUri] ParamsObject paramsObject)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            ///HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "");
            ///
            try
            {
                //List<string> cities = new List<string>();
               
                var connection = ConfigurationManager.ConnectionStrings["datalister_database"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("proc_Auto_CompanyName", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@State", SqlDbType.VarChar).Value = paramsObject.st1.Trim();
                        cmd.Parameters.Add("@ParamName", SqlDbType.VarChar).Value = paramsObject.st2.Trim();
                        con.Open();


                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
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


                        //SqlDataReader reader = cmd.ExecuteReader();
                        //if (reader.HasRows)
                        //{
                        //    while (reader.Read())
                        //    {
                        //        //cities.Add(reader["city"].ToString());
                        //        XmlDocument doc = new XmlDocument();
                        //        string sxml = reader[0].ToString();
                        //        doc.LoadXml(sxml);
                        //        string json = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);
                        //        response = Request.CreateResponse(HttpStatusCode.OK, json);
                        //    }
                        //}
                        //else
                        //{
                        //    Console.WriteLine("No rows found.");
                        //}
                        //reader.Close();
                    }
                }

                
            }
            catch (SystemException ex)
            {

            }
            Records rc = new Records();
            rc.fieldsjsn = rows;
            string sreturn = "";
            try
            {
                serializer.MaxJsonLength = int.MaxValue;
                sreturn = serializer.Serialize(rc);
                JavaScriptSerializer j = new JavaScriptSerializer();
                j.MaxJsonLength = int.MaxValue;
                object a = j.Deserialize(sreturn, typeof(object));

            }
            catch(Exception xx)
            {

            }
            
            

            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(sreturn.ToString(), Encoding.UTF8, "text/html");
            

            return res; //sreturn.Replace(@"\", "");
        }


        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
           
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
    //public class CityResults
    //{
    //    public string State_Code { get; set; }
    //    public string City { get; set; }
    //}
}