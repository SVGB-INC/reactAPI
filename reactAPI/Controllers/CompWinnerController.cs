using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace reactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompWinnerController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public CompWinnerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //getting the data
        [HttpGet]
        public JsonResult Get(string UserName)
        {
            string query = @"Select TOP 1 [uID],[comp_user],[comp_name],[comp_venue],[comp_create_date],[comp_participants],
                            [comp_winners],[comp_prizes],[comp_type],[comp_code],[comp_winner_names],
                            [comp_status],[comp_start_date]
                            FROM [react].[dbo].[dt_Comps]  
                            where comp_status = 'Done'
                            and [comp_user] = '" + UserName + @"' 
                            order by comp_start_date";

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
