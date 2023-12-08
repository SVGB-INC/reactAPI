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
    public class CompLaunchController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public CompLaunchController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //updating the data
        [HttpPut]
        public JsonResult Put(Competition comp)
        {
            string query = @"UPDATE dbo.dt_Comps SET 
                            [comp_winner_names] = '" + comp.CompWinnerNames + @"',
                            [comp_status] = '" + comp.CompStatus + @"',
                            [comp_start_date] = GETDATE()
                            WHERE [uID] = " + comp.CompID + @";";

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

            return new JsonResult("Competition Updated Successfully.");

        }

    }
}
