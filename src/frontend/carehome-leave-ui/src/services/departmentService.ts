import apiClient from "../api/apiClient";

export const getDepartments = async()=>{
    const response= await apiClient.get("/departments");
    return response.data;
}