import { useEffect, useState } from "react";  
import "../styles/Employee.css";  
import GenerateEntitlementDialog from "../components/common/Dialog/GenerateEntitlementDialog";
import EntitlementTable from "../components/DataGridTable/EntitlementTable";
import { getEntitlement, getFiscalYear } from "../services/entitlementService"; 
import type { Entitlements, FiscalYear } from "../types/entitlement";
import { Box } from "@mui/material";
import { ChevronDown } from "lucide-react";

export default function EntitlementsPage() {

  const [requests, setRequests] = useState<Entitlements[]>([]);
  const fiscalYearId = 1;  

  const [openCreateDialog, setOpenCreateDialog] = useState(false);
  const [selectedYear, setSelectedYear] = useState<number | null>(null); 
  const [fiscalYear, setFiscalYear] = useState<FiscalYear[]>([]); 

  useEffect(() => {
        const loadFiscalYears = async () => {
          try {
            const data = await getFiscalYear();  
            setFiscalYear(data);
            setSelectedYear(fiscalYearId);
          } catch (error) {
            console.error("Failed to load fiscal year:", error);
          }
        };
      
        loadFiscalYears(); 
      }, []);

   const handleYearChange = (
            e: React.ChangeEvent<HTMLSelectElement>
            ) => {
            const value = e.target.value;
 

            if (value === "") {
                setSelectedYear(null);
                return;
            } 
            setSelectedYear(Number(value)); 
            loadData();
            };
 
 const loadData = async () => {
    if (selectedYear === null) {
        setRequests([]);
        return;
    }

    try {
        const data = await getEntitlement(selectedYear);

        const rows = data.map((request) => ({
            ...request,
            balanceHours:
                (request.allocatedHrs ?? 0) - (request.takenHrs ?? 0),
        }));

        setRequests(rows);
    } catch (error) {
        console.error("Error loading data:", error);
    }
};

useEffect(() => {
    loadData();
}, [selectedYear]); 

const handleSubmit = async () => {  setOpenCreateDialog(true);};

  return (
    <>  
         
        <Box className="date-grid">
    <Box className="field year-field">
        <label>Year</label>

        <div className="select-wrapper">
            <select
                value={selectedYear ?? ""}
                onChange={handleYearChange}
            >
                <option value="" disabled>
                    Select
                </option>

                {fiscalYear?.map((year) => (
                    <option
                        key={year.fiscalYearId}
                        value={year.fiscalYearId}
                    >
                        {year.year}
                    </option>
                ))}
            </select>

            <ChevronDown size={18} className="select-icon" />
        </div>
    </Box>
</Box>

      <div className="leave-stat" style={{ marginBottom: "10px" }}>      
        <button
              type="button"
              className="new-button"
              onClick={handleSubmit}>Generate Entitlement</button>
        </div> 
    <EntitlementTable requests={requests} onRefresh={loadData}/>
    <GenerateEntitlementDialog open={openCreateDialog} onClose={() => {setOpenCreateDialog(false); loadData();}} />
    </>
  );
}