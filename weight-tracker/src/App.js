import logo from "./logo.svg";
import "./App.css";

import AddWeightForm from "./components/AddWeightForm/AddWeightForm";

const App = () => {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <AddWeightForm />
      </header>
    </div>
  );
};

export default App;
