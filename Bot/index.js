var restify = require('restify');
var builder = require('botbuilder');

var annswerArray = []

// Create bot and add dialogs
var connector = new builder.ChatConnector({
    appId: "44126538-1649-4331-ae87-7c6fee6c4bb3",
    appPassword: "OgAE5bXq6knVGHMBqeGxUms"
});

var questions = [
	"How often have you had little interest or pleasure in doing things?",
	"How often have you been bothered by feeling down, depressed or hopeless?",
	"How often have you been bothered by trouble falling or staying asleep, or sleeping too much?",
	"How often have you been bothered by feeling tired or having little energy?",
	"How often have you been bothered by poor appetite or overeating?",
	"How often have you been bothered by feeling bad about yourself, or that you are a failure, or have let yourself or your family down?",
	"How often have you been bothered by trouble concentrating on things, such as reading the newspaper or watching television?",
	"How often have you been bothered by moving or speaking so slowly that other people could have noticed, or the opposite - being so fidgety or restless that you have been moving around a lot more than usual?",
	"Have you had an anxiety attack (suddenly feeling fear or panic)?"
]

var answers = [
	["Not at all", "Several days", "More than half the days", "Nearly every day"],
	["Yes", "No"]
]

var responses = []

var bot = new builder.UniversalBot(connector, [
    function (session) {
        builder.Prompts.choice(session, questions[0], answers[0]);
    },
    function (session, results) {
    	responses.push(results.response)
        builder.Prompts.choice(session, questions[1],  answers[0]);
    },
    function (session, results) {
    	responses.push(results.response)
        builder.Prompts.choice(session, questions[2],  answers[0]);
    },
    function (session, results) {
    	responses.push(results.response)
        builder.Prompts.choice(session, questions[3],  answers[0]);
    },
    function (session, results) {
    	responses.push(results.response)
        builder.Prompts.choice(session, questions[4],  answers[0]);
    },
    function (session, results) {
    	responses.push(results.response)
        builder.Prompts.choice(session, questions[5],  answers[0]);
    },
    function (session, results) {
    	responses.push(results.response)
        builder.Prompts.choice(session, questions[6],  answers[0]);
    },
    function (session, results) {
    	responses.push(results.response)
        builder.Prompts.choice(session, questions[7],  answers[0]);
    },
    function (session, results) {
    	responses.push(results.response)
        builder.Prompts.choice(session, questions[8],  answers[1]);
    },
    function (session, results) {
        builder.Prompts.text(session, "The responses are: "+JSON.stringify(responses));
    }
]);

// Setup Restify Server
var server = restify.createServer();
server.post('/api/messages', connector.listen());
server.listen(process.env.port || 3978, function () {
    console.log('%s listening to %s', server.name, server.url); 
}); 