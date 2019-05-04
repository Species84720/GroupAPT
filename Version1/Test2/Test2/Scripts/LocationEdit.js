function LocationChange(number) {
    var locationSubmission;
    var checkSelection = true;
    if (number === 1) {
        document.getElementById("location2").hidden = true;
        document.getElementById("location3").hidden = true;
        document.getElementById("location4").hidden = true;
        document.getElementById("location5").hidden = true;

        var choice1 = document.getElementById("LocationChange1");
        locationSubmission = {
            campus: choice1.options[choice1.selectedIndex].text,
            building: null,
            floor: null,
            block: null,
            room: null
        };
        if (choice1.selectedIndex !== 0) {
            checkSelection = false;
            document.getElementById("location2").hidden = false;
        }
    }
    else if (number === 2) {
        document.getElementById("location2").hidden = false;
        document.getElementById("location3").hidden = true;
        document.getElementById("location4").hidden = true;
        document.getElementById("location5").hidden = true;

        var choice1 = document.getElementById("LocationChange1");
        var choice2 = document.getElementById("LocationChange2");
        locationSubmission = {
            campus: choice1.options[choice1.selectedIndex].text,
            building: choice2.options[choice2.selectedIndex].text,
            floor: null,
            block: null,
            room: null
        };
        if (choice2.selectedIndex !== 0) {
            checkSelection = false;
            document.getElementById("location3").hidden = false;
        }
    }
    else if (number === 3) {
        document.getElementById("location2").hidden = false;
        document.getElementById("location3").hidden = false;
        document.getElementById("location4").hidden = true;
        document.getElementById("location5").hidden = true;

        var choice1 = document.getElementById("LocationChange1");
        var choice2 = document.getElementById("LocationChange2");
        var choice3 = document.getElementById("LocationChange3");
        locationSubmission = {
            campus: choice1.options[choice1.selectedIndex].text,
            building: choice2.options[choice2.selectedIndex].text,
            floor: choice3.options[choice3.selectedIndex].text,
            block: null,
            room: null
        };
        if (choice3.selectedIndex !== 0) {
            checkSelection = false;
            document.getElementById("location4").hidden = false;
        }
    }
    else if (number === 4) {
        document.getElementById("location2").hidden = false;
        document.getElementById("location3").hidden = false;
        document.getElementById("location4").hidden = false;
        document.getElementById("location5").hidden = true;

        var choice1 = document.getElementById("LocationChange1");
        var choice2 = document.getElementById("LocationChange2");
        var choice3 = document.getElementById("LocationChange3");
        var choice4 = document.getElementById("LocationChange4");
        locationSubmission = {
            campus: choice1.options[choice1.selectedIndex].text,
            building: choice2.options[choice2.selectedIndex].text,
            floor: choice3.options[choice3.selectedIndex].text,
            block: choice4.options[choice4.selectedIndex].text,
            room: null
        };
        if (choice4.selectedIndex !== 0) {
            checkSelection = false;
            document.getElementById("location5").hidden = false;
        }
    }
    else if (number === 5) {
        var choice1 = document.getElementById("LocationChange1");
        var choice2 = document.getElementById("LocationChange2");
        var choice3 = document.getElementById("LocationChange3");
        var choice4 = document.getElementById("LocationChange4");
        var choice5 = document.getElementById("LocationChange5");
        locationSubmission = {
            campus: choice1.options[choice1.selectedIndex].text,
            building: choice2.options[choice2.selectedIndex].text,
            floor: choice3.options[choice3.selectedIndex].text,
            block: choice4.options[choice4.selectedIndex].text,
            room: choice5.options[choice5.selectedIndex].text
        };
        if (choice5.selectedIndex !== 0) {
            checkSelection = false;
        }
    }

    if (checkSelection === false) {
        $.ajax({
            url: "/ExamSessions/AjaxPost",
            type: "POST",
            dataType: "json",
            data: locationSubmission,
            success: function (data) {
                if (data.Id === 0) {
                    var EditedNumber = parseInt(number) + parseInt("1");
                    document.getElementById("location" + EditedNumber + "Dropdown").innerHTML = '';
                    var div = document.querySelector("#location" + EditedNumber + "Dropdown");
                    var select = document.createElement("select");
                    select.setAttribute("id", "LocationChange" + EditedNumber);
                    select.setAttribute("class", "form-control form-style");
                    select.setAttribute("onchange", "LocationChange(" + EditedNumber + ")");
                    select.setAttribute("name", "LocationId" + EditedNumber);

                    if (EditedNumber === 2)
                        select.options.add(new Option("Select Building"));
                    else if (EditedNumber === 3)
                        select.options.add(new Option("Select Floor"));
                    else if (EditedNumber === 4)
                        select.options.add(new Option("Select Block"));
                    else if (EditedNumber === 5)
                        select.options.add(new Option("Select Room"));

                    for (var i in data.location) {
                        var location = data.location[i];
                        select.options.add(new Option(location));
                    }

                    div.appendChild(select);
                }
                //if the data is of the room itself
                else {
                    var FinalChoice = document.getElementById("LocationChange5");
                    //document.getElementById("LocationId").value = data.Id;
                    FinalChoice.options[FinalChoice.selectedIndex].value = data.Id;
                    FinalChoice.setAttribute("name", "LocationId");
                }
            }
        });
    }
}