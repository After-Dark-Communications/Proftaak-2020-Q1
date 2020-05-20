function BecomeSelected(ShowScreen, ShowStart, button) {
    var buttons = [];
    for (var _i = 3; _i < arguments.length; _i++) {
        buttons[_i - 3] = arguments[_i];
    }
    document.getElementById(button).setAttribute("class", "Top-Bar TB-Selected");
    for (var i = 0; i < buttons.length; i++) {
        if (buttons[i] != null) {
            document.getElementById(buttons[i]).setAttribute("class", "Top-Bar TB-Normal");
        }
    }
    if (ShowScreen == false) {
        document.getElementById('ScreenContainer').style.display = "none";
    }
    else if (ShowScreen == true) {
        document.getElementById('ScreenContainer').style.display = "inline-block";
    }
    if (ShowStart == false) {
        document.getElementById('StartContainer').style.display = "none";
    }
    else if (ShowStart == true) {
        document.getElementById('StartContainer').style.display = "inline-block";
    }
}

function BottomBecomeSelected(button) {
    var buttons = [];
    for (var _i = 1; _i < arguments.length; _i++) {
        buttons[_i - 1] = arguments[_i];
    }
    document.getElementById(button).setAttribute("class", "TB-Tab TB-Tab-Selected TB-Tab-Image");
    for (var i = 0; i < buttons.length; i++) {
        if (buttons[i] != null) {
            document.getElementById(buttons[i]).setAttribute("class", "TB-Tab TB-Tab-Normal TB-Tab-Image");
        }
    }
    if (button == "RegisterButton") {
        $('#RegisterModal').modal('show');
    }
    if (button == "ReserverButton") {
        $('#ReserverModal').modal('show');
    }
}
//let x: Number = 5;
//function setInt(value: Number) {
//    x = value;
//    alert(x);
//}
//
//function GetInt() {
//    alert(x);
//}
//# sourceMappingURL=ButtonSwitcher.js.map