using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using reactAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace reactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRejectController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public UserRejectController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //updating the data
        [HttpPut]
        public JsonResult Put(User user)
        {
            string query = @"UPDATE dbo.dt_Users SET 
                            [user_status] = 'Rejected'
                            WHERE [uID] = " + user.UserID + @";";

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

            return new JsonResult("User Rejected Successfully.");

        }


    }
}
