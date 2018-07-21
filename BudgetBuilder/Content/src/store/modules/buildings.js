export default {
    state: {
        Buildings: [],
        SelectedBuilding: ''
    },
    getters: {
        getBuildings: function (state) {
            return state.Buildings;
        },
        getSelectedBuilding: function (state) {
            return state.SelectedBuilding;
        }
    },
    mutations: {
        setBuildings: function (state, buildings) {
            state.Buildings = buildings;
        },
        selectBuilding: function (state, building) {
            state.SelectedBuilding = building;
        }
    },
    actions: {
        setBuildings: function ({ commit, state }, buildings){
            commit('setBuildings', buildings);
        },
        selectBuilding: function ({ commit, state }, building) {
            commit('selectBuilding', building);
        }
    }
}