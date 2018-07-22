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

            <b-alert v-if="profile" class="fixed-bottom container"
                     variant="success"
                     :show="dismissCountDown"
                     dismissible
                     @dismissed="dismissCountDown=0"
                     @dismiss-count-down="countDownChanged">
                <p>Welcome {{profile.FirstName}}!</p>
                <b-progress variant="success"
                            :max="dismissSecs"
                            :value="dismissCountDown"
                            height="4px">
                </b-progress>
            </b-alert>
        </div>
    </div>
</template>

<script>
    

    module.exports = {
        components: {

        },
        data: function () {
            return {
                dismissSecs: 3,
                dismissCountDown: 3,
                router: this.$router
            }
        },
        methods: {
            countDownChanged: function(dismissCountDown) {
                this.dismissCountDown = dismissCountDown
            },
            showAlert: function() {
                this.dismissCountDown = this.dismissSecs
            },
            logout: function () {
                this.$store.dispatch('logout')
            }
        },
        computed: {
            profile: function () {
                return this.$store.getters.getProfile;
            },
            
        },
        beforeMount: function () {
 
        }
    }
</script>
