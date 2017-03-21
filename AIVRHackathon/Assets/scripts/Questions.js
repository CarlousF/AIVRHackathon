#pragma strict


function Start () {
    yield WaitForSeconds (5);
    questionNumber ++;
    yield WaitForSeconds (5);
    questionNumber ++;
    yield WaitForSeconds (5);
    questionNumber ++;
    yield WaitForSeconds (5);
    questionNumber ++;
    yield WaitForSeconds (5);
    Application.LoadLevel("Beach scene");
}


var questionText : TextMesh; 
var AnswerText1 : TextMesh; 
var AnswerText2 : TextMesh; 
var AnswerText3 : TextMesh; 
var AnswerText4 : TextMesh; 
//questionText : TextMesh = gameObject.GetComponent(TextMesh);

var questionNumber = 1;

function Update () {
    if(questionNumber == 1){
        questionText.text = "How often have you had little interest or pleasure in doing things?";
    }
    if(questionNumber == 2){
        questionText.text = "How often have you been bothered by feeling down, depressed or hopeless?";
    }
    if(questionNumber == 3){
        questionText.text = "How often have you been bothered by feeling tired or having little energy?";
    }
    if(questionNumber == 4){
        questionText.text = "How often have you been bothered by poor appetite or overeating?";
    }
    if(questionNumber == 5){
        questionText.text = "How often have you been bothered by feeling nervous, anxious or on edge?";
    }
    


    var hit : RaycastHit;
    var ray : Ray;

    if(Physics.Raycast( ray, hit) ){
        if(hit.collider.name.length < 3){
            questionNumber ++;
        }
    }
}
