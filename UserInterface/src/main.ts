import { createPinia } from 'pinia'
import { createApp } from 'vue'

import './Assets/Global.css'
import Toast from 'vue-toastification'
import 'vue-toastification/dist/index.css'
import App from './App.vue'
import router from './router'
import { setupAxiosInterceptors } from './Services/axiosSetup'

import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap'
import 'bootstrap-icons/font/bootstrap-icons.css'

setupAxiosInterceptors()

const app = createApp(App)

app.use(createPinia())
app.use(router)
app.use(Toast)
app.mount('#app')
