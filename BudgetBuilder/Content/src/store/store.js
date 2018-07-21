import Vue from 'vue';
import VueCookies from 'vue-cookies';
import Vuex from 'vuex';
import router from '@/services/router'
import api from '@/services/api';
require("babel-polyfill");

Vue.use(Vuex, VueCookies);
var vue = new Vue();

import buildings from '@/store/modules/buildings'

export default new Vuex.Store(
    {
        modules: {
            buildings: buildings
        },
        state: {
            profile: ''
        },
        getters: {
            getProfile: function (state) {
                return state.profile;
            }
        },
        mutations: {
            setProfile: function (state, profile) {
                state.profile = profile
            },
            login: function (state, profile) {
                localStorage.setItem('token', profile.UserID)
                state.profile = profile;
                router.push('/');
            },
            logout: function (state) {
                api().post('Account/Logout').then(function (r) {
                    console.log('Logout -', r)
                    localStorage.removeItem('token');
                    state.profile = '';
                    router.push('/');
                });
            }
        },
        actions: {
            setProfile: function ({ commit, state }, profile) {
                commit('setProfile', profile)
            },
            login: function ({ commit, state }, profile) {
                commit('login', profile)
            },
            logout: function ({ commit, state }, profile) {
                commit('login')
            },
            refreshBuildings: function ({ commit, state, getters }) {
                api().post('Buildings/List', {
                    UserID: getters.getProfile.UserID
                }).then(function (r) {
                    commit('setBuildings', r.Buildings);
                })
            }
        }
    }
)