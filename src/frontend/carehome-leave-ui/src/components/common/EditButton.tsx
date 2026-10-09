import IconButton from "@mui/material/IconButton";
import Tooltip from "@mui/material/Tooltip";
import EditOutlinedIcon from "@mui/icons-material/EditOutlined";

interface EditButtonProps {
  onClick: () => void;
}

export default function EditButton({ onClick }: EditButtonProps) {
  return (
    <Tooltip title="Edit leave request">
      <IconButton
        size="small"
        onClick={onClick}
        sx={{
          border: "1px solid #c1b87f",
          borderRadius: "4px",
          padding: "4px",
        }}
      >
        <EditOutlinedIcon sx={{ fontSize: 18 }} />
      </IconButton>
    </Tooltip>
  );
}