using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace reactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetLimitController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public GetLimitController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //getting the data
        [HttpGet]
        public JsonResult Get(string userID)
        {
            string query = @"Select * from dt_Limit where user_id = '" + userID + @"'";

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

        //updating the data
        [HttpPut]
        public JsonResult Put(string participants, string uID)
        {
            
            string query = @"UPDATE dbo.dt_limit SET 
                            [rem_limit] = CAST(rem_limit as int) - CAST('" + participants + @"' as int)
                            WHERE [user_id] = " + uID+ @";";

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

            return new JsonResult("Limit Updated Successfully.");

        }
    }
}
