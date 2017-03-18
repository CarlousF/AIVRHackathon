var restify = require('restify');
var builder = require('botbuilder');

// Create bot and add dialogs
var connector = new builder.ChatConnector({
    appId: "44126538-1649-4331-ae87-7c6fee6c4bb3",
    appPassword: "OgAE5bXq6knVGHMBqeGxUms"
});
var bot = new builder.UniversalBot(connector);  
bot.dialog('/', function (session) {
    session.send('Hello World');
});

// Setup Restify Server
var server = restify.createServer();
server.post('/api/messages', connector.listen());
server.listen(process.env.port || 3978, function () {
    console.log('%s listening to %s', server.name, server.url); 
});