// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

if (localStorage.getItem("login") == "true") {
    let elements = document.getElementsByClassName("hidden")
    for (let i = 0; i < elements.length; i++) {
        console.log(elements[i].innerHTML)
        elements[i].removeAttribute("hidden");
    }
    document.getElementById("login").hidden = true;
}
else {
    document.getElementById("login").removeAttribute("hidden");
   
}

