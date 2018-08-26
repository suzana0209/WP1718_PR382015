WebAPI.controller('CommentController', function ($scope, $location, $rootScope, ProfCont, $window) {

    function init() {
        $scope.Usp = false;
        $scope.Neusp = false;

    }

    init();

    $scope.DodajKomentarKo = function (ko) {
        if (ko == null) {
            alert('Morate unijeti komentar');
            return;
        }

        ProfCont.DodajKomentar(ko, $rootScope.VoznjaZaKomentar).then(function (response) {
            console.log(response.data);

            $window.location.href = "#!/MyHome";
        });
    }

    $scope.UspesnaVoznja = function () {
        $scope.Usp = true;
        $scope.Neusp = false;
        $scope.apply;
    }
    $scope.NeuspesnaVoznja = function () {
        $scope.Neusp = true;
        $scope.Usp = false;
        $scope.apply;
    }
    $scope.DodajKomentarVozac = function (ko) {
        if (ko == null || ko.Opis == "" || ko.Opis == null) {
            alert('Morate unijeti komentar');
            return;
        }
        ProfCont.DodajKomentarVozac(ko, $rootScope.VoznjaZaKomentarVozac).then(function (response) {
            console.log(response.data);
            $window.location.href = "#!/MyHome";
        });
    }
    $scope.DodajKraj = function (drive) {
        if (drive == null) {
            return;
        }
        if (drive.Cena <= 0) {
            alert('Morate unijeti cijenu');
            return;
        }
        if (document.getElementById("lon").value == null || document.getElementById("lon").value == "") {
            alert('Polje X kooridnata mora biti popunjeno!');
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

        ProfCont.DodajKraj(drive, $rootScope.VoznjaZaKomentarVozac).then(function (response) {
            console.log(response.data);
            $window.location.href = "#!/MyHome";
        });
    }
}); 