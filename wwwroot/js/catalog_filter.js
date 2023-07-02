let button = document.querySelector('.buttonFilter');
let change = document.querySelector('#catalogMenu');

let modeOfWork= false;

button.onclick = function(){
    if (modeOfWork) 
    {
        change.style.left = "-300px";
        modeOfWork = !modeOfWork;
    }
    else
    {
        change.style.left = "0px";
        modeOfWork = !modeOfWork;
    }
}