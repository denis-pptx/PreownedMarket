import { Card, CardContent, Typography, Button } from "@mui/material";

export default function Home() {
  return (
    <div className="flex justify-center items-center min-h-screen bg-gray-100">
      <Card className="w-full max-w-xl p-6 shadow-lg">
        <CardContent>
          <Typography variant="h4" gutterBottom>
            Добро пожаловать!
          </Typography>
          <Typography variant="body1" className="text-gray-600">
            Lorem ipsum dolor sit amet, consectetur adipiscing elit.
          </Typography>
          <div className="mt-4 flex justify-end">
            <Button variant="contained" color="primary">
              Начать
            </Button>
          </div>
        </CardContent>
      </Card>
    </div>
  );
}
