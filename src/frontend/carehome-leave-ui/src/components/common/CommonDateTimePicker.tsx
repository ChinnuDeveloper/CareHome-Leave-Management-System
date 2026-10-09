import { alpha, type SxProps, type Theme } from "@mui/material/styles";
import { DatePicker } from "@mui/x-date-pickers"; 
import type { Dayjs } from "dayjs";

/* ------------------------------------------------------------------ */
/* Sizing tokens                                                       */
/* ------------------------------------------------------------------ */
const CALENDAR_WIDTH = 280;
const DAY_SIZE = 32;
const DAY_ROW_HEIGHT = DAY_SIZE + 2; // 1px margin top + bottom
const CALENDAR_BODY_HEIGHT = DAY_ROW_HEIGHT * 6; // always reserve 6 week rows
const TIME_ITEM_HEIGHT = 30;

/* ------------------------------------------------------------------ */
/* Text field (the input that opens the popup)                         */
/* ------------------------------------------------------------------ */
const fieldSx: SxProps<Theme> = {
  "& .MuiPickersInputBase-sectionsContainer": { fontSize: 13 },
  "& .MuiIconButton-root": { padding: "4px" },
  "& .MuiSvgIcon-root": { fontSize: 18 },
};

/* ------------------------------------------------------------------ */
/* Popup (calendar + time columns)                                     */
/* ------------------------------------------------------------------ */
const popperSx: SxProps<Theme> = (theme) => ({
  "& .MuiPickersPopper-paper": {
    borderRadius: "12px",
    border: `1px solid ${theme.palette.divider}`,
    boxShadow: "0 8px 24px rgba(16, 24, 40, 0.12)",
    overflow: "hidden",
  },

  /* ---------- Header: "September 2021" on one line ---------- */
  "& .MuiDateCalendar-root": {
    width: CALENDAR_WIDTH,
    height: "auto",
    maxHeight: "none",
  },
  "& .MuiPickersCalendarHeader-root": {
    display: "flex",
    alignItems: "center",
    justifyContent: "space-between",
    flexWrap: "nowrap",
    margin: 0,
    padding: "8px 8px 4px 12px",
    minHeight: 40,
    maxHeight: "none",
  },
  "& .MuiPickersCalendarHeader-labelContainer": {
    maxHeight: "none",
    flex: "1 1 auto",
    minWidth: 0,
    margin: 0,
    whiteSpace: "nowrap", // keeps "September 2026" on one line
    overflow: "visible",
  },
  "& .MuiPickersCalendarHeader-label": {
    fontSize: 13,
    fontWeight: 600,
    whiteSpace: "nowrap",
    marginRight: "2px",
  },
  "& .MuiPickersCalendarHeader-switchViewButton": {
    padding: "2px",
    "& .MuiSvgIcon-root": { fontSize: 18 },
  },

  /* Prev / next month arrows: fixed size, never shrink, clear gap between */
  "& .MuiPickersArrowSwitcher-root": {
    display: "flex",
    alignItems: "center",
    flexShrink: 0,
    gap: "6px",
  },
  "& .MuiPickersArrowSwitcher-spacer": { display: "none" }, // gap replaces it
  "& .MuiPickersArrowSwitcher-root .MuiIconButton-root": {
    flexShrink: 0,
    width: 32,
    height: 32,
    minWidth: 32,
    padding: 0,
    borderRadius: "8px",
    "&:hover": {
      backgroundColor: alpha(theme.palette.primary.main, 0.08),
    },
    "& .MuiSvgIcon-root": { fontSize: 20 },
  },

  /* ---------- Weekday labels ---------- */
  "& .MuiDayCalendar-header": {
    justifyContent: "space-around",
  },
  "& .MuiDayCalendar-weekDayLabel": {
    width: DAY_SIZE,
    height: 28,
    margin: "0 2px",
    fontSize: 10,
    fontWeight: 600,
    textTransform: "uppercase",
    color: theme.palette.text.secondary,
  },

  /* ---------- Days ---------- */
  "& .MuiPickersSlideTransition-root": {
    minHeight: CALENDAR_BODY_HEIGHT,
  },
  "& .MuiPickersDay-root": {
    width: DAY_SIZE,
    height: DAY_SIZE,
    margin: "1px 2px",
    fontSize: 12,
    borderRadius: "8px",
    "&:hover": {
      backgroundColor: alpha(theme.palette.primary.main, 0.08),
    },
    "&.Mui-selected": {
      backgroundColor: theme.palette.primary.main,
      color: theme.palette.primary.contrastText,
      fontWeight: 600,
      "&:hover": { backgroundColor: theme.palette.primary.dark },
    },
    "&.MuiPickersDay-today:not(.Mui-selected)": {
      border: `1.5px solid ${theme.palette.primary.main}`,
    },
  },

  /* ---------- Year view ---------- */
  "& .MuiYearCalendar-root": {
    width: "100%",
    height: 28 + CALENDAR_BODY_HEIGHT, // same height as the day view
    maxHeight: 28 + CALENDAR_BODY_HEIGHT,
    padding: "0 4px",
    boxSizing: "border-box",
    overflowX: "hidden",
    scrollbarWidth: "thin",
    "&::-webkit-scrollbar": { width: 4 },
    "&::-webkit-scrollbar-thumb": {
      backgroundColor: alpha(theme.palette.text.primary, 0.2),
      borderRadius: 2,
    },
  },
  "& .MuiPickersYear-root": {
    justifyContent: "center",
  },
  "& .MuiPickersYear-yearButton": {
    width: 60, // 4 columns x 68px cell fits inside the 280px calendar
    height: 28,
    margin: "2px 0",
    fontSize: 12,
    borderRadius: "8px",
    "&:hover": {
      backgroundColor: alpha(theme.palette.primary.main, 0.08),
    },
    "&.Mui-selected": {
      backgroundColor: theme.palette.primary.main,
      color: theme.palette.primary.contrastText,
      fontWeight: 600,
    },
  },

  /* ---------- Time columns ---------- */
  "& .MuiMultiSectionDigitalClock-root": {
    borderLeft: `1px solid ${theme.palette.divider}`,
  },
  "& .MuiMultiSectionDigitalClockSection-root": {
    width: 56,
    maxHeight: 44 + 28 + CALENDAR_BODY_HEIGHT, // header + weekdays + 6 week rows
    scrollbarWidth: "thin",
    "&:not(:last-of-type)": {
      borderRight: `1px solid ${theme.palette.divider}`,
    },
    "&::-webkit-scrollbar": { width: 4 },
    "&::-webkit-scrollbar-thumb": {
      backgroundColor: alpha(theme.palette.text.primary, 0.2),
      borderRadius: 2,
    },
  },
  "& .MuiMultiSectionDigitalClockSection-item": {
    minHeight: TIME_ITEM_HEIGHT,
    height: TIME_ITEM_HEIGHT,
    margin: "1px 4px",
    padding: 0,
    fontSize: 12,
    borderRadius: "6px",
    justifyContent: "center",
    "&.Mui-selected": {
      backgroundColor: theme.palette.primary.main,
      color: theme.palette.primary.contrastText,
      fontWeight: 600,
      "&:hover": { backgroundColor: theme.palette.primary.dark },
    },
  },
});

/* ------------------------------------------------------------------ */
/* Action bar (Cancel / OK)                                            */
/* ------------------------------------------------------------------ */
const actionBarSx: SxProps<Theme> = (theme) => ({
  padding: "4px 8px",
  minHeight: 0,
  borderTop: `1px solid ${theme.palette.divider}`,
  "& .MuiButton-root": {
    fontSize: 12,
    fontWeight: 500,
    textTransform: "none", // remove this line to keep CANCEL / OK uppercase
    minWidth: 0,
    padding: "2px 10px",
    lineHeight: 1.6,
  },
});

/* ------------------------------------------------------------------ */
/* Component                                                           */
/* ------------------------------------------------------------------ */
export interface CompactDateTimePickerProps { 
  value: Dayjs | null;
  onChange: (value: Dayjs | null) => void;
  minDateTime?: Dayjs;
  maxDateTime?: Dayjs;
  disabled?: boolean;
  required?: boolean;
  error?: boolean;
  helperText?: string;
  /** 12-hour clock with AM/PM. Defaults to 24-hour. */
  ampm?: boolean;
}

export default function CommonDateTimePicker({ 
  value,
  onChange,
  minDateTime,
  maxDateTime,
  disabled,
  required,
  error,
  helperText
}: CompactDateTimePickerProps) {
  return (
    <DatePicker 
  value={value}
  format="DD-MMM-YY"
  onChange={(newValue) => onChange(newValue)}
  minDate={minDateTime}
  maxDate={maxDateTime}
  disabled={disabled}
  slotProps={{
    textField: {
      fullWidth: true,
      size: "small",
      required,
      error,
      helperText,
      sx: fieldSx,
    },
    actionBar: {
      actions: ["cancel", "accept"],
      sx: actionBarSx,
    },
    popper: {
      sx: popperSx,
    },
  }}
/>
  );
}
