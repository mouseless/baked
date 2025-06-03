var express = require("express");
var app = express();

app.listen(7806, () => {
 console.log("Server running on port 7806");
});

app.get("/random-names", (_, res, __) => {
  res.json(["John", "Michael", "James", "Rick", "Steven", "Adam", "Mike", "Daniel"]);
});