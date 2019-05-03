function show(x, number, paperQuestionId, a) {
    //getting the question answer
    var answer = document.getElementById('Answer ' + number).innerHTML;

    //hide the starting exam note and show the questions
    document.getElementById('examStart').hidden = true;
    document.getElementById('QuestionTextShown').hidden = false;
    document.getElementById('QuestionAnswer').hidden = false;
    document.getElementById('saveButton').hidden = false;
    document.getElementById('submitButton').hidden = false;
    document.getElementById('CurrentQuestion').innerHTML = paperQuestionId;

    document.getElementById('QuestionTextShown').innerHTML = x;
    document.getElementById('QuestionAnswer').name = number;
    document.getElementById('QuestionAnswer').value = answer;
}

function showChoices(a, b, c, d) {
    document.getElementById('Choice 1').hidden = false;
    document.getElementById('Choice 2').hidden = false;
    document.getElementById('Choice 3').hidden = false;
    document.getElementById('Choice 4').hidden = false;
    document.getElementById('Choice1').hidden = false;
    document.getElementById('Choice2').hidden = false;
    document.getElementById('Choice3').hidden = false;
    document.getElementById('Choice4').hidden = false;
    document.getElementById('Choice1').innerHTML = a;
    document.getElementById('Choice2').innerHTML = b;
    document.getElementById('Choice3').innerHTML = c;
    document.getElementById('Choice4').innerHTML = d;
    document.getElementById('QuestionAnswer').hidden = true;
}


function submit() {
    //getting the answer and number of question at the moment
    var answer;
    if (document.getElementById('Choice1').hidden === false) {
        answer = $("input[name='Choice']:checked").val();
    } else {
        answer = document.getElementById('QuestionAnswer').value;
    }
    var number = document.getElementById('QuestionAnswer').name;

    //setting the answer
    document.getElementById('Answer ' + number).innerHTML = answer;
    document.getElementById('Answer ' + number).setAttribute("submitted", "True");

    //sending the answer to the database
    var enrollment = '@User.Identity.GetUserName()' + "-" + '@examIdSplit[0]';
    var paperQuestion = document.getElementById('CurrentQuestion').innerHTML;
    var studentAnswer = {
        EnrollmentId: enrollment,
        PaperQuestionId: paperQuestion,
        Answer: answer,
        CommittedByStudent: true
    };


    $.ajax({
        type: "POST",
        url: '/Exams/Answer',
        data: studentAnswer,
        datatype: "html"
    });

    //going to the next question
    number = Number(number) + Number(1);
    var nextQuestion = document.getElementById(number);
    if (nextQuestion != null) {
        document.getElementById(number).click();
    } else {
        document.getElementById('examStart').hidden = false;
        document.getElementById('QuestionTextShown').hidden = true;
        document.getElementById('QuestionAnswer').hidden = true;
        document.getElementById('saveButton').hidden = true;
        document.getElementById('submitButton').hidden = true;
        document.getElementById('Choice 1').hidden = true;
        document.getElementById('Choice 2').hidden = true;
        document.getElementById('Choice 3').hidden = true;
        document.getElementById('Choice 4').hidden = true;
        document.getElementById('Choice1').hidden = true;
        document.getElementById('Choice2').hidden = true;
        document.getElementById('Choice3').hidden = true;
        document.getElementById('Choice4').hidden = true;

        //clear the button selection
        var x = document.getElementsByClassName("QuestionButton Active");
        if (x.length != 0) {
            if (document.getElementById("Answer " + x[0].id).innerHTML != "") {
                if (document.getElementById("Answer " + x[0].id).getAttribute("submitted") === "True") {
                    x[0].classList = "QuestionButton Finished";
                } else {
                    x[0].classList = "QuestionButton Started";
                }
            } else {
                x[0].classList = "QuestionButton";
            }
        }

        document.getElementById('examStart').innerHTML = "You have finished the exam!";
    }
}

function save() {
    //getting the answer and number of question at the moment
    var answer;
    if (document.getElementById('Choice1').hidden === false) {
        answer = $("input[name='Choice']:checked").val();
    } else {
        answer = document.getElementById('QuestionAnswer').value;
    }
    var number = document.getElementById('QuestionAnswer').name;

    //setting the answer and the question
    document.getElementById('Answer ' + number).innerHTML = answer;

    //sending the answer to the database
    if (document.getElementById('Choice1').hidden === false) {
        answer = $("input[name='Choice']:checked").val();
    }
    var enrollment = '@User.Identity.GetUserName()' + "-" + '@examIdSplit[0]';
    var paperQuestion = document.getElementById('CurrentQuestion').innerHTML;
    var submitted = false;
    document.getElementById('Answer ' + number).setAttribute("submitted", "False");
    /*
    if (document.getElementById('Answer ' + number).name === "submitted") {
        submitted = true;
    }
    */

    var studentAnswer = {
        EnrollmentId: enrollment,
        PaperQuestionId: paperQuestion,
        Answer: answer,
        CommittedByStudent: submitted
    };
    $.ajax({
        type: "POST",
        url: '/Exams/Answer',
        data: studentAnswer,
        datatype: "html",
        //success:
    });
}

function buttonAction(elmnt, question, number, paperQuestionId, a, b, c, d) {

    var x = document.getElementsByClassName("QuestionButton Active");
    /*
    var i;
    for (i = 0; i < x.length; i++) {
        x[i].classList = "QuestionButton";
    }
    */
    //checking if the previous answer was submitted or not
    if (x.length != 0) {
        if (document.getElementById("Answer " + x[0].id).innerHTML != "") {
            if (document.getElementById("Answer " + x[0].id).getAttribute("submitted") === "True") {
                x[0].classList = "QuestionButton Finished";
            } else {
                x[0].classList = "QuestionButton Started";
            }
        } else {
            x[0].classList = "QuestionButton";
        }
    }

    elmnt.classList = "QuestionButton Active";

    //showing the question
    show(question, number, paperQuestionId, a);

    if (a != " ") {
        showChoices(a, b, c, d);
        if (document.getElementById('QuestionAnswer').value != "") {
            var value = document.getElementById('QuestionAnswer').value;
            document.getElementById('Choice ' + document.getElementById('QuestionAnswer').value).click();
        } else {
            //deselect the radiobuttons
            document.getElementById('Choice 1').checked = false;
            document.getElementById('Choice 2').checked = false;
            document.getElementById('Choice 3').checked = false;
            document.getElementById('Choice 4').checked = false;
        }
    } else {
        document.getElementById('Choice 1').hidden = true;
        document.getElementById('Choice 2').hidden = true;
        document.getElementById('Choice 3').hidden = true;
        document.getElementById('Choice 4').hidden = true;
        document.getElementById('Choice1').hidden = true;
        document.getElementById('Choice2').hidden = true;
        document.getElementById('Choice3').hidden = true;
        document.getElementById('Choice4').hidden = true;
    }
}