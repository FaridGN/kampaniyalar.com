
let a = document.getElementById("mobbtn");
a.onclick = function Show() {
    let b = document.getElementById("catblock");
    if (b.style.display == "block") {
        b.style.display = "none";
    }
    else {
        b.style.display = "block";
    }
}

function GetIEVersion() {
    var sAgent = window.navigator.userAgent;
    var Idx = sAgent.indexOf("MSIE");

    // If IE, return version number.
    if (Idx > 0) {
        return parseInt(sAgent.substring(Idx + 5, sAgent.indexOf(".", Idx)));
    }
    // If IE 11 then look for Updated user agent string.
    else if (!!navigator.userAgent.match(/Trident\/7\./)|| window.navigator.userAgent.indexOf("Edge") > -1)
    {
        return 11;
    }
    else {
        return 0; //It is not IE
    }
}

var e = document.getElementById('currency');
var b = document.getElementById('nocurrency');
var s = document.getElementById('searchbtn');

if (GetIEVersion() > 0) {

    e.style.display = 'none';
    b.style.display = 'block';
    s.style.top = "0px";
}

(function () {
    var active;
    var catsNodeList = document.querySelectorAll('.catlist');
    [].forEach.call(catsNodeList, function (item, index, array) {
        item.onclick = function () {
            if (typeof active != 'undefined') active.style.display = '';
            if (this.getElementsByTagName('UL')[0] == active) {
                active.style.display = '';
                active = undefined;
            } else {
                active = this.getElementsByTagName('UL')[0];
                active.style.display = 'block';
            }
        }
    })
}())

/*
function getIPhone() {

    var iOS = /iPad|iPhone|iPod/.test(navigator.userAgent) && !window.MSStream;
    let searchicon = document.getElementById('si');

    if (iOS == true) {
        searchicon.style.left = "-19px";
    }
}

*/

/*
function setIphone() {
    let searchicon = document.getElementById('si');

    if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) {

        searchicon.style.left = "-19px";
    }
}

*/

/*   function changeClass(){
   let a = document.getElementsByClassName("lastinfo");
   let width = window.innerWidth;
    if(width <= 505){
        for(var i=0; i < a.length; i++){
            a[i].className = "mobinfo";
        }
     }
   } */
