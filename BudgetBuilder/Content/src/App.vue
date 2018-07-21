<template>
    <div class="wrapper container-fluid">
        <div id="app" class="mt-5 position-relative">
            <nav class="navbar navbar-dark fixed-top bg-dark">
                <b-link class="navbar-brand" :to="{name: 'Buildings'}">Budget Builder</b-link>
                <b-nav-text right>{{profile.FirstName + ' ' + profile.LastName}}</b-nav-text>
            </nav>
            <b-container class="pt-5 pb-5">
                <b-jumbotron header="Budget Builder"
                             lead="Manage your budget on different building projects!">
                </b-jumbotron>
                <router-view></router-view>
            </b-container>
            <b-alert class="fixed-bottom container"
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
            }
        },
        methods: {
            countDownChanged: function(dismissCountDown) {
                this.dismissCountDown = dismissCountDown
            },
            showAlert: function() {
                this.dismissCountDown = this.dismissSecs
            }
        },
        computed: {
            profile: function () {
                return this.$store.getters.getProfile;
            },
            
        }
    }
</script>
