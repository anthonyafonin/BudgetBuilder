const webpack = require('webpack');
const path = require('path');
const glob = require('glob')
const { VueLoaderPlugin } = require('vue-loader');

module.exports = function (env, opt) {

    return {
        watch: true,
        entry: {
            build: [
                './content/src/App.js'
            ]
        },
        output: {
            filename: '[name].js',
            path: path.resolve(__dirname, './Content/dist/js/'),
            publicPath: './Content/dist/js/'
        },
        module: {
            rules: [
                {
                    test: /\.vue$/,
                    exclude: /node_modules/,
                    loader: 'vue-loader'
                },
                {
                    test: /\.js$/,
                    exclude: /node_modules/,
                    use: [{
                        loader: "babel-loader",
                        options: {
                            presets: ['es2015']
                        }
                    }]
                },
                {
                    test: /\.css$/,
                    exclude: '/node_modules/',
                    use: ['style-loader', 'css-loader']
                },
                {
                    test: /\.less$/,
                    exclude: '/node_modules/',
                    use: [
                        { loader: "style-loader" },
                        { loader: "css-loader" },
                        { loader: "less-loader" }
                    ]
                }
            ]
        },
        plugins: [
            new VueLoaderPlugin(),
            new webpack.DefinePlugin({
                'NODE_ENV': '"' + env.NODE_ENV + '"'
            })
        ],
        resolve: {
            extensions: ['.js', '.vue', '.less'],
            alias: {
                "@": path.resolve(__dirname, "./Content/src"),
                'vue': env.NODE_ENV === 'development' ? 'vue/dist/vue.js' : 'vue/dist/vue.min.js'
            }
        }
    }
};