﻿WebAPI.controller('CommentController', function ($scope, $location, $rootScope, ProfCont, $window) {

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
            alert('Morate unijeti cenu');
            return;
        }
        if (drive.XCoord == "" || drive.YCoord == "" || drive.Street == "" || drive.Number == "" || drive.Town == "" || drive.PostalCode == "") {
            alert('Sva polja moraju biti popunjena');
            return;
        }

        ProfCont.DodajKraj(drive, $rootScope.VoznjaZaKomentarVozac).then(function (response) {
            console.log(response.data);
            $window.location.href = "#!/MyHome";
        });
    }
}); 