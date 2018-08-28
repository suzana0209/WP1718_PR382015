﻿WebAPI.factory('RegILogFactory', function ($http) {
    var factory = {};

    factory.GetUserStatusByUsername = function (username) {
        return $http.get('/api/Prof/GetUserStatusByUsername?username=' + username);
    }

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
    //smjestanje u objekat LogovaniKorisnik
    factory.LoginUser = function (user) {
        return $http.post('/api/RegILog/Login', {
            Username: user.username,
            Password: user.pwd
        });
    }

        //smjestanje u objekat VozacPomocna
    factory.RegisterDriver = function (user) { 
        return $http.post('/api/RegILog/RegisterDriver', {
            Username: user.username,
            Password: user.pwd,
            Ime: user.ime,
            Prezime: user.prezime,
            Pol: user.pol,
            Jmbg: user.jmbg,
            Telefon: user.kontaktTelefon,
            Email: user.email,
            Godina: user.godina,
            RegistarskaOznaka: user.registarskaOznaka,
            TipVozila: user.tipVozila
        });
    }

    return factory;
}); 