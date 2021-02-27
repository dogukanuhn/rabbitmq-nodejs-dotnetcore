const mongoose = require("mongoose");

mongoose.connect("mongodb://localhost/Rabbitmq-test", {
  useNewUrlParser: true,
  useUnifiedTopology: true,
});

const Schema = mongoose.Schema;

var messageScheme = new Schema({
  Title: String,
  Content: String,
});

module.exports = mongoose.model("Message", messageScheme);
