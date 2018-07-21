<template>
    <div class="trades">
        Trades

        <div class="clearfix">
            <router-link :to="{name: 'Buildings'}"><b-button class="float-left" :variant="'outline-primary'">Back to Buildings</b-button></router-link>
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

            }
        },
        computed: {

        },
        methods: {

        },
        beforeMount: function(){
        },
        beforeRouteLeave: function (to, from, next) {
            next();
        }
    }
</script>