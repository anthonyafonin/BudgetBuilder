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
            profile: '',
            prompt: {
                display: '',
                message: '',
                type: '',
            }
        },
        getters: {
            getProfile: function (state) {
                return state.profile;
            },
            getPromptState: function (state) {
                return state.prompt;
            }
        },
        mutations: {
            setProfile: function (state, profile) {
                state.profile = profile
            },
            login: function (state, profile) {
                localStorage.setItem('token', profile.UserID)
                state.profile = profile;
                router.push({ name: 'Buildings' });
            },
            logout: function (state) {
                localStorage.removeItem('token');
                api().post('Account/Logout').then(function (r) {
                    state.profile = '';
                    router.push({name: 'Portal'});
                });
            },
            setPromptState: function (state, obj) {
                state.prompt = {
                    display: true,
                    message: obj.message,
                    type: obj.type,
                }

                setTimeout(function () {
                    state.prompt = {
                        display: false,
                        message: '',
                        type: '',
                    }
                }, 3000)
            }
        },
        actions: {
            setProfile: function ({ commit, state }, profile) {
                commit('setProfile', profile)
            },
            login: function ({ commit, state }, profile) {
                commit('login', profile);
                commit('setPromptState', {
                    message: 'Welcome ' + profile.FirstName + profile.LastName + '!',
                    type: 'success'
                });
            },
            logout: function ({ commit, state }, profile) {
                commit('logout')
            },
            refreshBuildings: function ({ commit, state, getters }) {
                api().post('Buildings/List', {
                    UserID: getters.getProfile.UserID
                }).then(function (r) {
                    commit('setBuildings', r.Buildings);
                })
            },
            setPromptState: function ({ commit, state }, promptState) {
                commit('setPromptState', promptState);
            }
        }
    }
)