/**
 * Created by negut_000 on 05/08/2016.
 */
angular.module("myApp", [])
    .controller('myCtrl', function($scope, myService) {

        (function() {
            var cors_api_host = 'cors-anywhere.herokuapp.com';
            var cors_api_url = 'https://' + cors_api_host + '/';
            var slice = [].slice;
            var origin = window.location.protocol + '//' + window.location.host;
            var open = XMLHttpRequest.prototype.open;
            XMLHttpRequest.prototype.open = function() {
                var args = slice.call(arguments);
                var targetOrigin = /^https?:\/\/([^\/]+)/i.exec(args[1]);
                if (targetOrigin && targetOrigin[0].toLowerCase() !== origin &&
                    targetOrigin[1] !== cors_api_host) {
                    args[1] = cors_api_url + args[1];
                }
                return open.apply(this, args);
            };
        })();

        myService.getFoos().then(function(foos) {
            $scope.foos = foos;
            console.log("hello from ctrl", foos);
        });
    });

var app = angular.module("myApp");

    app.factory('myService', function($http) {
        var dataUrl = "https://beerbayapp.appspot.com/stock";
        return {
            getFoos: function() {
                //return the promise directly.
                return $http.get(dataUrl)
                    .then(function(result) {
                        //resolve the promise as the data
                        console.log("hello from .then()", result);
                        return result.data;
                    });
            }
        }
    });
