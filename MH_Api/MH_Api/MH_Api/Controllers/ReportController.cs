using MH_Api.Data;
using MH_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MH_Api.Controllers
{
    [ApiController]
    public class ReportController : ControllerBase
    {
        //using dep injection to read the data conn string -byJawa
        private readonly IConfiguration _configuration;
        public ReportController(IConfiguration configuration) {
            _configuration = configuration;
        }

        // GET method for all reports -byJawa
        [HttpGet]
        [Route("api/[controller]/GetAllReports")]

        public JsonResult GetAllReports() {
            string query = @"
                             SELECT NR.*, P.FirstName, P.LastName  FROM NewReport NR
                             LEFT JOIN Player P ON P.PlayerKey = NR.PlayerKey
                             WHERE IsDeleted = 0 AND P.PlayerKey = NR.PlayerKey
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
            catch (Exception ex) {
                return new JsonResult("Error occurred");
            };
        }

        //GET method for reports by ScoutKey -byJawa
        [HttpGet]
        [Route("api/[controller]/GetReportsByScoutKey/{ScoutKey}")]

        //Using FOR JSON PATH to get nested reports of each player -byJawa
        //Getting players team based on current/last season by (SELECT MAX(SeasonKey) from TeamPlayer)
        public JsonResult GetReportsByScoutKey(int ScoutKey) {
            string query = @"
                             SELECT NR.ReportId, T.TeamName, T.Conference, P.FirstName, P.LastName, P.Birthdate, P.FreeAgentYear, P.YearsOfService, 
                            (SELECT NR.ShootingRating, NR.AssistRating, NR.ReboundRating, NR.DefenseRating, NR.HighlightLink,  NR.CreatedDateTime, NR.UpdatedDateTime
                            FROM NewReport NR
                            WHERE NR.PlayerKey = P.PlayerKey
                            FOR JSON AUTO) AS reports
                            FROM Player P
                            RIGHT JOIN NewReport NR ON NR.PlayerKey = P.PlayerKey
                            LEFT JOIN Team T ON T.TeamKey = NR.TeamKey
                            LEFT JOIN TeamPlayer TP ON TP.PlayerKey = NR.PlayerKey
                            WHERE NR.PlayerKey = P.PlayerKey AND NR.ScoutKey = @ScoutKey AND TP.PlayerKey = NR.PlayerKey AND TP.SeasonKey =  (SELECT MAX(SeasonKey) from TeamPlayer) AND NR.IsDeleted = 0
                            GROUP BY P.FirstName, P.LastName, P.PlayerKey, T.TeamName, P.Birthdate, P.FreeAgentYear, P.YearsOfService, T.Conference
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;
            try
            {
                using (SqlConnection myConn = new SqlConnection(sqlDataSource))
                {
                myConn.Open();
                    using(SqlCommand myCommand=new SqlCommand(query, myConn))
                    {
                        myCommand.Parameters.AddWithValue("@ScoutKey", ScoutKey);
                   
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


        //POST method -byJawa
        [HttpPost]
        [Route("api/[controller]/CreateNewReport")]

        public JsonResult CreateNewReport(NewReport report)
        {
            string query = @"
                            INSERT INTO NewReport (PlayerKey, TeamKey, ScoutKey, Comments, HighlightLink, 
                            DefenseRating, ReboundRating, ShootingRating, AssistRating, 
                            CreatedDateTime, IsDeleted)
                            values (@PlayerKey, @TeamKey, @ScoutKey, @Comments, @HighlightLink, 
                            @DefenseRating, @ReboundRating, @ShootingRating, @AssistRating, 
                            @CreatedDateTime, @IsDeleted)
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
                        myCommand.Parameters.AddWithValue("@PlayerKey", report.PlayerKey);
                        myCommand.Parameters.AddWithValue("@TeamKey", report.TeamKey);
                        myCommand.Parameters.AddWithValue("@ScoutKey", report.ScoutKey);
                        myCommand.Parameters.AddWithValue("@Comments", report.Comments);
                        myCommand.Parameters.AddWithValue("@HighlightLink", report.HighlightLink);
                        myCommand.Parameters.AddWithValue("@DefenseRating", report.DefenseRating);
                        myCommand.Parameters.AddWithValue("@ReboundRating", report.ReboundRating);
                        myCommand.Parameters.AddWithValue("@ShootingRating", report.ShootingRating);
                        myCommand.Parameters.AddWithValue("@AssistRating", report.AssistRating);
                        myCommand.Parameters.AddWithValue("@CreatedDateTime", DateTime.Now);
                        myCommand.Parameters.AddWithValue("@IsDeleted", false);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myConn.Close();
                    }
                }
                return new JsonResult("Added successfully");
            }
            catch (Exception ex) {
                return new JsonResult("Error occurred");
            }
        }


        //PUT method for report by id -byJawa
        [HttpPut]
        [Route("api/[controller]/UpdateReport")]

        public JsonResult UpdateReport(NewReport report)
        {
            string query = @"
                            UPDATE dbo.NewReport
                            SET Comments=@Comments, HighlightLink=@HighlightLink, 
                            DefenseRating=@DefenseRating, ReboundRating=@ReboundRating, ShootingRating=@ShootingRating, AssistRating=@AssistRating, 
                             UpdatedDateTime=@UpdatedDateTime
                            WHERE ReportId = @ReportId
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
                        myCommand.Parameters.AddWithValue("@ReportId", report.ReportId);
                        myCommand.Parameters.AddWithValue("@Comments", report.Comments);
                        myCommand.Parameters.AddWithValue("@HighlightLink", report.HighlightLink);
                        myCommand.Parameters.AddWithValue("@DefenseRating", report.DefenseRating);
                        myCommand.Parameters.AddWithValue("@ReboundRating", report.ReboundRating);
                        myCommand.Parameters.AddWithValue("@ShootingRating", report.ShootingRating);
                        myCommand.Parameters.AddWithValue("@AssistRating", report.AssistRating);
                        myCommand.Parameters.AddWithValue("@UpdatedDateTime", DateTime.Now);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myConn.Close();
                    }
                }
                return new JsonResult("Updated successfully");
            }
            catch (Exception ex)
            {
                return new JsonResult("Error occurred");
            }
        }

        //PUT method for delete(soft) report by id -byJawa
        [HttpPut]
        [Route("api/[controller]/DeleteReport/")]

        public JsonResult DeleteReport(NewReport report)
        {
            string query = @"
                            UPDATE dbo.NewReport
                            SET IsDeleted=1
                            WHERE ReportId = @ReportId
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
                        myCommand.Parameters.AddWithValue("@ReportId", report.ReportId);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myConn.Close();
                    }
                }
                return new JsonResult("Deleted successfully");
            }
            catch (Exception ex)
            {
                return new JsonResult("Error occurred");
            }
        }


    }
}
