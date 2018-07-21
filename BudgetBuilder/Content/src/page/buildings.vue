<template>
    <div class="buildings">
        Buildings
        <div class="building card mb-2" v-for="(building, index) in buildings"
             @click="selectBuilding(building)">
            <div class="card-body">
                {{building.BuildingModelsID}} - {{building.Title}} - {{building.Budget}} - {{building.DateAdded}} - {{building.DateModified}}

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
                
            }
        },
        computed: {
            buildings: function () {
                return this.$store.getters.getBuildings;
            }
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
            selectBuilding: function (building) {
                this.$store.dispatch('selectBuilding', building);
                this.$router.push({ name: 'Trades' });
            }

        },
        beforeMount: function () {
            this.$store.dispatch('refreshBuildings');
        },
        beforeRouteLeave: function (to, from, next) {
            next();
        }
    }
</script>