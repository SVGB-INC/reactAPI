using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using reactAPI.Models;

namespace reactAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //getting the data
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT [uID], [user_name],[user_email],[user_pass],[user_mobile],[comp_name],[job_title]
                            ,[office_address],[region],[city],[category],[user_dtJoin],[user_status],[user_fname],[user_mname],[user_lname] from dbo.dt_Users";


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
        public JsonResult Post(User user)
        {
            string query = @"INSERT INTO dbo.dt_Users([user_name],[user_email],[user_pass],[user_mobile],[comp_name],[job_title],[office_address],[region],[city],[category],[user_status],[user_fname],[user_mname],[user_lname], [user_zip],[user_country] ,[user_time] ,[user_language],[user_currency],[user_gender]) VALUES('" + user.UserName + @"','" + user.UserEmail + @"','" + user.UserPassword + @"', '"
                            + user.UserMobile + @"','" + user.CompName + @"','" + user.JobTitle + @"', '" + user.OfficeAddress
                            + @"','" + user.Region + @"','" + user.City + @"','" + user.Category + @"', '" + user.Status + @"', '" + user.FirstName + @"','" + user.MiddleName + @"','" + user.LastName + @"','" + user.Zip + @"','" + user.Country + @"','" + user.Time + @"','" + user.Language + @"','" + user.Currency + @"','" + user.Gender + @"')";

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

            return new JsonResult("New User Added Successfully.");

        }

        //updating the data
        [HttpPut]
        public JsonResult Put(User user)
        {
            string query = @"UPDATE dbo.dt_Users SET 
                            [user_name] = '" + user.UserName + @"',
                            [user_email] = '" + user.UserEmail + @"',
                            [user_mobile] = '" + user.UserMobile + @"',
                            [comp_name] = '" + user.CompName + @"',
                            [job_title] = '" + user.JobTitle + @"', 
                            [office_address] = '" + user.OfficeAddress + @"',
                            [region] = '" + user.Region + @"',
                            [city] = '" + user.City + @"',
                            [category] = '" + user.Category + @"', 
                            [user_status] = '" + user.Status + @"',
                            [user_fname] = '" + user.FirstName + @"',
                            [user_mname] = '" + user.MiddleName + @"',
                            [user_lname] = '" + user.LastName + @"',
                            [user_zip] = '" + user.Zip + @"',
                            [user_country] = '" + user.Country + @"',
                            [user_time] = '" + user.Time + @"',
                            [user_language] = '" + user.Language + @"',
                            [user_currency] = '" + user.Currency + @"'
                            WHERE [uID] = " + user.UserID + @";";

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

            return new JsonResult("User Updated Successfully.");

        }

        //Deleting the data
        [HttpDelete("{userID}")]
        public JsonResult Delete(int userID)
        {
            string query = @"Delete from dbo.dt_Users WHERE [uID] = " + userID + @";";

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

            return new JsonResult("User Deleted Successfully.");

        }

    }
}
