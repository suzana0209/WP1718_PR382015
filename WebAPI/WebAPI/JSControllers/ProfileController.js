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
            alert('X koordinata mora biti popunjena!');
            return;
        }
        else if (drive.YCoord == null || drive.YCoord == "") {
            alert('Y koordinata mora biti popunjena!');
            return;
        }
        else if (drive.Street == null || drive.Street == "") {
            alert('Ulica koordinata mora biti popunjena!');
            return;
        }
        else if (drive.Number == null || drive.Number == "") {
            alert('Broj mora biti popunjena!');
            return;
        } else if (drive.Town == null || drive.Town == "") {
            alert('Grad mora biti popunjen!');
            return;
        } else if (drive.PostalCode == null || drive.PostalCode == "") {
            alert('Postanski broj mora biti popunjen!');
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
            alert('X koordinata mora biti popunjena!');
            return;
        }
        else if (drive.YCoord == null || drive.YCoord == "") {
            alert('Y koordinata mora biti popunjena!');
            return;
        }
        else if (drive.Street == null || drive.Street == "") {
            alert('Ulica mora biti popunjena!');
            return;
        }
        else if (drive.Number == null || drive.Number == "") {
            alert('Broj mora biti popunjen!');
            return;
        } else if (drive.Town == null || drive.Town == "") {
            alert('Grad mora biti popunjen!');
            return;
        } else if (drive.PostalCode == null || drive.PostalCode == "") {
            alert('Postanski broj mora biti popunjen!');
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
});