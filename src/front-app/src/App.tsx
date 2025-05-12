import { Button, Typography } from "@mui/material";

function App() {
  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100">
      <div className="text-center">
        <Typography variant="h3" className="mb-4">
          Привет, мир!
        </Typography>
        <Button variant="contained" color="primary">
          Добро пожаловать!
        </Button>
      </div>
    </div>
  );
}

export default App;
