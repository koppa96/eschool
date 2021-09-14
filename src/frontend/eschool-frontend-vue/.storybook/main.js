const path = require('path')

module.exports = {
  stories: ['../src/**/*.stories.mdx', '../src/**/*.stories.@(js|jsx|ts|tsx)'],
  addons: ['@storybook/addon-links', '@storybook/addon-essentials'],
  webpackFinal: async (config, { configType }) => {
    const src = path.resolve(__dirname, '..', 'src/')

    config.resolve.alias = {
      ...config.alias,
      vue: 'vue/dist/vue.esm-bundler.js',
      '@': src
    }

    config.module.rules.push({
      test: /\.(scss|sass)$/,
      use: ['style-loader', 'css-loader', 'sass-loader'],
      include: path.resolve(__dirname, '../')
    })

    return config
  }
}
