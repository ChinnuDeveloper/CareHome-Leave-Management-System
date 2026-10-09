import apiClient from "../api/apiClient";
import type { CreateEntitlement, Entitlements, UpdateEntitlement } from "../types/entitlement";

export const getEntitlement = async (
  fiscalYearId: number 
): Promise<Entitlements[]> => {
  const response = await apiClient.get<Entitlements[]>(
    `/Entitlements/${fiscalYearId}`
  );

  return response.data;
};

export const getFiscalYear = async()=>{
    const response= await apiClient.get("/Entitlements/fiscal-years");
    return response.data;
} 

export const createEntitlement = async (
  request: CreateEntitlement
): Promise<void> => { 
  await apiClient.post("/Entitlements",
    request
  );
};

export const entitlementExists = async (
    fiscalYearId: number
): Promise<boolean> => {
    const response = await apiClient.get<{ exists: boolean }>(
        `/Entitlements/exists/${fiscalYearId}`
    );

    return response.data.exists;
};

export const getEntitlementDetails = async (
    entitlementId : number
): Promise<Entitlements> => {
    const response = await apiClient.get<Entitlements>(
        `/Entitlements/entitlement/${entitlementId }`
    );

    return response.data;
};

export const updateEntitlement = async (
    entitlementId: number,
    data: UpdateEntitlement
): Promise<void> => {
    await apiClient.put(
        `/Entitlements/${entitlementId}`,
        data
    );
};