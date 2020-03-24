function BecomeSelected(ShowScreen: Boolean, ShowStart: Boolean, button: any, ...buttons: string[]) {
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

function BottomBecomeSelected(button, ...buttons) {
    document.getElementById(button).setAttribute("class", "TB-Tab TB-Tab-Selected TB-Tab-Image");
    for (var i = 0; i < buttons.length; i++) {
        if (buttons[i] != null) {
            document.getElementById(buttons[i]).setAttribute("class", "TB-Tab TB-Tab-Normal TB-Tab-Image");
        }
    }
}    