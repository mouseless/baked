const express = require("express");
const cors = require("cors");

const app = express();
const port = 80;

app.use(
  cors({
    allowedHeaders: [ "Access-Control-Allow-Origin", "Authorization", "Origin" ],
    credentials: true,
    methods: "GET",
    origin: [ "http://localhost:5151" ],
    preflightContinue: false,
  })
);

app.listen(port, () => {
  console.log(`Server running on port ${port}`);
});

app.get("/random-names", (_, res, __) => {
  res.json(["John", "Michael", "James", "Rick", "Steven", "Adam", "Mike", "Daniel"]);
});