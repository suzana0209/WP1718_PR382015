WebAPI.controller('JSRegILogController', function ($scope, RegILogFactory, $window, $rootScope) {
    $scope.user = {}; //inicijalizacija na prazno
    function init() {
        console.log('Login controller initialized'); //ispis na konzoli da se inicijalizovao
    };
    init();
    $scope.RegisterUser = function (user) {
        if (user.username == null || user.username == "") {
            alert('Korisnicko ime ne smije biti prazno!');
            return;
        }
        else if (user.ime == null || user.ime == "") {
            alert('Ime ne smije biti prazno!');
            return;
        }
        else if (user.prezime == null || user.prezime == "") {
            alert('Prezime ne smije biti prazno !');
            return;
        }
        else if (user.pol == null || user.pol == "") {
            alert('Izaberite pol!');
            return;
        }
        else if (user.jmbg == null || user.jmbg == "") {
            alert('Jmbg ne smije biti prazno!');
            return;
        }
        else if (user.jmbg.match(/[a-z]/i)) {
            alert('Jmbg ne smije da sadrzi slova! ');
            return;
        }
        else if (user.jmbg.length != 13) {
            alert('Jmbg mora da sadrzi 13 cifara');
            return;
        }
        else if (user.kontaktTelefon == null || user.kontaktTelefon == "") {
            alert('Broj telefona ne smije biti prazan!');
            return;
        }
        else if (user.kontaktTelefon.match(/[a-z]/i)) {
            alert('Broj telefona ne smije da sadrzi slova! ');
            return;
        }
        else if (user.email == null || user.email == "") {
            alert('Email ne smije biti prazan!');
            return;
        }
        else if (!user.email.includes('@')) {
            alert('Email nije ispravno unesen!');
            return;
        }
        else if (user.pwd == null || user.pwd == "") {
            alert('Lozinka ne smije biti prazna!');
            return;
        }
        RegILogFactory.RegisterUser(user).then(function (response) {
            if (response.data == true) {
                console.log(response.data);
                $rootScope.RegisterSuccess = "Uspjesno ste se registrovali! Sada se mozete prijaviti!";
                $window.location.href = "#!/Login";
            }
            else {
                alert("Korisnicko ime vec postoji, pokusajte ponovo.");
            }
        });
    };
    $scope.LoginUser = function (user) {
        $rootScope.RegisterSuccess = "";
        if (user.username == null || user.username == "") {
            alert('Korisnicko ime ne smije biti prazno!');
            return;
        }
        else if (user.pwd == null || user.pwd == "") {
            alert('Lozinka ne smije biti prazna!');
            return;
        }
        RegILogFactory.LoginUser(user).then(function (response) {
            
            if (response.data == null) {
                alert("Ne postoji korisnik sa unijetim korisnickim imenom i lozinkom! ");
            }
            else {
                $rootScope.RegisterSuccess = "";
                console.log(response);
                document.cookie = "user=" + JSON.stringify({
                    username: response.data.KorisnickoIme,
                    role: response.data.Uloga,
                    nameSurname: response.data.Ime + " " + response.data.Prezime
                }) + ";expires=Thu, 01 Jan 2019 00:00:01 GMT;";
                sessionStorage.setItem("username", response.data.KorisnickoIme);
                sessionStorage.setItem("role", response.data.Uloga);
                sessionStorage.setItem("nameSurname", response.data.Ime + " " + response.data.Prezime);
                $rootScope.loggedin = true;
                $rootScope.user = {
                    username: sessionStorage.getItem("username"),
                    role: sessionStorage.getItem("role"),
                    nameSurname: sessionStorage.getItem("nameSurname")
                };
                $window.location.href = "#!/";
            }
        });
    }
}); 