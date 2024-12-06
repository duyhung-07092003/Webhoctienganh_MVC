window.onload = function () {
    render();
}

function render() {
    window.recaptchaVerifier = new firebase.auth.RecaptchaVerifier('recaptcha-container');
    recaptchaVerifier.render();
}

function phoneAuth() {
    debugger
    var a = document.getElementById("number").value;
    var b = "+84";
    var number = b + a;

    firebase.auth().signInWithPhoneNumber(number, this.window.recaptchaVerifier).then(function (confirmationResult) {
        // is in lowercase
        this.window.confirmationResult = confirmationResult;
        coderesult = confirmationResult;
        console.log(coderesult);
        alert("Message Sent");
    }).catch(function (error) {
        alert(error.message);
    });
}

function codeverify() {
    debugger
    var code = document.getElementById("verificationCode").value;
    coderesult.confirm(code).then(function (result) {
        alert("Message Verified");
        var user = result.user;
        window.location.href = "/StartPage/registration";
    }).catch(function (error) {
        alert(error.message);
    });
}