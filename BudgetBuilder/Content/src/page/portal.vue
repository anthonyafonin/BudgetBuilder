<template>
    <div class="portal">
        Hello portal
    </div>
</template>

<script>
    import api from '@/services/api';

    module.exports = {
        components: {

        },
        data: function () {
            return {

            }
        },
        computed: {

        },
        methods: {
            login: function () { 
                var vm = this;
                var user = {}

                api().post('Account/Login', user)
                    .then(function (r) {
                        if (r.Success) {
                            vm.$store.dispatch('login', r.User);
                        } 
                    })
            }
        },
        beforeMount: function(){
            //this.login();
        },
        beforeRouteLeave: function (to, from, next) {
            next();
        }
    }
</script>