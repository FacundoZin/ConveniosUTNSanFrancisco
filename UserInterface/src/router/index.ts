import EditConvenioEspecificoForm from '@/Components/EditConvenioEspecificoForm.vue'
import EditConvenioMarcoForm from '@/Components/EditConvenioMarcoForm.vue'
import FormCargaConvEspecifico from '@/Components/FormCargaConvEspecifico.vue'
import FormCargaConvMarco from '@/Components/FormCargaConvMarco.vue'
import VistaConvenioEspecifico from '@/Components/VistaConvenioEspecifico.vue'
import VistaConvenioMarco from '@/Components/VistaConvenioMarco.vue'
import DashBoardView from '@/Views/DashBoardView.vue'
import { createRouter, createWebHashHistory } from 'vue-router'

const router = createRouter({
  history: createWebHashHistory('/'),
  routes: [
    { path: '/', component: DashBoardView, name: 'ListaConvenios' },
    { path: '/ConvenioMarco/:id', name: 'VistaConvenioMarco', component: VistaConvenioMarco },
    { path: '/ConvenioEspecifico/:id', name: 'VistaConvenioEspecifico', component: VistaConvenioEspecifico, props: true },
    { path: '/editConvenioMarco/:id', name: 'EditConvenioMarco', component: EditConvenioMarcoForm, props: true } ,
    { path: '/editConvenioEspecifico/:id', name: 'EditConvenioEspecifico', component: EditConvenioEspecificoForm, props: true },
    { path: '/CargarConvenioMarco', name: 'CargarConvenioMarco', component: FormCargaConvMarco},
    { path: '/CargarConvenioEspecifico/:id', name: 'CreateConvenioEspecifico', component: FormCargaConvEspecifico, props: true},
  ],
})

export default router
