<template>
    <div class="profile">
        Hello profile
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