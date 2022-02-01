import { createContext, useState } from "react";

export const NotificationBarContext = createContext({
  setNotificationBar: () => {},
  snackbar: {},
});

/**
 * Used to wrap all the components that use the NotificationBar.
 * @param {*} children The children of the component.
 */
export const NotificationContextProvider = (props) => {
  const [notificationBar, setNotificationBar] = useState({
    message: "",
    severity: "success",
    autoHideDuration: 6000,
  });

  /**
   * Method passed through context to change the nofication bar contents.
   * @param {*} message The message to display.
   * @param {*} severity The severity of the message.
   * @param {*} autoHideDuration The duration for which to show the message.
   */
  const handlenotificationBarSet = (
    message = "",
    severity,
    autoHideDuration
  ) => {
    setNotificationBar({
      message,
      severity,
      autoHideDuration,
    });
  };

  const contextValue = {
    setNotificationBar: handlenotificationBarSet,
    notificationBar,
  };

  return (
    <NotificationBarContext.Provider value={contextValue}>
      {props.children}
    </NotificationBarContext.Provider>
  );
};
