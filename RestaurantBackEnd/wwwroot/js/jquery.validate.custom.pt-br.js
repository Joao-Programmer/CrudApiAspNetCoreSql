/*$.validator.methods.range = function (value, element, param) {
    var globalizedValue = value.replace(".", "");
    globalizedValue = globalizedValue.replace(",", ".");
    return this.optional(element) ||
        (globalizedValue >= param[0] &&
            globalizedValue <= param[1]);
};*/

/* /^\d+,\d{2}$/ */
/* Esse validador sobrescreve o validador padrão da lib jquery-validate. O campos já está sendo tratado como decimal e peritindo somente número e uma vírgula usando javascript. */
$.validator.methods.number = function (value, element) {
    return this.optional(element) ||
        /\d/
            .test(value);
};

 /*$.validator.methods.number = function (value, element) {
    return this.optional(element) ||
        /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/
            .test(value);
}; */