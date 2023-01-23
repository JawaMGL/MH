import React, { useState } from "react";
import Variables from "../Utilities/Variables";
export default function DeleteReport(props) {
  // creating object for placeholder of form -byJawa
  const initialFormData = Object.freeze({
    PlayerKey: props.report.PlayerKey,
    TeamKey: props.report.TeamKey,
    ScoutKey: props.report.ScoutKey,
    Comments: props.report.Comments,
    ShootingRating: props.report.ShootingRating,
    AssistRating: props.report.AssistRating,
    ReboundRating: props.report.ReboundRating,
    DefenseRating: props.report.DefenseRating,
    Highlight: props.report.HighlightLink,
  });

  // declaring formData using useState and put above object as initial value. -byJawa
  const [formData, setFormData] = useState(initialFormData);

  // function handleChange using setFormData, changing target value only and rest of formData. -byJawa
  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };
  // function handleSubmit, creating object reportToCreate, and filling data from formData state. -byJawa
  const handleSubmit = (e) => {
    e.preventDefault();
    const reportToDelete = {
      ReportId: props.report.ReportId
    };

    
    const url = Variables.API_URL_DELETE_REPORT;
    // using above url variable, and fetch data to server -byJawa
    fetch(url, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(reportToDelete),
    })
      .then((response) => response.json())
      .then((reportsToServer) => {
        console.log(reportsToServer);
        setFormData(reportsToServer);
      })
      .catch((error) => {
        console.log(error);
        alert(error);
      });
      // after successfully created, send reportToCreate object to OnReportCreated function using props -byJawa
    props.OnReportDeleted(reportToDelete);
  };

  return (
    // creating form of new report -byJawa
    <div>
      <form className="w-100 px-5">
        <h1 className="mt-5">Deleting the report #{props.report.ReportId}</h1>
        <div className="mt-4">
          <label className="h3 form-label">Player Key</label>
          <input
            value={formData.PlayerKey}
            disabled={true}
            name="PlayerKey"
            className="form-control"
            onChange={handleChange}
          />
        </div>
        <div className="mt-4">
          <label className="h3 form-label">Team Key</label>
          <input
            disabled={true}
            value={formData.TeamKey}
            name="TeamKey"
            className="form-control"
            onChange={handleChange}
          />
        </div>
        <div className="mt-4">
          <label className="h3 form-label">Scout Key</label>
          <input
            disabled={true}
            value={formData.ScoutKey}
            name="ScoutKey"
            className="form-control"
            onChange={handleChange}
          />
        </div>
        <div className="mt-4">
          <label className="h3 form-label">Comments</label>
          <input
            value={formData.Comments}
            name="Comments"
            className="form-control"
            onChange={handleChange}
          />
        </div>
        <div className="mt-4">
          <label className="h3 form-label">Shooting Rating</label>
          <input
            value={formData.ShootingRating}
            name="ShootingRating"
            className="form-control"
            onChange={handleChange}
          />
        </div>
        <div className="mt-4">
          <label className="h3 form-label">Assist Rating</label>
          <input
            value={formData.AssistRating}
            name="AssistRating"
            className="form-control"
            onChange={handleChange}
          />
        </div>
        <div className="mt-4">
          <label className="h3 form-label">ReboundRating</label>
          <input
            value={formData.ReboundRating}
            name="ReboundRating"
            className="form-control"
            onChange={handleChange}
          />
        </div>
        <div className="mt-4">
          <label className="h3 form-label">DefenseRating</label>
          <input
            value={formData.DefenseRating}
            name="DefenseRating"
            className="form-control"
            onChange={handleChange}
          />
        </div>
        <div className="mt-4">
          <label className="h3 form-label">Highlight</label>
          <input
            value={formData.Highlight}
            name="Highlight"
            className="form-control"
            onChange={handleChange}
          />
        </div>
        {/* submit button, onClick -byJawa*/}
        <button
          onClick={handleSubmit}
          className="btn-outline-dark btn-lg w-100 mt-5"
        >
          Delete
        </button>
        {/* cancel button, send null object to OnReportUpdated function -byJawa */}
        <button
          onClick={() => props.OnReportDeleted(null)}
          className="btn-outline-danger btn-lg w-100 mt-3"
        >
          Cancel
        </button>
      </form>
    </div>
  );
}
