import EditConvenioEspecificoForm from '@/Components/EditConvenioEspecificoForm.vue'
import EditConvenioMarcoForm from '@/Components/EditConvenioMarcoForm.vue'
import VistaConvenioEspecifico from '@/Components/VistaConvenioEspecifico.vue'
import VistaConvenioMarco from '@/Components/VistaConvenioMarco.vue'
import DashBoardView from '@/Views/DashBoardView.vue'
import { createRouter, createWebHashHistory } from 'vue-router'

const router = createRouter({
  history: createWebHashHistory('/'),
  routes: [
    { path: '/', component: DashBoardView},
    { path: '/ConvenioMarco/:id', name: 'VistaConvenioMarco', component: VistaConvenioMarco },
    { path: '/ConvenioEspecifico/:id', name: 'VistaConvenioEspecifico', component: VistaConvenioEspecifico, props: true },
    { path: '/editConvenioMarco/:id', name: 'EditConvenioMarco', component: EditConvenioMarcoForm, props: true } ,
    { path: '/editConvenioEspecifico/:id', name: 'EditConvenioEspecifico', component: EditConvenioEspecificoForm, props: true } 
  ],
})

export default router
