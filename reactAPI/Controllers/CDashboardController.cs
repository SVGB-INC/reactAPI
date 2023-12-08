using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace reactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CDashboardController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public CDashboardController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //getting the data
        [HttpGet]
        public JsonResult Get(string UserName)
        {
            string query = @"SELECT comps.uID,[comp_user],comps.comp_name,[comp_venue],[comp_create_date],[comp_participants]
                            ,[comp_winners],[comp_prizes],[comp_type],[comp_code],[comp_winner_names],
                            [comp_status],[comp_start_date],users.user_email, users.category
                            from dbo.dt_Comps as comps 
                            inner join dt_Users as users
                            on comps.comp_user = users.user_name
                            where comp_user = '" + UserName + @"'";

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
