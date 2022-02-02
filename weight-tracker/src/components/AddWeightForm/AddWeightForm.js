import { useState, useContext } from "react";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import TextField from "@mui/material/TextField";
import InputAdornment from "@mui/material/InputAdornment";
import Box from "@mui/material/Box";
import { NotificationBarContext } from "../context/NotificationBarContext";

const AddWeightForm = (props) => {
  const [weight, setWeight] = useState(0);
  const { setNotificationBar } = useContext(NotificationBarContext);

  /**
   * Handles the submission of a weight to the API.
   * @param {*} event The event containing the weight.
   */
  const handleSubmit = (event) => {
    // prevent page reload
    event.preventDefault();

    // allows us to use the key/value pairs
    const data = new FormData(event.currentTarget);
    let promise = postNewWeight();
    promise
      .then(() => {
        setNotificationBar("Weight successfully added", "success", 6000);
      })
      .catch(() => {
        setNotificationBar("Could not reach API", "error", 6000);
      });
  };

  /**
   * Handles the change event for the weight input.
   * @param {Event} e - The event object.
   */
  const weightChangeHandler = (e) => {
    const re = /^[0-9\b]+(\.([0-9\b]+)?)?$/;

    // if weight is not blank, then test if only numbers are entered
    if (e.target.value === "" || re.test(e.target.value)) {
      setWeight(e.target.value);
    }
  };

  /**
   * Posts a new weight through the API to the database.
   */
  const postNewWeight = () => {
    return fetch("http://localhost:5262/weightData", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        timestamp: new Date(),
        weight: weight,
      }),
    }).then((res) => res.json());
  };

  return (
    <>
      <Typography component="h1" variant="h5">
        Add a new weight:
      </Typography>
      <Box
        component="form"
        onSubmit={handleSubmit}
        noValidate
        sx={{
          marginTop: 2,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <TextField
          label="Current Weight"
          id="weight"
          name="weight"
          sx={{ m: 1, width: "20ch" }}
          InputProps={{
            endAdornment: <InputAdornment position="end">kg</InputAdornment>,
          }}
          value={weight}
          onChange={weightChangeHandler}
        />
        <Button variant="contained" type="submit">
          Add weight
        </Button>
      </Box>
    </>
  );
};

export default AddWeightForm;
