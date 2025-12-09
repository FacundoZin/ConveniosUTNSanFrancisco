import CargaConvEspecificoView from '@/Views/CargaConvEspecificoView.vue'
import CargaConvMarcoView from '@/Views/CargaConvMarcoView.vue'
import ConvenioEspecificoView from '@/Views/ConvenioEspecificoView.vue'
import ConvenioMarcoView from '@/Views/ConvenioMarcoView.vue'
import DashboardEmpresas from '@/Views/DashboardEmpresas.vue'
import DashBoardView from '@/Views/DashBoardView.vue'
import EditConvenioEspecificoView from '@/Views/EditConvenioEspecificoView.vue'
import EditConvenioMarcoView from '@/Views/EditConvenioMarcoView.vue'
import EmpresaConveniosView from '@/Views/EmpresaConveniosView.vue'
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
  ],
})

export default router
