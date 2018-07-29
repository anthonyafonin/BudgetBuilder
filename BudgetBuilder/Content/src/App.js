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
import api from '@/services/api';

Vue.mixin(mixins);

router.push('/');

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
        var token = localStorage.getItem('token');
        var vm = this;
        if (token) {
            api().post('Account/Details', { ApplicationUserID: token }).then(function (r) {
                if (r.Success) {
                    vm.$store.dispatch('login', r.User);
                }
            });
        }
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
    if (!localStorage.getItem('token') && to.path !== '/Portal') {
        console.log('Unauthorized');
        next('/Portal');
    } else {
        next();
    }
});

router.afterEach(function (to, from) {

});