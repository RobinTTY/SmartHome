import logo from "./logo.svg";
import "./App.css";

import { createTheme, ThemeProvider } from "@mui/material/styles";
import AddWeightForm from "./components/AddWeightForm/AddWeightForm";

const darkTheme = createTheme({
  palette: {
    mode: "dark",
  },
});

const App = () => {
  return (
    <ThemeProvider theme={darkTheme}>
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <AddWeightForm />
      </header>
    </ThemeProvider>
  );
};

export default App;
