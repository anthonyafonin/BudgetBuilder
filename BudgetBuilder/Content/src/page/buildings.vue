<template>
    <div class="buildings">
        Hello Buildings
        <div class="building" v-for="(building, index) in buildings">
            {{building.BuildingModelsID}} - {{building.Title}} - {{building.Budget}} - {{building.DateAdded}} - {{building.DateModified}}
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
                buildings: []
            }
        },
        computed: {

        },
        methods: {

            createBuilding: function (formData) {
                var vm = this;
                api().post('Buildings/Create', {
                    Title: 'Test title',
                    Budget: 15000,
                    ApplicationUserID: vm.$store.getters.getProfile.UserID
                }).then(function (r) {
                    console.log(r)
                })
            },
            updateBuilding: function () {

            },
            deleteBuilding: function () {

            },
            loadBuilding: function () {

            },
            refreshBuildings: function () {
                var vm = this;
                api().post('Buildings/List', {
                    UserID: vm.$store.getters.getProfile.UserID
                }).then(function (r) {
                    vm.buildings = r.Buildings;
                })
            }
        },
        beforeMount: function () {
            this.refreshBuildings();
        },
        beforeRouteLeave: function (to, from, next) {
            next();
        }
    }
</script>