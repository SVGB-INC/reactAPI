using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace reactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportBundlesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ReportBundlesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //getting the data
        [HttpGet]
        public JsonResult Get()
        {
            //string query = @"select 
            //                bundles.uID, bundles.user_bundle ,users.comp_name, users.user_dtJoin, 
            //                users.user_name, users.user_mobile, users.user_email, bundles.user_bundle_price
            //                from User_Bundles as bundles
            //                inner join dt_Users as users
            //                on bundles.user_ID = users.uID";

            string query = @"select 
                            cast(bundles.uID as varchar) as uID, bundles.user_bundle ,users.comp_name, 
							CAST(users.user_dtJoin AS varchar) as user_dtJoin, 
                            users.user_name, users.user_mobile, users.user_email, 
							CONCAT('$',bundles.user_bundle_price) as user_bundle_price
                            from User_Bundles as bundles
                            inner join dt_Users as users
                            on bundles.user_ID = users.uID

							UNION ALL

							select 
                            '', '', '', '', '', '', 'Total Earning:', 
                            CONCAT('$',sum(CAST(bundles.user_bundle_price AS int)))
                            from User_Bundles as bundles
                            inner join dt_Users as users
                            on bundles.user_ID = users.uID";

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
