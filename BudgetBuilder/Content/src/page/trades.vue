<template>
    <div class="trades">
        <h1 class="mb-4">Expenses</h1>
        <div class="clearfix">
            <router-link :to="{name: 'Buildings'}">
                <b-button class="float-left" :variant="'outline-primary'">
                    <span class="fas fa-long-arrow-alt-left pr-2"></span>Back to Buildings
                </b-button>
            </router-link>
            <b-button class="float-right" @click="displayExpenseModal" :variant="'outline-success'">New Expense +</b-button>
        </div>
        <div class="trades pt-3">
            <div class="row">
                <div class="trade mb-3 col-md-6 col-xs-12 position-relative" v-for="(trade, index) in expenses" v-if="expenses">
                    <div class="card ">
                        <b-dropdown variant="link" class="position-absolute p-a" style="right: 0px;" size="lg" no-caret right>
                            <template slot="button-content">
                                <span class="fas fa-ellipsis-v"></span>
                                <span class="sr-only">More</span>
                            </template>
                            <b-dropdown-item-button @click="displayBuildingModal(index)">Edit</b-dropdown-item-button>
                            <b-dropdown-item-button @click="deleteBuilding(index)">Delete</b-dropdown-item-button>
                        </b-dropdown>

                        <div class="card-body">
                            <h5>{{building.Title}}</h5>
                            <p>
                                <span>Budget: {{building.Budget | formatNumeral('$0,0.00')}}</span><br />
                                <span v-if="getTradeCostTotal(index)">Expenses: {{getTradeCostTotal(index) | formatNumeral('$0,0.00')}}</span>
                            </p>
                            <b-button class="float-right" @click="selectBuilding(building)" :size="'sm'" :variant="'outline-primary'">
                                <span class="fas fa-long-arrow-alt-right fa-lg"></span>
                            </b-button>
                        </div>
                    </div>
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
                trades: [],
                displayExpenseModal: false
            }
        },
        computed: {
            expenses: function () {
                return this.$store.getters.getSelectedBuilding.Trade;
            }
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