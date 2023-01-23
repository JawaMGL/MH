using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MH_Api.Controllers
{

    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TeamController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //GET method for teams by LeagueKey -byJawa
        [HttpGet]
        [Route("api/[controller]/GetTeamsByLeagueKey/{LeagueKey}")]

        public JsonResult GetTeamsByLeagueKey(int LeagueKey)
        {
            string query = @"
                            SELECT T.TeamKey, T.TeamName, T.Conference, T.TeamCity FROM Team T
                            WHERE T.LeagueKey = @LeagueKey           
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;
            try
            {
                using (SqlConnection myConn = new SqlConnection(sqlDataSource))
                {
                    myConn.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myConn))
                    {
                        myCommand.Parameters.AddWithValue("@LeagueKey", LeagueKey);

                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);


                        myReader.Close();
                        myConn.Close();
                    }
                }
                return new JsonResult(table);
            }
            catch (Exception ex)
            {
                return new JsonResult("Error occurred;");
            }
        }

    }
}
