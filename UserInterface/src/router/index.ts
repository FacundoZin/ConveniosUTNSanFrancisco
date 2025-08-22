import VistaConvenioEspecifico from '@/Components/VistaConvenioEspecifico.vue'
import VistaConvenioMarco from '@/Components/VistaConvenioMarco.vue'
import DashBoardView from '@/Views/DashBoardView.vue'
import { createRouter, createWebHashHistory, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHashHistory(import.meta.env.BASE_URL),
  routes: [
    { path: '/', component: DashBoardView},
    { path: '/ConvenioMarco/:id', name: 'VistaConvenioMarco', component: VistaConvenioMarco },
    { path: '/ConvenioEspecifico/:id', name: 'VistaConvenioEspecifico', component: VistaConvenioEspecifico, props: true }
  ],
})

export default router
