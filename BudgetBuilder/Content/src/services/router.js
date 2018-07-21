import Vue from 'vue'
import VueRouter from 'vue-router'
import store from '@/store/store'

import Portal from '@/page/portal';
import Profile from '@/page/profile';
import Buildings from '@/page/buildings';
import Trades from '@/page/trades';

Vue.use(VueRouter)

export default new VueRouter({
    routes: [
        {
            path: '/',
            redirect: localStorage.getItem('token') ? '/Buildings' : '/Portal'
        },
        {
            path: '/Portal',
            name: 'Portal',
            component: Portal
        },
        {
            path: '/Profile',
            name: 'Profile',
            component: Portal
        },
        {
            path: '/Buildings',
            name: 'Buildings',
            component: Buildings
        },
        {
            path: '/Trades',
            name: 'Trades',
            component: Trades
        },
    ]
});
