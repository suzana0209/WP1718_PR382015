WebAPI.controller('ProfileController', function ($scope, ProfCont, $routeParams, $window) {
    function init() {
        console.log('Profile controller initialized');
        ProfCont.getUserByUsername($routeParams.username).then(function (response) {
            console.log(response.data);
            $scope.userProfile = response.data;
        });
    }
    init();
    $scope.AddDriveCustomer = function (drive) {
        if (drive.XCoord == null || drive.XCoord == "") {
            alert('Polje X koordinata mora biti popunjena!');
            return;
        }
        else if (drive.YCoord == null || drive.YCoord == "") {
            alert('Polje Y koordinata mora biti popunjena!');
            return;
        }
        else if (drive.Street == null || drive.Street == "") {
            alert('Polje Ulica koordinata mora biti popunjena!');
            return;
        }
        else if (drive.Number == null || drive.Number == "") {
            alert('Polje Broj mora biti popunjena!');
            return;
        } else if (drive.Town == null || drive.Town == "") {
            alert('Polje Grad mora biti popunjen!');
            return;
        } else if (drive.PostalCode == null || drive.PostalCode == "") {
            alert('Polje Postanski broj mora biti popunjen!');
            return;
        }
        ProfCont.AddDriveCustomer(drive).then(function (response) {
            if (response.data == true) {
                console.log(response.data);
                $scope.newDrive = response.data;
                //$rootScope.RegisterSuccess = "Registration was successful. You can login now.";
                $window.location.href = "#!/MyHome";
            }
            else {
                alert("Voznja ne postoji.");
            }
        });
    }
    $scope.AddDriveDispecer = function (drive) {
        if (drive.XCoord == null || drive.XCoord == "") {
            alert('Polje X koordinata mora biti popunjena!');
            return;
        }
        else if (drive.YCoord == null || drive.YCoord == "") {
            alert('Polje Y koordinata mora biti popunjena!');
            return;
        }
        else if (drive.Street == null || drive.Street == "") {
            alert('Polje Ulica mora biti popunjena!');
            return;
        }
        else if (drive.Number == null || drive.Number == "") {
            alert('Polje Broj mora biti popunjen!');
            return;
        } else if (drive.Town == null || drive.Town == "") {
            alert('Polje Grad mora biti popunjen!');
            return;
        } else if (drive.PostalCode == null || drive.PostalCode == "") {
            alert('Polje Postanski broj mora biti popunjen!');
            return;
        }
        ProfCont.AddDriveDispecer(drive).then(function (response) {
            if (response.data == true) {
                console.log(response.data);
                $scope.newDrive = response.data;
                //$rootScope.RegisterSuccess = "Registration was successful. You can login now.";
                $window.location.href = "#!/MyHome";
            }
            else {
                alert("Voznja ne postoji.");
            }
        });
    }

    $scope.EditUser = function (user) {
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
            alert('Jmbg ne smije da sadrzi slova');
            return;
        }
        else if (user.jmbg.length != 13) {
            alert('Jmbg mora da sadrzi cifre');
            return;
        }
        else if (user.kontaktTelefon == null || user.kontaktTelefon == "") {
            alert('Broj telefona ne smije biti prazno!');
            return;
        }
        else if (user.kontaktTelefon.match(/[a-z]/i)) {
            alert('Broj telefona ne smije da sadrzi slova');
            return;
        }
        else if (user.email == null || user.email == "") {
            alert('Email ne smije biti prazno!');
            return;
        }
        else if (!user.email.includes('@')) {
            alert('Email nije ispravno!');
            return;
        }
        else if (user.pwd == null || user.pwd == "") {
            alert('Lozinka ne smije prazno!');
            return;
        }
        ProfCont.EditUser(user).then(function (response) {
            if (response.data == true) {
                console.log(response.data);
                $rootScope.RegisterSuccess = "Izmjene su uspjesno sazuvane. Sada se mozete prijaviti.";
                $window.location.href = "#!/MyHome";
            }
            else {
                alert("Korisnicko ime vec postoji, pokusajte ponovo.");
            }
        });
    };

});