import Vue from "vue"
const moment = require('moment');
const numeral = require('numeral');

Vue.filter('formatNumeral', function (value, format) {
    if (value.toString().length) {
        return numeral(value).format(format);
    }
});