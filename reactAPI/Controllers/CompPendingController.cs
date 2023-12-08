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
    public class CompPendingController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CompPendingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //getting the data
        [HttpGet]
        public JsonResult Get(string UserName)
        {
            string query = @"SELECT [uID],[comp_user],[comp_name],[comp_venue],[comp_create_date],[comp_participants]
                            ,[comp_winners],[comp_prizes],[comp_type],[comp_code],[comp_winner_names], [comp_status], [comp_allowed] from dbo.dt_Comps where comp_status = 'pending' and [comp_user] = '" + UserName + @"'";

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

        ////posting the data
        //[HttpPost]
        //public JsonResult Post(Competition comp)
        //{
        //    string query = @"INSERT INTO dbo.dt_Comps([comp_user],[comp_name],[comp_venue],[comp_create_date],[comp_participants]
        //                    ,[comp_winners],[comp_prizes],[comp_type],[comp_code],[comp_winner_names])
        //                    VALUES('" + comp.UserName + @"','" + comp.CompName + @"','" + comp.CompVenue + @"', '"
        //                    + comp.CompCreateDate + @"','" + comp.CompParticipants + @"','" + comp.CompWinners + @"', '" + comp.CompPrizes
        //                    + @"','" + comp.CompType + @"','" + comp.CompCode + @"','" + comp.CompWinnerNames + @"')";

        //    DataTable table = new DataTable();

        //    string sqlDataSource = _configuration.GetConnectionString("reactDB");

        //    SqlDataReader myReader;

        //    using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
        //    {
        //        sqlConnection.Open();
        //        using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
        //        {
        //            myReader = sqlCommand.ExecuteReader();
        //            table.Load(myReader);

        //            myReader.Close();
        //            sqlConnection.Close();
        //        }
        //    }

        //    return new JsonResult("New Competition Added Successfully.");

        //}

        ////updating the data
        //[HttpPut]
        //public JsonResult Put(Competition comp)
        //{
        //    string query = @"UPDATE dbo.dt_Comps SET 
        //                    [comp_user] = '" + comp.UserName + @"',
        //                    [comp_name] = '" + comp.CompName + @"',
        //                    [comp_venue] = '" + comp.CompVenue + @"',
        //                    [comp_create_date] = '" + comp.CompCreateDate + @"',
        //                    [comp_participants] = '" + comp.CompParticipants + @"', 
        //                    [comp_winners] = '" + comp.CompWinners + @"',
        //                    [comp_prizes] = '" + comp.CompPrizes + @"',
        //                    [comp_type] = '" + comp.CompType + @"',
        //                    [comp_code] = '" + comp.CompCode + @"', 
        //                    [comp_winner_names] = '" + comp.CompWinnerNames + @"' 
        //                    WHERE [uID] = " + comp.CompID + @";";

        //    DataTable table = new DataTable();

        //    string sqlDataSource = _configuration.GetConnectionString("reactDB");

        //    SqlDataReader myReader;

        //    using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
        //    {
        //        sqlConnection.Open();
        //        using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
        //        {
        //            myReader = sqlCommand.ExecuteReader();
        //            table.Load(myReader);

        //            myReader.Close();
        //            sqlConnection.Close();
        //        }
        //    }

        //    return new JsonResult("Competition Updated Successfully.");

        //}

        ////Deleting the data
        //[HttpDelete("{CompID}")]
        //public JsonResult Delete(int CompID)
        //{
        //    string query = @"Delete from dbo.dt_Comps WHERE [uID] = " + CompID + @";";

        //    DataTable table = new DataTable();

        //    string sqlDataSource = _configuration.GetConnectionString("reactDB");

        //    SqlDataReader myReader;

        //    using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
        //    {
        //        sqlConnection.Open();
        //        using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
        //        {
        //            myReader = sqlCommand.ExecuteReader();
        //            table.Load(myReader);

        //            myReader.Close();
        //            sqlConnection.Close();
        //        }
        //    }

        //    return new JsonResult("Competition Deleted Successfully.");

        //}

    }
}
