WebAPI.factory('RegILogFactory', function ($http) {
    var factory = {};
     //smjestanje u objekat MusterijaPomocni.cs
    factory.RegisterUser = function (user) {       
        return $http.post('/api/RegILog/Register', {
            Username: user.username,
            Password: user.pwd,
            Ime: user.ime,
            Prezime: user.prezime,
            Pol: user.pol,
            Jmbg: user.jmbg,
            Telefon: user.kontaktTelefon,
            Email: user.email
        });
    }
    //smjestnje u objekat LogovaniKorisnik
    factory.LoginUser = function (user) {
        return $http.post('/api/RegILog/Login', {
            Username: user.username,
            Password: user.pwd
        });
    }
    return factory;
}); 