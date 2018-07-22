<template>
    <div class="portal">
        <div class="row">

            <div class="col-md-8 mb-3">
                <b-jumbotron class="h-100" header="Budget Builder"
                             lead="Manage your budget on different building projects!">
                </b-jumbotron>
            </div>
            <div class="col-md-4 mb-3">
                <div class="card h-100 p-4">
                    <b-form @submit.prevent="login">
                        <h3 class="text-center">Sign In</h3>
                        <b-form-group id="email-group"
                                      label="Email Address"
                                      label-for="email">
                            <b-form-input id="email"
                                          type="email"
                                          v-model="loginForm.Email"
                                          required
                                          placeholder="Enter email address">
                            </b-form-input>
                        </b-form-group>
                        <b-form-group id="pw-group"
                                      label="Password"
                                      label-for="password">
                            <b-form-input id="password"
                                          type="password"
                                          v-model="loginForm.Password"
                                          required
                                          placeholder="Enter password">
                            </b-form-input>
                        </b-form-group>
                        <b-button class="float-right" type="submit" variant="primary">Login</b-button>
                    </b-form>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import api from '@/services/api';

    module.exports = {
        components: {

        },
        data: function () {
            return {
                loginForm: {
                    Email: '',
                    Password: '',
                    RememberMe: true
                }
            }
        },
        computed: {

        },
        methods: {
            login: function (e) { 
                var vm = this;
                var user = {}
                console.log(e)
                 console.log(vm.loginForm)
                api().post('Account/Login', vm.loginForm)
                    .then(function (r) {
                        if (r.Success) {
                            vm.$store.dispatch('login', r.User);
                        } 
                    })
            }
        },
        beforeMount: function () {
           
        },
        beforeRouteLeave: function (to, from, next) {
            next();
        }
    }
</script>