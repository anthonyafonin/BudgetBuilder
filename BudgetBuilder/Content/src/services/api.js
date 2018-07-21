import axios from 'axios';
import store from '@/store/store';
require("babel-polyfill");

export default function () {
    const api = axios.create({
        headers: {
            'Content-Type': 'application/json; charset=utf-8'
        }
    });

    // Intercept axios response and return data/error message
    api.interceptors.response.use(
        function (r) {
            console.log(r);
            return r.data;
        });

    return api;
}