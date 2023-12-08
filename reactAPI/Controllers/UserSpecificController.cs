using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace reactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSpecificController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public UserSpecificController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //getting the data
        [HttpGet]
        public JsonResult Get(int uID)
        {
            string query = @"SELECT [uID], [user_name],[user_email],[user_pass],[user_mobile],[comp_name],[job_title]
                            ,[office_address],[region],[city],[category],[user_dtJoin],[user_status],[user_fname],[user_mname],[user_lname],[user_zip],[user_country],[user_time],[user_language],[user_currency] from dbo.dt_Users where [uID] = " + uID + @";";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("reactDB");

            SqlDataReader myReader;

            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    sqlConnection.Close();
                }
            }

            return new JsonResult(table);
        }
    }
}
