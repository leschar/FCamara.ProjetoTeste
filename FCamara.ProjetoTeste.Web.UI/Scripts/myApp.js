var myApp = angular.module('myApp', ['ngRoute']);
//configurando a rota
myApp.config([
    '$routeProvider', function($routeProvider) {
        $routeProvider
            .when('/',
            {
                redirectTo: '/home'
            })
            .when('/home',
            {
                templateUrl: '/template/home.html',
                controller: 'homeController'
            })
            .when('/autenticacao',
            {
                templateUrl: '/template/autenticacao.html',
                controller: 'autenticacaoController'
            })
            .when('/autorizacao',
            {
                templateUrl: '/template/autorizacao.html',
                controller: 'autorizacaoController'
            })
            .when('/login',
            {
                templateUrl: '/template/login.html',
                controller: 'loginController'
            })
            .when('/naoautorizado',
            {
                templateUrl: '/template/naoautorizado.html',
                controller: 'naoautorizadoController'
            });
    }
]);
//variaveis globais
myApp.constant('caminhoBase', 'http://localhost:41407');
//controllers
myApp.controller('homeController',
[
    '$scope', 'dataService', function($scope, dataService) {
        $scope.data = "";
        dataService.GetAnonymousData()
            .then(function(data) {
                $scope.data = data;
            });
    }
]);
myApp.controller('autenticacaoController',
[
    '$scope', 'dataService', function($scope, dataService) {
        $scope.data = "";
        dataService.GetAuthenticateData()
            .then(function(data) {
                $scope.data = data;
            });
    }
]);
myApp.controller('autorizacaoController', ['$scope', 'dataService', function ($scope, dataService) {
    $scope.data = "";
    $scope.result = "";
    dataService.GetAuthorizeData().then(function (data) {
        $scope.data = data;
        dataService.GetProdutosData().then(function (result) {
            $scope.result = result;
        });
    });
}
]);
myApp.controller('loginController',
[
    '$scope', 'contaService', '$location', function($scope, contaService, $location) {
        $scope.conta = {
            username: '',
            password: ''
        }
        $scope.message = "";
        $scope.login = function() {
            contaService.login($scope.conta)
                .then(function(data) {
                        $location.path('/home');
                    },
                    function(error) {
                        $scope.message = error.error_description;
                    });
        }
    }
]);
myApp.controller('naoautorizadoController',
[
    '$scope', function($scope) {
        $scope.data = "Desculpe, você não esta autorizado a acessar esta pagina!";
    }
]);
//services
myApp.factory('dataService',
[
    '$http', 'caminhoBase', function($http, caminhoBase) {
        var fac = {};
        fac.GetAnonymousData = function() {
            return $http.get(caminhoBase + '/api/login/home')
                .then(function(response) {
                    return response.data;
                });
        }

        fac.GetAuthenticateData = function() {
            return $http.get(caminhoBase + '/api/login/autenticacao')
                .then(function(response) {
                    return response.data;
                });
        }

        fac.GetAuthorizeData = function() {
            return $http.get(caminhoBase + '/api/login/autorizacao')
                .then(function(response) {
                    return response.data;
                });
        }

        fac.GetProdutosData = function () {
            return $http.get('http://localhost:47641' + '/api/produtos/lista').then(function (response) {
                return response.data;
            });
        }
        return fac;
    }
]);

myApp.factory('userService',
    function() {
        var fac = {};
        fac.CurrentUser = null;
        fac.SetCurrentUser = function(user) {
            fac.CurrentUser = user;
            sessionStorage.user = angular.toJson(user);
        }
        fac.GetCurrentUser = function() {
            fac.CurrentUser = angular.fromJson(sessionStorage.user);
            return fac.CurrentUser;
        }
        return fac;
    });
myApp.factory('contaService',
[
    '$http', '$q', 'caminhoBase', 'userService', function($http, $q, caminhoBase, userService) {
        var fac = {};
        fac.login = function(user) {
            var obj = { 'username': user.username, 'password': user.password, 'grant_type': 'password' };
            Object.toparams = function objectsToParams(obj) {
                var p = [];
                for (var key in obj) {
                    if (obj.hasOwnProperty(key)) {
                        p.push(key + '=' + encodeURIComponent(obj[key]));
                    }
                }
                return p.join('&');
            }

            var defer = $q.defer();
            $http({
                    method: 'post',
                    url: caminhoBase + "/token",
                    data: Object.toparams(obj),
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                })
                .then(function(response) {
                        userService.SetCurrentUser(response.data);
                        defer.resolve(response.data);
                    },
                    function(error) {
                        defer.reject(error.data);
                    });
            return defer.promise;
        }
        fac.logout = function() {
            userService.CurrentUser = null;
            userService.SetCurrentUser(userService.CurrentUser);
        }
        return fac;
    }
]);
//http interceptor
myApp.config(['$httpProvider', function ($httpProvider) {
    var interceptor = function (userService, $q, $location) {
        return {
            request: function (config) {
                var currentUser = userService.GetCurrentUser();
                if (currentUser != null) {
                    config.headers['Authorization'] = 'Bearer ' + currentUser.access_token;
                }
                return config;
            },
            responseError: function (rejection) {
                if (rejection.status === 401) {
                    $location.path('/login');
                    return $q.reject(rejection);
                }
                if (rejection.status === 403) {
                    $location.path('/naoautorizado');
                    return $q.reject(rejection);
                }
                return $q.reject(rejection);
            }

        }
    }
    var params = ['userService', '$q', '$location'];
    interceptor.$inject = params;
    $httpProvider.interceptors.push(interceptor);
}]);