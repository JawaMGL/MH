// declaring api url for development and production -byJawa
const API_BASE_URL_DEV = 'https://localhost:7225';
const API_BASE_URL_PROD = 'https://app.azure.net';

// declaring endpoints of report -byJawa
const ENDPOINTS = {
    GetAllReports:'api/Report/GetAllReports',
    GetReportsByScoutKey:'api/Report/GetReportsByScoutKey',
    CreateNewReport:'api/Report/CreateNewReport',
    UpdateReport:'api/Report/UpdateReport',
    DeleteReport:'api/Report/DeleteReport'
}

// declaring full api urls of development -byJawa
const development ={
    API_URL_GET_ALL_REPORTS:`${API_BASE_URL_DEV}/${ENDPOINTS.GetAllReports}`,
    API_URL_GET_REPORTS_BY_SCOUTKEY:`${API_BASE_URL_DEV}/${ENDPOINTS.GetReportsByScoutKey}`,
    API_URL_CREATE_NEW_REPORT:`${API_BASE_URL_DEV}/${ENDPOINTS.CreateNewReport}`,
    API_URL_UPDATE_REPORT:`${API_BASE_URL_DEV}/${ENDPOINTS.UpdateReport}`,
    API_URL_DELETE_REPORT:`${API_BASE_URL_DEV}/${ENDPOINTS.DeleteReport}`
}

// declaring full api urls of production -byJawa
const production ={
    API_URL_GET_ALL_REPORTS:`${API_BASE_URL_PROD}/${ENDPOINTS.GetAllReports}`,
    API_URL_GET_REPORTS_BY_SCOUTKEY:`${API_BASE_URL_PROD}/${ENDPOINTS.GetReportsByScoutKey}`,
    API_URL_CREATE_NEW_REPORT:`${API_BASE_URL_PROD}/${ENDPOINTS.CreateNewReport}`,
    API_URL_UPDATE_REPORT:`${API_BASE_URL_PROD}/${ENDPOINTS.UpdateReport}`,
    API_URL_DELETE_REPORT:`${API_BASE_URL_PROD}/${ENDPOINTS.DeleteReport}`
}

// checking Node environment and setting URL for API 
const Variables = process.env.NODE_ENV === "development" ? development : production;

export default Variables;
