function BecomeSelected(button, ...buttons) {
    document.getElementById(button).setAttribute("class", "Top-Bar TB-Selected");
    for (var i = 0; i < buttons.length; i++) {
        document.getElementById(buttons[i]).setAttribute("class", "Top-Bar TB-Normal");
    }
}