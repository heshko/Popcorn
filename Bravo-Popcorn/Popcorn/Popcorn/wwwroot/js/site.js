// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// Popup section  //


    // Get the modal
    var modal = document.getElementById("myModal");
    // Get the button that opens the modal
    var btn = document.getElementById("myBtn");
    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];
    // When the user clicks the button, open the modal

    window.onload = function () {
        if (this.document.URL == location.origin + "/"||
            this.document.URL == location.origin + "/Home/Index" || 
            this.document.URL == "https://popcorncinema.azurewebsites.net/")
    {
        modal.style.display = "block";
    }
        
    }
    // When the user clicks on <span> (x), close the modal
    span.onclick = function () {
                modal.style.display = "none";
        }
        // When the user clicks anywhere outside of the modal, close it
    window.onclick = function (event) {
        if (event.target == modal) {
                modal.style.display = "none";
        }
    }