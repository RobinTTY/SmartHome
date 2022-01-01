import { useState } from "react";
import Button from "@mui/material/Button";

const AddWeightForm = (props) => {
  const [title, setTitle] = useState(props.title);

  const addWeightHandler = () => {};

  return (
    <Button variant="contained" onClick={addWeightHandler}>
      Add weight
    </Button>
  );
};

export default AddWeightForm;
