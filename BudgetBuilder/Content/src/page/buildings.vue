<template>
    <div class="building-view">
        <h1 class="mb-4">Buildings</h1>
        <div class="clearfix">
            <b-button class="float-right" @click="displayBuildingModal" :variant="'outline-success'">New Building +</b-button>
        </div>
        <div class="buildings pt-3">
            <div class="row">
                <div class="building mb-3 col-md-6 col-xs-12 position-relative" v-for="(building, index) in buildings" v-if="building">
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
                                <span class="text-primary" >Budget: {{building.Budget | formatNumeral('$0,0[.]00')}}</span><br />
                                <span :class="getTradeCostTotal(index) <= building.Budget ? 'text-success' : 'text-danger'">
                                    Total Expenses: {{getTradeCostTotal(index) | formatNumeral('$0,0[.]00')}}
                                </span>
                            </p>
                            <b-button class="float-right" @click="selectBuilding(building)" :size="'sm'" :variant="'outline-primary'">
                                <span class="fas fa-long-arrow-alt-right fa-lg"></span>
                            </b-button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <b-modal ref="createModal" v-model="showBuildingModal"
                 @hide="formHandler">
            <div slot="modal-title">
                <span v-if="modalTypeCreate">New Building</span>
                <span v-else>Edit Building</span>
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
                <b-btn size="sm" variant="default" @click="showBuildingModal = false">
                    Cancel
                </b-btn>
                <b-btn size="sm" variant="success" @click="formHandler($event, true)" :disabled="!buildingBudgetState || !buildingTitleState">
                    <span v-if="modalTypeCreate">Create</span>
                    <span v-else>Update</span>
                </b-btn>
            </div>
        </b-modal>

        <b-modal ref="deleteModal" v-model="deletePrompt.visible"
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
        </b-modal>
    </div>
</template>

<script>
    import api from '@/services/api';

    module.exports = {
        components: {

        },
        data: function () {
            return {
                showBuildingModal: false,
                modalTypeCreate: true,
                building: {
                    Title: '',
                    Budget: ''
                },
                selectedBuildingIndex: '',
                deletePrompt: {
                    visible: false,
                    callback: ''
                }
            }
        },
        computed: {
            buildings: function () {
                return this.$store.getters.getBuildings;
            },
            invalidTitleFeedback: function() {
                if (this.building.Title.length >= 4) {
                    return ''
                } else if (this.building.Title.length > 0) {
                    return 'Please enter at least 4 characters'
                } else {
                    return 'Please enter valid text'
                }
            },
            invalidBudgetFeedback: function () {
                if (this.building.Budget.toString().length >= 3 && this.building.Budget.toString() > 0) {
                    return ''
                } else if (this.building.Budget.toString() <= 0) {
                    return 'Please enter a valid amount'
                } else if (this.building.Budget.toString().length > 0) {
                    return 'Please enter at least 3 digits'
                } else {
                    return 'Please enter a valid amount'
                }
            },
            buildingTitleState: function () {
                var title = this.building.Title;
                return title.length >= 4;
            },
            buildingBudgetState: function () {
                var budget = this.building.Budget;
                return budget.toString().length >= 3 && this.building.Budget.toString() > 0;
            },
          
        },
        methods: {
            deleteformHandler: function (e) {
                if (e.trigger == 'backdrop') {
                    e.preventDefault();
                }
            },
            formHandler: function (e, submit) {
                var vm = this;
                if (e.trigger == 'backdrop') {
                    e.preventDefault();
                }

                if (vm.buildingTitleState && vm.buildingBudgetState && submit) {
                    if (vm.modalTypeCreate) {
                        vm.createBuilding();
                    }
                    else {
                        vm.updateBuilding(vm.building);
                    }
                }
            },
            createBuilding: function () {
                var vm = this;

                var postData = {
                    Title: vm.building.Title,
                    Budget: vm.building.Budget,
                    ApplicationUserID: vm.$store.getters.getProfile.UserID
                }

                api().post('Buildings/Create', postData).then(function (r) {
                    vm.resetTempBuilding();

                    var buildings = vm.buildings;
                    buildings.push(r.Building);
                    vm.$store.dispatch('setBuildings', buildings);
                    vm.showBuildingModal = false;
                });
            },
            updateBuilding: function (building) {
                var vm = this;

                var postData = {
                    Title: building.Title,
                    Budget: building.Budget,
                    ApplicationUserID: vm.$store.getters.getProfile.UserID,
                    BuildingModelsID: building.BuildingModelsID
                }
                api().post('Buildings/Update', postData).then(function (r) {
                    var index = vm.selectedBuildingIndex;
                    vm.resetTempBuilding();

                    var buildings = vm.buildings;
                    buildings[index] = r.Building;
                    vm.$store.dispatch('setBuildings', buildings);
                    
                    vm.showBuildingModal = false;
                });
            },
            deleteBuilding: function (index) {
                var vm = this;
                var buildings = vm.buildings;

                vm.deletePrompt.visible = true;

                vm.deletePrompt.callback = function () {
                    api().post('Buildings/Delete', { BuildingModelsID: buildings[index].BuildingModelsID }).then(function (r) {
                        buildings.splice(index, 1);
                        vm.$store.dispatch('setBuildings', buildings)
                        vm.deletePrompt.visible = false;
                    });
                };
            },
            selectBuilding: function (building) {
                this.$store.dispatch('selectBuilding', building);
                this.$router.push({ name: 'Trades' });
            },
            displayBuildingModal: function (index) {
                var vm = this
                vm.resetTempBuilding();
                vm.modalTypeCreate = !isNaN(index) ? false : true;
                
                if (!isNaN(index)) {
                    vm.selectedBuildingIndex = index;
                    vm.building = vm.normalize(vm.buildings[index]);
                }

                this.showBuildingModal = true;
            },
            resetTempBuilding: function () {
                this.building = {
                    Title: '',
                    Budget: ''
                }
                this.selectedBuildingIndex = '';
            },
            getTradeCostTotal: function (index) {
                var building = this.buildings[index];
                var sum = 0;
                if (building.Trades) {
                    for (var i = 0; i < building.Trades.length; i++) {
                        sum += buildings.Trades[i].TotalCost;
                    }
                }
                return sum;
            }
        },
        beforeMount: function () {
            if (!this.$store.getters.getBuildings.length) {
                this.$store.dispatch('refreshBuildings');
            }
        },
        beforeRouteLeave: function (to, from, next) {
            next();
        }
    }
</script>