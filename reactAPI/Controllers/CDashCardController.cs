using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace reactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CDashCardController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CDashCardController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //getting the data
        [HttpGet]
        public JsonResult Get(string UserName)
        {
            string query = @"SELECT 
                            count(*) as total_comps, 
                            (select count(*) from dbo.dt_Comps where comp_user = '" + UserName + @"' and comp_status = 'Done') as done_comps,
                            ISNULL(((select count(*) from dbo.dt_Comps where comp_user = '" + UserName + @"' and comp_status = 'Done') * 100 ) / 
							NULLIF(count(*),0),0) as done_percent,
                            (select count(*) from dbo.dt_Comps where comp_user = '" + UserName + @"' and comp_status <> 'Done') as pending_comps
                            from dbo.dt_Comps
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
