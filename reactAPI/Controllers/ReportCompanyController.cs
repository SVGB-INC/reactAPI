using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace reactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportCompanyController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ReportCompanyController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //getting the data
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"Select
                            users.uID, comp_name,user_dtJoin, users.user_name, user_mobile, user_email, user_status, 
                            count(bundles.user_bundle) as bundle_count
                            from dt_Users as users
                            left join user_bundles as bundles
                            on users.user_name = bundles.user_name
                            group by users.uID, comp_name,user_dtJoin, users.user_name, user_mobile, user_email, user_status";

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
