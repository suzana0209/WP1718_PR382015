﻿WebAPI.controller('JSRegILogController', function ($scope, RegILogFactory, $window, $rootScope) {
    $scope.user = {}; //inicijalizacija na prazno

    function init() {
        console.log('Login controller initialized');
        $rootScope.Uloga = sessionStorage.getItem("role");
    };
    init();
    $scope.RegisterUser = function (user) {
        if (user.username == null || user.username == "") {
            alert('Polje Korisnicko ime ne smije biti prazno!');
            return;
        }
        else if (user.ime == null || user.ime == "") {
            alert('Polje Ime ne smije biti prazno!');
            return;
        }
        else if (user.prezime == null || user.prezime == "") {
            alert('Polje Prezime ne smije biti prazno !');
            return;
        }
        else if (user.pol == null || user.pol == "") {
            alert('Izaberite pol!');
            return;
        }
        else if (user.jmbg == null || user.jmbg == "") {
            alert('Polje Jmbg ne smije biti prazno!');
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
            alert('Polje Broj telefona ne smije biti prazno!');
            return;
        }
        else if (user.kontaktTelefon.match(/[a-z]/i)) {
            alert('Broj telefona ne smije da sadrzi slova! ');
            return;
        }
        else if (user.email == null || user.email == "") {
            alert('Polje Email ne smije biti prazno!');
            return;
        }
        else if (!user.email.includes('@')) {
            alert('Email nije ispravno unesen!');
            return;
        }
        else if (user.pwd == null || user.pwd == "") {
            alert('Polje Lozinka ne smije biti prazno!');
            return;
        }
        RegILogFactory.RegisterUser(user).then(function (response) {
            if (response.data == true) {
                console.log(response.data);
                $window.location.href = "#!/Login";
            }
            else {
                alert("Korisnicko ime vec postoji, pokusajte ponovo.");
            }
        });
    };
    $scope.LoginUser = function (user) {
        //$rootScope.RegisterSuccess = "";
        if (user.username == null || user.username == "") {
            alert('Polje Korisnicko ime ne smije biti prazno!');
            return;
        }
        else if (user.pwd == null || user.pwd == "") {
            alert('Polje Lozinka ne smije biti prazno!');
            return;
        }
        RegILogFactory.LoginUser(user).then(function (response) {
            $rootScope.ZapamtiKorisnika = response.data;
            if (response.data == null) {
                alert("Ne postoji korisnik sa unijetim korisnickim imenom i lozinkom! ");
                return;
            }
            else {

                RegILogFactory.GetUserStatusByUsername(user.username).then(function (response) {
                    if (response.data == true) {
                        alert('Ne mozete se prijaviti, BLOKIRANI STE!');
                        return;
                    }
                
                console.log(response);
                document.cookie = "user=" + JSON.stringify({
                    
                    username: $rootScope.ZapamtiKorisnika.KorisnickoIme, /*response.data.KorisnickoIme,*/
                    role: $rootScope.ZapamtiKorisnika.Uloga, /*response.data.Uloga*/
                    nameSurname: $rootScope.ZapamtiKorisnika.Ime + " " + $rootScope.ZapamtiKorisnika.Prezime/*response.data.Ime + " " + response.data.Prezime*/

                }) + ";expires=Thu, 01 Jan 2019 00:00:01 GMT;";
                sessionStorage.setItem("username", $rootScope.ZapamtiKorisnika.KorisnickoIme);
                sessionStorage.setItem("role", $rootScope.ZapamtiKorisnika.Uloga);
                sessionStorage.setItem("nameSurname", $rootScope.ZapamtiKorisnika.Ime + " " + $rootScope.ZapamtiKorisnika.Prezime);

                $rootScope.moraKomentar = false;
                $rootScope.moraKomentarKorisnik = false;

                $rootScope.loggedin = true;
                $rootScope.user = {
                    username: sessionStorage.getItem("username"),
                    role: sessionStorage.getItem("role"),
                    nameSurname: sessionStorage.getItem("nameSurname")
                };
                $window.location.href = "#!/";
                });
            }
        });
    }

    $scope.RegisterDriver = function (user) {
        if (user.username == null || user.username == "") {
            alert('Polje korisnicko ime ne smije biti prazno!');
            return;
        }
        else if (user.ime == null || user.ime == "") {
            alert('Polje ime ne smije biti prazno!');
            return;
        }
        else if (user.prezime == null || user.prezime == "") {
            alert('Polje prezime ne smije biti prazno !');
            return;
        }
        else if (user.pol == null || user.pol == "") {
            alert('Izaberite pol!');
            return;
        }
        else if (user.jmbg == null || user.jmbg == "") {
            alert('Polje jmbg ne smije biti prazno!');
            return;
        }
        else if (user.jmbg.match(/[a-z]/i)) {
            alert('Jmbg ne smije da sadrzi slova!');
            return;
        }
        else if (user.jmbg.length != 13) {
            alert('Jmbg mora da sadrzi 13 cifara');
            return;
        }
        else if (user.kontaktTelefon == null || user.kontaktTelefon == "") {
            alert('Polje broj telefona ne smije biti prazno!');
            return;
        }
        else if (user.kontaktTelefon.match(/[a-z]/i)) {
            alert('Broj telefona ne smije da sadrzi slova');
            return;
        }
        else if (user.email == null || user.email == "") {
            alert('Polje Email ne smije biti prazno!');
            return;
        }
        else if (!user.email.includes('@')) {
            alert('Email nije ispravan!');
            return;
        }
        else if (user.pwd == null || user.pwd == "") {
            alert('Polje Lozinka ne smije biti prazno!');
            return;
        }
        else if (user.godina == null || user.godina == "") {
            alert('Polje godina proizvodnje ne smije biti prazno!');
            return;
        }

        else if (user.registarskaOznaka == null || user.registarskaOznaka == "") {
            alert('Polje Registarska oznaka ne smije biti prazno!');
            return;
        }
        else if (user.tipVozila == null || user.tipVozila == "") {
            alert('Izaberite tip vozila!');
            return;
        }
        RegILogFactory.RegisterDriver(user).then(function (response) {
            if (response.data == true) {
                console.log(response.data);
                $window.location.href = "#!/MyHome";
            }
            else {
                alert("Korisnicko ime je zauzeto ili vec postoji registarska oznaka. Pokusajte ponovo.");
            }
        });
    };
}); 
