using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using reactAPI.Models;

namespace reactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BundleController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public BundleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //getting the data
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT [uID], [bundle_name],[bundle_desc],[bundle_price],[bundle_participants],[bundle_features],[bundle_date], [bundle_status] from dbo.dt_Bundles";

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

        //posting the data
        [HttpPost]
        public JsonResult Post(Bundle bundle)
        {
            string query = @"INSERT INTO dbo.dt_Bundles([bundle_name],[bundle_desc],[bundle_price],[bundle_participants],[bundle_features])
                            VALUES('" + bundle.BundleName + @"','" + bundle.BundleDesc + @"','" + bundle.BundlePrice + @"', "
                            + bundle.BundleParticipants+ @",'" + bundle.BundleFeatures + @"')";

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

            return new JsonResult("New Bundle Added Successfully.");

        }

        //updating the data
        [HttpPut]
        public JsonResult Put(Bundle bundle)
        {
            string query = @"UPDATE dbo.dt_Bundles SET 
                            [bundle_name] = '" + bundle.BundleName + @"',
                            [bundle_desc] = '" + bundle.BundleDesc + @"',
                            [bundle_price] = '" + bundle.BundlePrice + @"',
                            [bundle_participants] = '" + bundle.BundleParticipants + @"',
                            [bundle_features] = '" + bundle.BundleFeatures + @"',
                            [bundle_status] = '" + bundle.BundleStatus + @"'
                            WHERE [uID] = " + bundle.BundleId + @";";

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

            return new JsonResult("Bundle Updated Successfully.");

        }

        //Deleting the data
        [HttpDelete("{bundleID}")]
        public JsonResult Delete(int bundleID)
        {
            string query = @"Delete from dbo.dt_Bundles WHERE [uID] = " + bundleID + @";";

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

            return new JsonResult("Bundle Deleted Successfully.");

        }

    }
}
