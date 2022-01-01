import { useState } from "react";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import TextField from "@mui/material/TextField";
import InputAdornment from "@mui/material/InputAdornment";
import Box from "@mui/material/Box";

const AddWeightForm = (props) => {
  const [weight, setWeight] = useState(0);

  const handleSubmit = (event) => {
    // prevent page reload
    event.preventDefault();

    // allows us to use the key/value pairs
    const data = new FormData(event.currentTarget);

    // TODO: add to database
    console.log({
      weight: data.get("weight"),
    });
  };

  /**
   * Handles the change event for the weight input.
   * @param {Event} e - The event object
   */
  const weightChangeHandler = (e) => {
    const re = /^[0-9\b]+$/;

    // if weight is not blank, then test if only numbers are entered
    if (e.target.value === "" || re.test(e.target.value)) {
      setWeight(e.target.value);
    }
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
