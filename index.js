var restify = require('restify');
var builder = require('botbuilder');

var annswerArray = []

// Create bot and add dialogs
var connector = new builder.ChatConnector({
    appId: "44126538-1649-4331-ae87-7c6fee6c4bb3",
    appPassword: "GvHFeg80KeRpdOWYfXm4DiA"
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
	"Have you had an anxiety attack (suddenly feeling fear or panic)?",
    "How often have you been bothered by feeling nervous, anxious or on edge?",
    "How often have you been bothered by not being able to stop or control worrying?",
    "How often have you been bothered by worrying too much about different things?",
    "How often have you been bothered by having trouble relaxing?",
    "How often have you been bothered by being so restless that it is hard to sit still?",
    "How often have you been bothered by becoming easily annoyed or irritable?",
    "How often have you been bothered by feeling afraid as if something awful might happen?",
    "Have you been bothered by worrying about any of the following?",
    "If this questionnaire has highlighted any problems, how difficult have these problems made it for you to do your work, take care of things at home, or get along with other people"
]

var answers = [
	["Not at all", "Several days", "More than half the days", "Nearly every day"],
	["Yes", "No"],
    ["Your health","Your weight or how your look","Little or no sexual desire or pleasure during sex","Difficulties with your partner","The stress of taking care of family members","Stress at work, school or outside home","By financial problems or worreis","Having no one to turn to","Something bad that happened recently", "None of the above"]
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
    	responses.push(results.response)
        builder.Prompts.choice(session, questions[9],  answers[0]);
    },
    
    function (session, results) {
    	responses.push(results.response)
        builder.Prompts.choice(session, questions[10],  answers[0]);
    },
    function (session, results) {
    	responses.push(results.response)
        builder.Prompts.choice(session, questions[11],  answers[0]);
    },
    function (session, results) {
    	responses.push(results.response)
        builder.Prompts.choice(session, questions[12],  answers[0]);
    },
    function (session, results) {
    	responses.push(results.response)
        builder.Prompts.choice(session, questions[13],  answers[0]);
    },
    function (session, results) {
    	responses.push(results.response)
        builder.Prompts.choice(session, questions[14],  answers[0]);
    },
    function (session, results) {
    	responses.push(results.response)
        builder.Prompts.choice(session, questions[15],  answers[0]);
    },
    function (session, results) {
    	responses.push(results.response)
        builder.Prompts.choice(session, questions[16],  answers[2]);
    },
    function (session, results) {
    	responses.push(results.response)
        builder.Prompts.choice(session, questions[17],  answers[0]);
    },
    function (session, results) {
        builder.Prompts.text(session, "The responses are: "+JSON.stringify(responses));
    }
]);


// Setup Restify Server
var server = restify.createServer();
server.post('/', connector.listen());
server.listen(process.env.PORT || 3978, function () {
    console.log('%s listening to %s', server.name, server.url); 
}); 