const google = document.querySelectorAll('.google');

if (google) {
    google.forEach(btn => {
        btn.addEventListener('click', function () {
            console.log("dasd");
            window.location.href = `/Login/AuthenticationGoogle?returnURL=${encodeURIComponent(window.location.href)}`;
        });
    });
};