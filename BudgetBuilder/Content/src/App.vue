<template>
    <div class="wrapper container-fluid">
        <div id="app" class="mt-5 position-relative">
            <nav class="navbar navbar-dark fixed-top bg-dark">
                <b-link class="navbar-brand" :to="{name: 'Buildings'}">Budget Builder</b-link>
               
                <div right v-if="profile">
                    <b-nav-text class="pr-4">{{profile.FirstName + ' ' + profile.LastName}}</b-nav-text>
                    <b-btn @click="logout" variant="outline-secondary">Logout</b-btn>
                </div>
               
            </nav>

            <b-container class="pt-5 pb-5">
                <!-- Main view -->
                <router-view></router-view>
            </b-container>

            <transition enter-active-class="">
                <b-alert class="fixed-bottom container" v-if="prompt.display" :show="prompt.display" :variant="prompt.type" dismissible>
                    <p>{{prompt.message}}</p>
                </b-alert>
            </transition>
        </div>
    </div>
</template>

<script>
    module.exports = {
        components: {

        },
        data: function () {
            return {

            }
        },
        methods: {
            logout: function () {
                this.$store.dispatch('logout')
            }
        },
        computed: {
            profile: function () {
                return this.$store.getters.getProfile;
            },
            prompt: function () {
                return this.$store.getters.getPromptState;
            }
        },
        beforeMount: function () {
 
        }
    }
</script>
