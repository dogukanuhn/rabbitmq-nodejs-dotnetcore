const amqp = require("amqplib/callback_api");

const Message = require("./Message");

amqp.connect("amqp://localhost", function (err, conn) {
  if (err) throw err;
  conn.createChannel(function (err, channel) {
    var queue = "FirstQueue";
    channel.assertQueue(queue, {
      durable: false,
      autoDelete: true,
    });

    channel.consume(queue, (x) => {
      console.log(x.content.toString());
      Message.create(JSON.parse(x.content.toString()));
    });
  });
});
