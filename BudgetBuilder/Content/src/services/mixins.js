export default {
    methods: {
        normalize: function (reactiveObj) {
            return JSON.parse(JSON.stringify(reactiveObj));
        },
        log: function () {
            for (var i = 0; i < arguments.length; i++) {
                console.log(arguments[i]);
            }
        }
    }
}