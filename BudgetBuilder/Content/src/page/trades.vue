<template>
    <div class="trades">
        <h1 class="mb-4">{{building.Title}}</h1>
        <div class="clearfix">
            <router-link :to="{name: 'Buildings'}">
                <b-button class="float-left" :variant="'outline-primary'">
                    <span class="fas fa-long-arrow-alt-left pr-2"></span>Back to Buildings
                </b-button>
            </router-link>
            <b-button class="float-right" @click="displayExpenseModal = true" :variant="'outline-success'">New Expense +</b-button>
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
                            <!--<b-dropdown-item-button @click="displayBuildingModal(index)">Edit</b-dropdown-item-button>
                        <b-dropdown-item-button @click="deleteBuilding(index)">Delete</b-dropdown-item-button>-->
                        </b-dropdown>

                        <div class="card-body">
                            <h5>{{trade.Category}} - {{trade.SubCategory}}</h5>
                            <p>
                                <span>Budget: {{trade.Budget | formatNumeral('$0,0[.]00')}}</span><br />
                                <!--<span v-if="getTradeCostTotal(index)">Expenses: {{getTradeCostTotal(index) | formatNumeral('$0,0[.]00')}}</span>-->
                            </p>
                            <!--<b-button class="float-right" @click="selectBuilding(building)" :size="'sm'" :variant="'outline-primary'">
                            <span class="fas fa-long-arrow-alt-right fa-lg"></span>
                        </b-button>-->
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <b-modal ref="createModal" v-model="displayExpenseModal"
                 @hide="formHandler">
            <div slot="modal-title">
                <span v-if="modalTypeCreate">New Expense</span>
                <span v-else>Edit Expense</span>
            </div>
            <form>
                <b-form-group id="b-title-group"
                              description="Enter a name for this project."
                              label="Title"
                              label-for="b-title"
                              :invalid-feedback="invalidTitleFeedback"
                              :state="buildingTitleState">
                    <b-form-input id="b-title" type="text" :state="buildingTitleState" v-model.trim="building.Title"></b-form-input>
                </b-form-group>
                <b-form-group id="b-budget-group"
                              description="Enter a budget for this project."
                              label="Budget"
                              label-for="b-budget"
                              :invalid-feedback="invalidBudgetFeedback"
                              :state="buildingBudgetState">
                    <b-form-input id="b-budget" type="number" :state="buildingBudgetState" v-model.trim="building.Budget"></b-form-input>
                </b-form-group>
            </form>
            <div slot="modal-footer" class="w-100 text-right">
                <b-btn size="sm" variant="default" @click="displayExpenseModal = false">
                    Cancel
                </b-btn>
                <b-btn size="sm" variant="success" @click="formHandler($event, true)" :disabled="!buildingBudgetState || !buildingTitleState">
                    <span v-if="modalTypeCreate">Create</span>
                    <span v-else>Update</span>
                </b-btn>
            </div>
        </b-modal>

        <!--<b-modal ref="deleteModal" v-model="deletePrompt.visible"
                 @hide="deleteformHandler">
            <div slot="modal-title">
                <span>Delete Building</span>
            </div>
            <p>Are you sure you want this delete this item?</p>
            <div slot="modal-footer" class="w-100 text-right">
                <b-btn size="sm" variant="default" @click="deletePrompt.visible = false">
                    Cancel
                </b-btn>
                <b-btn size="sm" variant="danger" @click="deletePrompt.callback">
                    <span>Delete</span>
                </b-btn>
            </div>
        </b-modal>-->
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
            building: function () {
                return this.$store.getters.getSelectedBuilding;
            },
            expenses: function () {
                return this.building.Trades;
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