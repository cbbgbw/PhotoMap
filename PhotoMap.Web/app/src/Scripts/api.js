import axios from "axios" 

const API_URL = "https://photomapapi.azurewebsites.net/"
export default axios.create({
    baseURL: API_URL
  });

