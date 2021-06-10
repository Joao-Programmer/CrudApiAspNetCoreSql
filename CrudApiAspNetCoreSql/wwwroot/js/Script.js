'use strict';

$(document).ready(function () {
    $(".dataMask").inputmask('99/99/9999');
    $(".cpfMask").inputmask('999.999.999-99');
    $(".cnpjMask").inputmask('99.999.999/9999-99');$(".Numeric_12_2").inputmask("mask", { "mask": "999.999.999,99" }, { reverse: true });
});

function currencyFormat(v) {
    var length = v.length;

    if (_signalFirst(v)) {
        console.log('_signalFirst');

        v = v.replace(/[,.]/, '');
        if (length === 2) v = '0,' + v + '0';
        else v = '0,' + v;
    } else if (_onlyNumber(v)) {
        console.log('_onlyNumber');

        v = v + ',00';
    } else if (_rightPosition(v)) {
        console.log('_rightPosition');

        v = v.replace(/[,.]/, '');
        v = v.replace(/(\d)(\d{2})$/, '$1,$2');
    } else if (_signalOne(v)) {
        console.log('_signalOne');

        v = v.replace(/[,.]/, '');
        v = v + '0';
        v = v.replace(/(\d)(\d{2})$/, "$1,$2");
    } else {
        v = v.replace(/[,.]/, '');
        v = v.replace(/(\d{1,3})$/g, "$1,00");
        v = v.replace(/(\d{1,3})(\d{3},00)$/, "$1.$2");
    }
    return v;
}

function _signalFirst(v) {
    return /^(,|\.)/.test(v);
}

function _signalOne(v) {
    return /,|\.+\d$/.test(v);
}

function _onlyNumber(v) {
    return /^\d+$/.test(v);
}

function _rightPosition(v) {
    return /(,|\.)\d{2}$/.test(v);
}

function onlyDecimalNumber(obj, e) {
    var tecla = (window.event) ? e.keyCode : e.which;
    var texto = obj.value
    var indexvir = texto.indexOf(",")
    var indexpon = texto.indexOf(".")

    if (tecla == 8 || tecla == 0)
        return true;
    if (tecla != 44 && tecla != 46 && tecla < 48 || tecla > 57)
        return false;
    if (tecla == 44) { if (indexvir !== -1 || indexpon !== -1) { return false } }
    if (tecla == 46) { if (indexvir !== -1 || indexpon !== -1) { return false } }
}

function selectFile() {
    if (document.getElementById('inputFile').value) {
        document.getElementById('msgErrorInput').innerHTML = document.getElementById('inputFile').value.match(
            /[\/\\]([\w\d\s\.\-\(\)]+)$/
        )[1];
    } else {
        document.getElementById('msgErrorInput').innerHTML = "No image chosen, yet.";
    }
}

(function (global) {

    var dc = {};

    // Convenience function for inserting innerHTML for 'select'
    var insertHtml = function (selector, html) {
        var targetElem = document.querySelector(selector);
        targetElem.innerHTML = html;
    };

    dc.loadTitle = function (txtTitle, selector) {
        var html = "<h1 class='title'>" + txtTitle + "</h1>"
        insertHtml(selector, html);
    }

    dc.cleanTag = function (selector) {
        var html = ""
        insertHtml(selector, html);
    }

    global.$dc = dc;

})(window);