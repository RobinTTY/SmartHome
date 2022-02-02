import "./App.css";

import { createTheme, ThemeProvider } from "@mui/material/styles";
import { lightBlue } from "@mui/material/colors";
import MonitorWeightIcon from "@mui/icons-material/MonitorWeight";
import AddWeightForm from "./components/AddWeightForm/AddWeightForm";
import NotificationBar from "./components/NotificationBar";

const darkTheme = createTheme({
  palette: {
    mode: "dark",
  },
});

const App = () => {
  return (
    <ThemeProvider theme={darkTheme}>
      <header className="App-header">
        <h1 style={{ margin: 0 + "px" }}>Weight Tracking</h1>
        <MonitorWeightIcon sx={{ fontSize: 512, color: lightBlue[200] }} />
        <AddWeightForm />
        <NotificationBar />
      </header>
    </ThemeProvider>
  );
};

export default App;
