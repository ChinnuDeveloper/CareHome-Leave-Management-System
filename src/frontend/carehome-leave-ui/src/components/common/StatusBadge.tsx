import Box from "@mui/material/Box";

interface StatusBadgeProps {
  status: string;
}

const statusStyles: Record<
  string,
  { backgroundColor: string; color: string }
> = {
  Approved: {
    backgroundColor: "#E3F2E5",
    color: "#2E6B35",
  },
   Active: {
    backgroundColor: "#E3F2E5",
    color: "#2E6B35",
  },
  Pending: {
    backgroundColor: "#FFF4CC",
    color: "#8A6D1D",
  },
  Rejected: {
    backgroundColor: "#FDE7E7",
    color: "#B42318",
  },
  Cancelled: {
    backgroundColor: "#E9ECEF",
    color: "#5F6368",
  },
  Inactive: {
    backgroundColor: "#E9ECEF",
    color: "#5F6368",
  },
};

export default function StatusBadge({ status }: StatusBadgeProps) {
  const style = statusStyles[status] ?? {
    backgroundColor: "#F1F3F4",
    color: "#5F6368",
  };

  return (
    <Box
      sx={{
        backgroundColor: style.backgroundColor,
        color: style.color,
        borderRadius: "20px",
        padding: "4px 12px",
        fontSize: "11px",
        fontWeight: 550,
        display: "inline-flex",
        alignItems: "center",
        justifyContent: "center",
        height: "19px",
      }}
    >
      {status}
    </Box>
  );
}