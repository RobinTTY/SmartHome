import { useContext } from "react";
import { Snackbar, Alert } from "@mui/material";
import { NotificationBarContext } from "./context/NotificationBarContext";

const NotificationBar = () => {
  const { notificationBar, setNotificationBar } = useContext(
    NotificationBarContext
  );

  const handleClose = (_, reason) => {
    if (reason === "clickaway") {
      return;
    }

    setNotificationBar("", notificationBar.severity, 6000);
  };

  return (
    <>
      <Snackbar
        anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
        open={!!notificationBar.message}
        autoHideDuration={notificationBar.autoHideDuration}
        onClose={handleClose}
      >
        <Alert
          onClose={handleClose}
          severity={notificationBar.severity}
          sx={{ width: "100%" }}
        >
          {notificationBar.message}
        </Alert>
      </Snackbar>
    </>
  );
};

export default NotificationBar;
