WebAPI.controller('ProfileController', function ($scope, ProfCont, $window, $rootScope) {

    if (!$rootScope.loggedin) {
        $window.location.href = '#!/Login';
    }

    function init() {
        console.log('Profile controller initialized');
        $scope.PrikaziKorisnickoIme = false;
        $scope.PrikaziIme = false;
        $scope.PrikaziPrezime = false;
        $scope.najbliziVozaci = false;
        $scope.Prazna = false;

        ProfCont.getUserByUsername(sessionStorage.getItem("username")).then(function (response) {
            console.log(response.data);

            $scope.userProfile = response.data;
            if ($scope.userProfile.Pol == "Musko") {
                $scope.P = 'Musko';
            }
            else {
                $scope.P = 'Zensko';
            }
            
           // $scope.userProfile = response.data;
        });
    }
    init();


    $scope.AddDriveCustomer = function (drive) {
        if (document.getElementById("lon").value == null || document.getElementById("lon").value == "") {
            alert('Polje X koordinata mora biti popunjeno!');
            return;
        }
        else if (document.getElementById("lat").value == null || document.getElementById("lat").value == "") {
            alert('Polje Y koordinata mora biti popunjeno!');
            return;
        }
        else if (document.getElementById("address").innerHTML == null || document.getElementById("address").innerHTML == "") {
            alert('Polje Ulica  mora biti popunjeno!');
            return;
        }
        //else if (drive.Number == null || drive.Number == "") {
        //    alert('Polje Broj mora biti popunjeno!');
        //    return;
        //} else if (drive.Town == null || drive.Town == "") {
        //    alert('Polje Grad mora biti popunjeno!');
        //    return;
        //} else if (drive.PostalCode == null || drive.PostalCode == "") {
        //    alert('Polje Postanski broj mora biti popunjeno!');
        //    return;
        //}

        //drive.XCoord = document.getElementById("xCoord").value;
        //drive.YCoord = document.getElementById("yCoord").value;

        drive.XCoord = document.getElementById("lon").value;
        drive.YCoord = document.getElementById("lat").value;
        drive.Street = document.getElementById("address").innerHTML;

        ProfCont.AddDriveCustomer(drive).then(function (response) {
            if (response.data == true) {
                console.log(response.data);
                $scope.newDrive = response.data;
                $window.location.href = "#!/MyHome";
            }
            else {
                alert("Voznja ne postoji.");
            }
        });
    };


    $scope.AddDriveDispecer = function (drive) {
        //$scope.RegisterSuccess = "";
        if (document.getElementById("lon").value == null || document.getElementById("lon").value == "") {
            alert('Polje X koordinata mora biti popunjeno!');
            return;
        }
        else if (document.getElementById("lat").value == null || document.getElementById("lat").value == "") {
            alert('Polje Y koordinata mora biti popunjeno!');
            return;
        }
        else if (document.getElementById("address").innerHTML == null || document.getElementById("address").innerHTML == "") {
            alert('Polje Ulica mora biti popunjeno!');
            return;
        }
        
        drive.XCoord = document.getElementById("lon").value;
        drive.YCoord = document.getElementById("lat").value;
        drive.Street = document.getElementById("address").innerHTML;
        $scope.VoznjaZaDodavanjeDispecer = drive;

        ProfCont.AddDriveDispecer(drive).then(function (response) {
            
            console.log(response.data);
            if (response.data.length == 0) {
                $scope.Prazna = true;
            }

            $scope.ListaNajblizih = response.data;
            $scope.najbliziVozaci = true;
            $scope.apply;
        });

        //ProfCont.AddDriveDispecer(drive).then(function (response) {
        //    //$scope.RegisterSuccess = "";
        //    if (response.data == true) {
        //        console.log(response.data);
        //        $scope.newDrive = response.data;
        //        //$rootScope.RegisterSuccess = "Registration was successful. You can login now.";
        //        $window.location.href = "#!/MyHome";
        //    }
        //    else {
        //        alert("Voznja ne postoji.");
        //    }
        //});
    };

    $scope.DodajVoznjuDisp = function (noviModel) {
        if (noviModel == null) {
            alert('Morate selektovati vozaca');
            return;
        }
        ProfCont.DodajVoznjuDisp(noviModel, $scope.VoznjaZaDodavanjeDispecer).then(function (response) {
            console.log(response.data);
            $window.location.href = "#!/MyHome";
        });
    }


    $scope.EditUser = function (user) {
        if (user.username == null || user.username == "") {
            user.username = $scope.userProfile.KorisnickoIme;
            
        }
         if (user.ime == null || user.ime == "") {
            user.ime = $scope.userProfile.Ime;
        }
         if (user.prezime == null || user.prezime == "") {
            user.prezime = $scope.userProfile.Prezime;
        }
         if (user.pol == null || user.pol == "") {
            user.pol = $scope.userProfile.Pol;
        }
         if (user.jmbg == null || user.jmbg == "") {
            user.jmbg = $scope.userProfile.Jmbg;
        }

         if (user.kontaktTelefon == null || user.kontaktTelefon == "") {
            user.kontaktTelefon = $scope.userProfile.KontaktTelefon;
        }
        
         if (user.email == null || user.email == "") {
            user.email = $scope.userProfile.Email;
        }

         if (user.pwd == null || user.pwd == "") {
            user.pwd = $scope.userProfile.Lozinka;
        }

        ProfCont.EditUser(user).then(function (response) {
            if (response.data != 3) {
                console.log(response.data);
                document.cookie = "user=" + JSON.stringify({
                    username: user.username, //response.data.UserName,
                    role: response.data, //response.data.Role,
                    nameSurname: user.ime + " " + user.prezime //response.data.Name + " " + response.data.Surname
                }) + ";expires=Thu, 01 Jan 2019 00:00:01 GMT;";
                sessionStorage.setItem("username", user.username);
                sessionStorage.setItem("role", response.data);
                sessionStorage.setItem("nameSurname", user.ime + " " + user.prezime);
                //$rootScope.loggedin = true;
                $rootScope.user.username = sessionStorage.getItem("username");
                $rootScope.user.nameSurname = sessionStorage.getItem("nameSurname");
                
                $window.location.href = "#!/MyHome";
            }
            else {
                alert("Korisnicko ime vec postoji !");
            }
        });
    };

    $scope.Izmeni = function (drive) {
        if (drive == null) {
            drive = {};
            drive.tipAuta = $rootScope.VoznjaZaIzmenu.TipAuta;
        }

        if (document.getElementById("lon").value == null || document.getElementById("lon").value == "") {
            drive.XCoord = $rootScope.VoznjaZaIzmenu.LokacijaDolaskaTaksija.X;
        }
        else {
            drive.XCoord = document.getElementById("lon").value;
        }
        if (document.getElementById("lat").value == null || document.getElementById("lat").value == "") {
            drive.YCoord = $rootScope.VoznjaZaIzmenu.LokacijaDolaskaTaksija.Y;
        } else {
            drive.YCoord = document.getElementById("lat").value;
        }
        if (document.getElementById("address").innerText == null || document.getElementById("address").innerText == "") {
            drive.Street = $rootScope.VoznjaZaIzmenu.LokacijaDolaskaTaksija.Adresa.FormatAdrese;
        } else {
            drive.Street = document.getElementById("address").innerText;
        }
        if (drive.tipAuta == null || drive.tipAuta == "") {
            drive.tipAuta = $rootScope.VoznjaZaIzmenu.ZeljeniAutomobil;
        }
        drive.Datum = $rootScope.VoznjaZaIzmenu.DatumPorudzbine;
        ProfCont.Izmeni(drive).then(function (response) {
            if (response.data == true) {
                console.log(response.data);
                //$scope.newDrive = response.data;
                //$rootScope.RegisterSuccess = "Registration was successful. You can login now.";
                $window.location.href = "#!/MyHome";
            }
            else {
                alert("Voznja ne postoji !");
            }
        });
    }
    $scope.IzmeniV = function () {
        drive = {};
        if (document.getElementById("lon").value == null || document.getElementById("lon").value == "" ||
            document.getElementById("lat").value == null || document.getElementById("lat").value == "" ||
            document.getElementById("address").innerText == null ||
            document.getElementById("address").innerText == "") {
            alert('Morate odabrati novu lokaciju');
            return;
        }
        else {
            drive.XCoord = document.getElementById("lon").value;
            drive.YCoord = document.getElementById("lat").value;
            drive.Street = document.getElementById("address").innerText;
        }
        ProfCont.IzmeniV(drive).then(function (response) {
            window.location.href = "#!/MyHome";
        });
    }

    $scope.ObradiVoznjuDisp = function (noviModel) {
        if (noviModel == null) {
            alert('Morate odabrati vozaca');
            return;
        }
        ProfCont.ObradiVoznjuDisp(noviModel, $rootScope.VoznjaZaObradu).then(function (response) {
            window.location.href = "#!/MyHome";
        });
    }

});