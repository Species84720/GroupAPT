var Choice = document.getElementById("selectingType");
if (Choice.options[Choice.selectedIndex].text === "MultipleChoice") {
    document.getElementById("MultipleChoice").hidden = false;
    document.getElementById("MultipleChoice").disabled = false;
    document.getElementById("question_SampleAnswer").readOnly = true;
}

function MultipleChoiceOption() {
    var Choice = document.getElementById("selectingType");
    var QuestionType = Choice.options[Choice.selectedIndex].text;
    if (QuestionType === "MultipleChoice") {
        document.getElementById("MultipleChoice").hidden = false;
        document.getElementById("MultipleChoice").disabled = false;
        document.getElementById("question_SampleAnswer").readOnly = true;
    }
    else {
        document.getElementById("MultipleChoice").hidden = true;
        document.getElementById("MultipleChoice").disabled = true;
        document.getElementById("question_SampleAnswer").readOnly = false;
    }
}

function RadioChange(letter) {
    document.getElementById("question_SampleAnswer").value = document.getElementById("Choice " + letter).value;

    if (letter === "A") {
        document.getElementById("CorrectChoiceSelect").selectedIndex = 1;
    }
    else if (letter === "B") {
        document.getElementById("CorrectChoiceSelect").selectedIndex = 2;
    }
    else if (letter === "C") {
        document.getElementById("CorrectChoiceSelect").selectedIndex = 3;
    }
    else if (letter === "D") {
        document.getElementById("CorrectChoiceSelect").selectedIndex = 4;
    }

    //checking if there is a 0 or not
    var Selected = document.getElementById("CorrectChoiceSelect");
    var Result = Selected.options[Selected.selectedIndex];
    if (Result === undefined) {
        document.getElementById("CorrectChoiceSelect").selectedIndex = 3;
    }
    else if (Result.text !== letter) {
        document.getElementById("CorrectChoiceSelect").selectedIndex = document.getElementById("CorrectChoiceSelect").selectedIndex - 1;
    }
}

function ChoiceTextChange(letter) {
    if (document.getElementById("Choice" + letter).checked === true) {
        document.getElementById("question_SampleAnswer").value = document.getElementById("Choice " + letter).value;
    }
}