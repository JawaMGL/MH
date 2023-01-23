using MH_Api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;


namespace MH_Api.Controllers
{
    
    [ApiController]
    public class PlayerController : ControllerBase
    {
        // using dep injection to read the data connection string -byJawa
        private readonly IConfiguration _configuration;
        public PlayerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET method for player by name -byJawa

        [HttpGet]
        [Route("api/[controller]/GetPlayerByName/{player}")]
        public JsonResult GetPlayerByName(string player)
        {
            // parameter player can be last and first name -byJawa
            string query = @"
                       SELECT * FROM Player P
                        LEFT JOIN TeamPlayer TP ON TP.PlayerKey = P.PlayerKey
                        WHERE TP.SeasonKey = (SELECT MAX(SeasonKey) FROM TeamPlayer) AND P.FirstName LIKE @playerName OR P.LastName LIKE @playerName
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
                        myCommand.Parameters.AddWithValue("@playerName", player);
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
                return new JsonResult("Error occured");

            }
        }
        // GET method for player by team name and season(was team id on assessment, changed to name) -byJawa

        [HttpGet]
        [Route("api/[controller]/GetPlayersByTeamAndSeason/{TeamName}/{SeasonKey}")]
        public JsonResult GetPlayersByTeamAndSeason(string TeamName, int SeasonKey)
        {
            // get team players based on last(current) - byJawa
            string query = @"                 
                            SELECT * FROM Player P
                            LEFT JOIN TeamPlayer TP ON TP.PlayerKey = P.PlayerKey 
                            LEFT JOIN Team T ON TP.TeamKey = T.TeamKey
                            WHERE TP.PlayerKey = P.PlayerKey AND TP.SeasonKey = @SeasonKey AND TP.TeamKey = T.TeamKey  AND T.TeamName = @TeamName
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
                        myCommand.Parameters.AddWithValue("@TeamName", TeamName);
                        myCommand.Parameters.AddWithValue("@SeasonKey", SeasonKey);
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
                return new JsonResult("Error occured");

            }
        }
    }
}
