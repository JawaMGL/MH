import React, { useState } from "react";
import Variables from "./Utilities/Variables";
import CreateReport from "./components/CreateReport";
import UpdateReport from "./components/UpdateReport";
import DeleteReport from "./components/DeleteReport";
export default function App() {
  // declaring states for new report, update report and button visibility status -byJawa
  const [reports, setReports] = useState([]);
  const [showNewReportForm, setShowNewReportForm] = useState(false);
  const [reportCurrentlyBeingUpdated, setReportCurrentlyBeingUpdated] =
    useState(null);
  const [reportCurrentlyBeingDeleted, setReportCurrentlyBeingDeleted] =
    useState(null);

  // fetching reports -byJawa
  function getAllReports() {
    const url = Variables.API_URL_GET_ALL_REPORTS;
    fetch(url, {
      method: "GET",
    })
      .then((response) => response.json())
      .then((reportsFromServer) => {
        //console.log(reportsFromServer);
        setReports(reportsFromServer);
      })
      .catch((error) => {
        console.log(error);
        alert(error);
      });
  }
  return (
    <div className="container">
      <div className="row min-vh-100">
        <div className="col d-flex flex-column justify-content-center align-items-center">
          {showNewReportForm === false &&
            reportCurrentlyBeingUpdated === null &&
            reportCurrentlyBeingDeleted === null && (
              <div>
                <h1>Scout Reporting</h1>
                <div className="mt-5">
                  <button
                    onClick={getAllReports}
                    className="btn btn-danger w-100 btn-lg"
                  >
                    Reports
                  </button>
                  <button
                    onClick={() => setShowNewReportForm(true)}
                    className="btn btn-dark w-100 btn-lg mt-4"
                  >
                    New Report
                  </button>
                </div>
              </div>
            )}
          {/* check reports object from server and button visibility before rendering table -byJawa */}
          {reports.length > 0 &&
            showNewReportForm === false &&
            reportCurrentlyBeingUpdated === null &&
            reportCurrentlyBeingDeleted === null &&
            renderReportTable()}
          {/* check button visibility and open CreateReport component -byJawa */}
          {showNewReportForm && (
            <CreateReport OnReportCreated={OnReportCreated} />
          )}
          {/* */}
          {reportCurrentlyBeingUpdated !== null && (
            <UpdateReport
              report={reportCurrentlyBeingUpdated}
              OnReportUpdated={OnReportUpdated}
            />
          )}
          {reportCurrentlyBeingDeleted !== null && (
            <DeleteReport
              report={reportCurrentlyBeingDeleted}
              OnReportDeleted={OnReportDeleted}
            />
          )}
        </div>
      </div>
    </div>
  );
  // rendering table for reports
  function renderReportTable() {
    return (
      <div className="table-responsive mt-5">
        <table className="table table-bordered border-dark">
          <thead>
            <tr>
              <th scope="col">ReportId</th>
              <th scope="col">Player Key</th>
              <th scope="col">First Name</th>
              <th scope="col">Last Name</th>
              <th scope="col">Comment</th>
              <th scope="col">Shooting Rating</th>
              <th scope="col">Assist Rating</th>
              <th scope="col">Rebound Rating</th>
              <th scope="col">Defense Rating</th>
              <th scope="col">Highlight</th>
              <th scope="col">Update/Delete</th>
            </tr>
          </thead>
          <tbody>
            {reports.map((report) => (
              <tr key={report.ReportId}>
                <th scope="row">{report.ReportId}</th>
                <td>{report.PlayerKey}</td>
                <td>{report.FirstName}</td>
                <td>{report.LastName}</td>
                <td>{report.Comments}</td>
                <td>{report.ShootingRating}</td>
                <td>{report.AssistRating}</td>
                <td>{report.ReboundRating}</td>
                <td>{report.DefenseRating}</td>
                <td>{report.HighlightLink}</td>
                <td>
                  <button
                    onClick={() => setReportCurrentlyBeingUpdated(report)}
                    className="btn btn-outline-dark btn-sm"
                  >
                    Update
                  </button>
                  <button
                    onClick={() => setReportCurrentlyBeingDeleted(report)}
                    className="btn btn-outline-danger btn-sm"
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
  }
  // after submitting new report, change button visibility status using state -byJawa
  function OnReportCreated(createdReport) {
    setShowNewReportForm(false);
    if (createdReport === null) {
      return;
    }
    alert("New report submitted successfully.");

    // call function to refresh the table
    getAllReports();
  }

  function OnReportUpdated(updatedReport) {
    setReportCurrentlyBeingUpdated(null);
    if (updatedReport === null) {
      return;
    }

    let reportsCopy = [...reports];
    const index = reportsCopy.findIndex((reportsCopyRepost, currentIndex) => {
      if (reportsCopyRepost.ReportId === updatedReport.ReportId) {
        return true;
      }
    });
    if (index !== -1) {
      reportsCopy[index] = updatedReport;
    }
    setReports(reportsCopy);
    alert(`Report #${updatedReport.ReportId} successfully updated.`);
    getAllReports();
  }

  
  function OnReportDeleted(deletedReport) {
    setReportCurrentlyBeingDeleted(null);
    if (deletedReport === null) {
      return;
    }

    let reportsCopy = [...reports];
    const index = reportsCopy.findIndex((reportsCopyRepost, currentIndex) => {
      if (reportsCopyRepost.ReportId === deletedReport.ReportId) {
        return true;
      }
    });
    if (index !== -1) {
      reportsCopy[index] = deletedReport;
    }
    setReports(reportsCopy);
    alert(`Report #${deletedReport.ReportId} successfully deleted.`);
    getAllReports();
  }
}
