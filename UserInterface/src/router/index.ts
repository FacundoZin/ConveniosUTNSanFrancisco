import CargaConvEspecificoView from '@/modules/convenios/views/CargaConvEspecificoView.vue'
import CargaConvMarcoView from '@/modules/convenios/views/CargaConvMarcoView.vue'
import ConvenioEspecificoView from '@/modules/convenios/views/ConvenioEspecificoView.vue'
import ConvenioMarcoView from '@/modules/convenios/views/ConvenioMarcoView.vue'
import EditConvenioEspecificoView from '@/modules/convenios/views/EditConvenioEspecificoView.vue'
import EditConvenioMarcoView from '@/modules/convenios/views/EditConvenioMarcoView.vue'
import DashBoardView from '@/modules/convenios/views/DashBoardView.vue'
import DashboardEmpresas from '@/modules/empresas/views/DashboardEmpresas.vue'
import EmpresaConveniosView from '@/modules/empresas/views/EmpresaConveniosView.vue'
import InvolucradosPorAreaView from '@/modules/involucrados/views/InvolucradosPorAreaView.vue'
import { createRouter, createWebHashHistory } from 'vue-router'

const router = createRouter({
  history: createWebHashHistory('/'),
  routes: [
    { path: '/', component: DashBoardView, name: 'ListaConvenios' },
    { path: '/ConvenioMarco/:id', name: 'VistaConvenioMarco', component: ConvenioMarcoView },
    {
      path: '/ConvenioEspecifico/:id',
      name: 'VistaConvenioEspecifico',
      component: ConvenioEspecificoView,
    },
    { path: '/editConvenioMarco/:id', name: 'EditConvenioMarco', component: EditConvenioMarcoView },
    {
      path: '/editConvenioEspecifico/:id',
      name: 'EditConvenioEspecifico',
      component: EditConvenioEspecificoView,
      props: true,
    },
    { path: '/CargarConvenioMarco', name: 'CargarConvenioMarco', component: CargaConvMarcoView },
    {
      path: '/CargarConvenioEspecifico',
      name: 'CreateConvenioEspecifico',
      component: CargaConvEspecificoView,
      props: true,
    },
    { path: '/empresas', name: 'Empresas', component: DashboardEmpresas },
    {
      path: '/empresa/:id/convenios',
      name: 'EmpresaConvenios',
      component: EmpresaConveniosView,
      props: true,
    },
    {
      path: '/involucrados-por-area',
      name: 'InvolucradosPorArea',
      component: InvolucradosPorAreaView,
    },
  ],
})

export default router
