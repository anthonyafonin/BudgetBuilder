import 'babel-polyfill';
import Vue from 'vue';
import App from '@/App.vue';

import BootstrapVue from 'bootstrap-vue';
Vue.use(BootstrapVue);

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

import VueCookies from 'vue-cookies';
Vue.use(VueCookies);

// Vue plugins
import router from '@/services/router';
import store from '@/store/store';
import '@/services/filters';
import mixins from '@/services/mixins';
Vue.mixin(mixins);

router.push('/Portal');

// Create main vue instance
const app = new Vue({
    el: '#app',
    router: router,
    store: store,
    data: function () {
        return {

        }
    },
    render: function (r) {
        return r(App);
    },
    beforeCreate: function () {
        
    },
    methods: {
        init: function () {

        }
    }
});

router.beforeResolve(function (to, from, next) {
    next();
});

router.beforeEach(function (to, from, next) {
    next();
});

router.afterEach(function (to, from) {

});