// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.




var queue = [];
const anteText = "Acest cod este valabil pentru urmatoarele";
const postText = "secunde."
//const timpAcum = new Date().getTime();
let dataCurenta = new Date();
console.log("dataCurenta = ", dataCurenta);

let timpCurent = dataCurenta.getTime();
console.log("timpCurent = ", timpCurent);

dataCurenta.setSeconds(dataCurenta.getSeconds() + 30);
let timpCurentPlus30Secunde = dataCurenta.getTime();
console.log("timpCurentPlus30Secunde = ", timpCurentPlus30Secunde);


function heartBeat(interval) {
    for (let i = 0; i < queue.length; i++) {
        if (queue[i]) {
            console.log(queue[i].nume);
            // execute each tick function
            queue[i].tick();

        }
    }
    setTimeout(
        () => heartBeat(interval), interval
    );
}
function queueKill(nume) {
    for (let i = 0; i < queue.length; i++) {
        if (queue[i].nume === nume)
            queue.splice(i, 1);
    }
}
let contor = {
    nume: 'contor',
    tick: function () {
        let timpAcum = new Date().getTime();
        console.log("TimpAcum = ", timpAcum);
        let interval = timpCurentPlus30Secunde - timpAcum;
        console.log("Interval temporar = ", interval);
        let secunde = Math.floor((interval % (1000 * 60)) / 1000);
        console.log("secunde = ", secunde);
        document.getElementById("contor").innerHTML = anteText.concat(" ", secunde, " ", postText);
        if (interval < 0) {
            queueKill('contor');
            document.getElementById("contor").innerHTML = "Acest cod nu mai este valid, te rog cere un nou cod.";
            document.getElementById("valideaza-otp").disabled = true;
            // const regenereazaOtpButton = document.getElementsByClassName("regenereaza-otp")[0];
            const regenereazaOtpButton = document.getElementById("regenereaza-otp");
            console.log(regenereazaOtpButton);
            regenereazaOtpButton.className = regenereazaOtpButton.className.replace('invisible', '');
        }
    }
}
queue.push(contor);
heartBeat(1000);


