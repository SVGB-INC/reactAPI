using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace reactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ADashCardController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ADashCardController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //getting the data
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"Select 
                            (select count(DISTINCT(comp_name)) from dt_Users) as tot_companies,
                            (select count((user_status)) from dt_Users where user_status = 'Pending') as tot_pending,
                            (select count(*) from dt_comps ) as tot_comps";

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
