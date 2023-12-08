﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace reactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportCompetitionDateController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ReportCompetitionDateController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //getting the data
        [HttpGet]
        public JsonResult Get(string startDate, string endDate)
        {
            string query = @"Select uID, comp_name,comp_create_date, 
                            comp_user, comp_participants, comp_winners, comp_prizes from dt_Comps where comp_create_date > '" + startDate + @"' and comp_create_date <= '" + endDate + @"'";

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
