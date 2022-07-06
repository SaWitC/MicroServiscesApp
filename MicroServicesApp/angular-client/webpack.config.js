//const path = require('path');
//const webpack = require('webpack');
//const { CleanWebpackPlugin } = require('clean-webpack-plugin')
//const HtmlWebpackPlugin = require('html-webpack-plugin');
//const MiniCssExtractPlugin = require("mini-css-extract-plugin");
//module.exports = {
// // context: path.resolve(__dirname,'src'),
//  mode:'development',
//  entry: {

//    'polyfills': './src/polyfills.ts',
//    'app': './src/main.ts'

//    //app: './main.ts',
//    //polyfills: './polyfills.ts'

//    //'polyfills': './polyfills.ts',
//    //'app': './main.ts'
//  },
//  output: {
//    path: path.resolve(__dirname, 'dist'),     // путь к каталогу выходных файлов - папка public
//    publicPath: '/',
///*    filename: '[name].[fullhash].js'*/
//    filename: '[name].js'

//  },
//  plugins: [
//    new HtmlWebpackPlugin({
//      template:'./src/index.html'
//    }),
//    new CleanWebpackPlugin()
//  ],
//  module: {
//    rules: [
//      {
//        test: /\.tsx?$/,
//        loader: 'ts-loader',
//      },
//      {
//        test:/\.css$/, ////<-----
//        use:['style-loader','css-loader']
//      },
//      //{
//      //  test: /\.m?js$/,
//      //  exclude: /(node_modules|bower_components)/,
//      //  use: {
//      //    loader: 'babel-loader',
//      //    options: {
//      //      presets: ['@babel/preset-env']
//      //    }
//      //  }
//      //}
//    ]
//     //loaders: [ { 
//     //           test   : /.js$/,
//     //           loader : 'babel-loader'
//     //       }
//     //   ]
//  },
//    resolve: {
//      extensions: ['.ts', '.js', '.html', '.json', '.css'],
//  },
//  devServer: {
//    historyApiFallback: true,
//    port: 8081,
//    open: true
//  },
//}



const path = require('path');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
module.exports = {
  mode: 'development',
  entry: {
    'polyfills': './src/polyfills.ts',
    'app': './src/main.ts'
  },
  output: {
    path: path.resolve(__dirname, 'dist'),     // путь к каталогу выходных файлов - папка public
    publicPath: '/',
    filename: '[name]_[fullhash].js'
  },
  devServer: {
    historyApiFallback: true,
    port: 8081,
    open: true
  },
  resolve: {
    extensions: ['.ts', '.js']
  },
  module: {
    rules: [   //загрузчик для ts
      {
        test: /\.js$/,
        enforce: 'pre',
        use: ['source-map-loader'],
      },
      {
        test: /\.ts$/, // определяем тип файлов
        use: [
          {
            loader: 'ts-loader',
            options: { configFile: path.resolve(__dirname, 'tsconfig.json') }
          },
          'angular2-template-loader'
        ]
      }, {
        test: /\.html$/,
        loader: 'html-loader'
      }, {
        test: /\.(png|jpe?g|gif|svg|woff|woff2|ttf|eot|ico)$/,
        loader: 'file-loader',
        options: {
          name: '[name].[fullhash].[ext]',
        }
      }, {
        test: /\.css$/,
        exclude: path.resolve(__dirname, 'src/app'),
        use: [
          MiniCssExtractPlugin.loader,
          "css-loader"
        ]
      }, {
        test: /\.css$/,
        include: path.resolve(__dirname, 'src/app'),
        loader: 'raw-loader'
      }
    ]
  },
  plugins: [
    new webpack.ContextReplacementPlugin(
      /angular(\\|\/)core/,
      path.resolve(__dirname, 'src'), // каталог с исходными файлами
      {} // карта маршрутов
    ),
    new HtmlWebpackPlugin({
      template: 'src/index.html'
    }),
    new MiniCssExtractPlugin({
      filename: "[name].css"
    }),
    new webpack.NoEmitOnErrorsPlugin(),
    new webpack.LoaderOptionsPlugin({
      htmlLoader: {
        minimize: false
      }
    })
  ]
}
