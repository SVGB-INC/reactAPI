using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace reactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportBundlesDateController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ReportBundlesDateController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //getting the data
        [HttpGet]
        public JsonResult Get(string startDate, string endDate)
        {
            string query = @"select 
                            cast(bundles.uID as varchar) as uID, bundles.user_bundle ,users.comp_name, 
							CAST(users.user_dtJoin AS varchar) as user_dtJoin, 
                            users.user_name, users.user_mobile, users.user_email, 
							CAST(bundles.dt_purchase AS varchar) as user_dtPurchase, 
							CONCAT('$',bundles.user_bundle_price) as user_bundle_price
                            from User_Bundles as bundles
                            inner join dt_Users as users
                            on bundles.user_ID = users.uID
							where bundles.dt_purchase > '" + startDate + @"' and bundles.dt_purchase  <= '" + endDate + @"' 

							UNION ALL

							select 
                            '', '', '', '', '', '', '','Total Earning:', 
                            CONCAT('$',sum(CAST(bundles.user_bundle_price AS int)))
                            from User_Bundles as bundles
                            inner join dt_Users as users
                            on bundles.user_ID = users.uID
							where bundles.dt_purchase > '" + startDate + @"' and bundles.dt_purchase  <= '" + endDate + @"'";

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
