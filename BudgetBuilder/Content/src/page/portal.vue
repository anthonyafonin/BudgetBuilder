<template>
    <div class="portal">
        <div class="row">

            <div class="mb-3 d-none d-sm-block" :class="showRegisterForm ? 'col-md-6' : 'col-md-8'">
                <b-jumbotron  header="Budget Builder"
                             lead="Manage your budget on different building projects!">
                </b-jumbotron>
            </div>
            <div class="mb-3" :class="showRegisterForm ? 'col-md-6' : 'col-md-4'">
                <div class="card h-100 p-4" v-if="showRegisterForm">
                    <b-form @submit.prevent="register">
                        <h3 class="text-center">Register</h3>
                        <b-form-group id="email1-group"
                                      label="Email Address"
                                      label-for="email">
                            <b-form-input id="email1"
                                          type="email"
                                          v-model="registerForm.Email"
                                          required
                                          placeholder="Enter email address">
                            </b-form-input>
                        </b-form-group>
                        <b-form-group id="fname-group"
                                      label="First Name"
                                      label-for="fname">
                            <b-form-input id="fname"
                                          type="text"
                                          v-model="registerForm.FirstName"
                                          required
                                          placeholder="Enter first name">
                            </b-form-input>
                        </b-form-group>
                        <b-form-group id="lname-group"
                                      label="Last Name"
                                      label-for="lname">
                            <b-form-input id="lname"
                                          type="text"
                                          v-model="registerForm.LastName"
                                          required
                                          placeholder="Enter last name">
                            </b-form-input>
                        </b-form-group>
                        <b-form-group id="pw1-group"
                                      label="Password"
                                      label-for="password">
                            <b-form-input id="password1"
                                          type="password"
                                          v-model="registerForm.Password"
                                          required
                                          placeholder="Enter password">
                            </b-form-input>
                        </b-form-group>
                        <b-form-group id="confirm-group"
                                      label="Confirm Password"
                                      label-for="confirmPass">
                            <b-form-input id="confirmPass"
                                          type="password"
                                          v-model="registerForm.ConfirmPassword"
                                          required
                                          placeholder="Confirm password">
                            </b-form-input>
                        </b-form-group>
                        <b-button class="float-right" type="submit" variant="primary">Create Account</b-button>
                    </b-form>
                </div>
                <div class="card h-100 p-4" v-else>
                    <b-form @submit.prevent="login">
                        <h3 class="text-center">Sign In</h3>
                        <b-form-group id="email2-group"
                                        label="Email Address"
                                        label-for="email2">
                            <b-form-input id="email2"
                                            type="email"
                                            v-model="loginForm.Email"
                                            required
                                            placeholder="Enter email address">
                            </b-form-input>
                        </b-form-group>
                        <b-form-group id="pw2-group"
                                        label="Password"
                                        label-for="password2">
                            <b-form-input id="password2"
                                            type="password"
                                            v-model="loginForm.Password"
                                            required
                                            placeholder="Enter password">
                            </b-form-input>
                        </b-form-group>
                        <b-button class="float-right" type="submit" variant="primary">Login</b-button>
                    </b-form>
                </div>
                 <div class="text-center text-primary" @click.prevent="showRegisterForm = !showRegisterForm"><a href="">{{showRegisterForm ? 'Already have an Account?' : 'Create New Account'}}</a></div>
               
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
                showRegisterForm: false,
                loginForm: {
                    RememberMe: true
                },
                registerForm: {}
            }
        },
        computed: {

        },
        methods: {
            login: function (e) { 
                var vm = this;
                api().post('Account/Login', vm.loginForm).then(function (r) {
                    if (r.Success) {
                        vm.$store.dispatch('login', r.User);
                    }
                    else {
                        vm.displayError('Invalid Email Address or Password')
                    }
                })
            },
            register: function () {
                var vm = this;

                if (vm.registerForm.Password.length < 7) {
                    vm.displayError('The password must be at least 6 characters long');
                    return;
                }
                if (vm.registerForm.Password != vm.registerForm.ConfirmPassword) {
                    vm.displayError('The password fields must match');
                    return;
                }

                api().post('Account/Register', vm.registerForm).then(function (r) {
                    if (r.Success) {
                        vm.$store.dispatch('login', r.User);
                    }
                    else {
                        var msg = '';
                        for (var i = 0; i < r.Message.length; i++) {
                            msg += r.Message[i] + '\n';
                        }
                        vm.displayError(msg);
                    }
                });
            },
            displayError: function (msg) {
                this.$store.dispatch('setPromptState', {message: msg, type: 'danger'})
            }
        },
        beforeMount: function () {
           
        },
        beforeRouteLeave: function (to, from, next) {
            next();
        }
    }
</script>