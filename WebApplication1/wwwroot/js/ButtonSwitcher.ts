function BecomeSelected(ShowBottom: Boolean, button:any, ...buttons:string[]) {
    document.getElementById(button).setAttribute("class", "Top-Bar TB-Selected");
    for (var i = 0; i < buttons.length; i++) {
        document.getElementById(buttons[i]).setAttribute("class", "Top-Bar TB-Normal");
    }
    if (ShowBottom == false) {
        document.getElementById('ConductButton').style.display = "none";
        document.getElementById('MechanicButton').style.display = "none";
        document.getElementById('CleanerButton').style.display = "none";
    }
    else if (ShowBottom == true) {
        document.getElementById('ConductButton').style.display =  "inherit";
        document.getElementById('MechanicButton').style.display = "inherit";
        document.getElementById('CleanerButton').style.display =  "inherit";
    }
}

function BottomBecomeSelected(button, ...buttons) {
    document.getElementById(button).setAttribute("class", "TB-Tab TB-Tab-Selected TB-Tab-Image");
    for (var i = 0; i < buttons.length; i++) {
        document.getElementById(buttons[i]).setAttribute("class", "TB-Tab TB-Tab-Normal TB-Tab-Image");
    }
}    