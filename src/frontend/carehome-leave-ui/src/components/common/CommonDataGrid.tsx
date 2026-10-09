import { DataGrid, type GridColDef, type GridRowId, type GridValidRowModel } from "@mui/x-data-grid";
import Box from "@mui/material/Box";

interface CommonDataGridProps<T extends GridValidRowModel> {
  rows: T[];
  columns: GridColDef<T>[];
   getRowId?: (row: any) => GridRowId;
}

export default function CommonDataGrid<T extends GridValidRowModel>({
  rows,
  columns,
  getRowId,
}: CommonDataGridProps<T>) {
  return (
    <Box className="table-card" sx={{ width: "100%" }}>
      <DataGrid
        getRowId={getRowId}
        rows={rows}
        columns={columns}
        autoHeight
        columnHeaderHeight={30}
        rowHeight={45}
        disableRowSelectionOnClick
        
        sx={{
          width: "100%",
          border: "1px solid #c1b87f",

          "& .MuiDataGrid-columnHeader": {
            backgroundColor: "#3e6c23",
            color: "#FFFFFF",
            borderBottom: "1px solid #c1b87f",
            borderRight: "1px solid #c1b87f",
          },

          "& .MuiDataGrid-columnHeader:first-of-type": {
            borderLeft: "1px solid #c1b87f",
          },

          "& .MuiDataGrid-columnHeaderTitle": {
            color: "#FFFFFF",
            fontWeight: 700,
            fontSize: "12px",
          },

          "& .MuiDataGrid-cell": {
            borderRight: "1px solid #c1b87f",
            borderBottom: "1px solid #c1b87f",
            fontSize: "13px",
          },

          "& .MuiDataGrid-row .MuiDataGrid-cell:first-of-type": {
            borderLeft: "1px solid #c1b87f",
          },

          "& .MuiDataGrid-row:hover": {
            backgroundColor: "#F5F9F6",
          },

          "& .MuiDataGrid-toolbarContainer": {
            padding: "12px",
          },

          "& .MuiDataGrid-footerContainer": {
            fontSize: "12px",
          },

          "& .MuiTablePagination-root": {
            fontSize: "12px",
          },

          "& .MuiTablePagination-selectLabel": {
            fontSize: "12px",
          },

          "& .MuiTablePagination-displayedRows": {
            fontSize: "12px",
          },

          "& .MuiTablePagination-select": {
            fontSize: "12px",
          },
        }}
      />
    </Box>
  );
}