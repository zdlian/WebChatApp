﻿(function() {
    angular.module('wechatApp').factory('Page', function() {
        var title = "WechatApp";
        return {
            title: function() {
                return title;
            },
            setTitle: function(newTitle) {
                title = newTitle;
            }
        }
    });

    angular.module('planApp').factory('Page', function () {
        var title = "WechatApp";
        return {
            title: function () {
                return title;
            },
            setTitle: function (newTitle) {
                title = newTitle;
            }
        }
    });
})();