using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;



namespace MH_Api.Controllers
{
    [ApiController]
    public class LeagueController : Controller
    {
        // using dep injection to read the data connection string -byJawa
        private readonly IConfiguration _configuration;
        public LeagueController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET method for league -byJawa
        [HttpGet]
        [Route("api/[controller]/GetLeague/")]

        public JsonResult GetLeague()
        {
            string query = @"
                            SELECT LeagueName, Country FROM dbo.League
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
                return new JsonResult("Error occurred");
            };
        }

    }
}
