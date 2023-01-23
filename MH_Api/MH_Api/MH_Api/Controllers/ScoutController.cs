using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MH_Api.Controllers
{
    [ApiController]
    public class ScoutController : ControllerBase
    {   
        //using dep injection to read the data conn string -byJawa
        private readonly IConfiguration _configuration;
        public ScoutController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET method for all active scouts -byJawa
        [HttpGet]
        [Route("api/[controller]/GetAllScouts")]

        public JsonResult GetAllScouts()
        {
            string query = @"
                            SELECT * FROM dbo.Scout WHERE IsActive = 1
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
