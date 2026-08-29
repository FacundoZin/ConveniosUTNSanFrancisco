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
import InvolucradoConveniosView from '@/modules/involucrados/views/InvolucradoConveniosView.vue'
import { useAuthStore } from '@/modules/auth/stores/authStore'
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
      path: '/CargarConvenioEspecifico/:id?',
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
    {
      path: '/involucrado/:id/convenios',
      name: 'InvolucradoConvenios',
      component: InvolucradoConveniosView,
      props: true,
    },
    {
      path: '/admin',
      name: 'AdminUsuarios',
      component: () => import('@/modules/usuarios/views/AdminUsuariosView.vue'),
      meta: { requiresAdmin: true },
    },
    {
      path: '/login',
      name: 'Login',
      component: () => import('@/modules/auth/views/LoginView.vue'),
    },
  ],
})

router.beforeEach(async (to) => {
  const authStore = useAuthStore()

  if (to.path === '/login') {
    // Un usuario ya autenticado no debe ver el login.
    const autenticado = await authStore.restaurarSesion()
    if (autenticado) {
      return authStore.esAdmin ? '/admin' : '/'
    }
    return
  }

  // Toda ruta privada exige sesión activa; si Pinia está vacío se
  // intenta restaurarla desde la cookie consultando /Auth/me.
  const autenticado = await authStore.restaurarSesion()
  if (!autenticado) {
    return '/login'
  }

  // Si el usuario es Administrador, únicamente tiene acceso a /admin
  if (authStore.esAdmin) {
    if (to.path !== '/admin') {
      return '/admin'
    }
    return
  }

  // Un usuario no Administrador no puede acceder a rutas que requieren rol de Admin
  if (to.meta.requiresAdmin && !authStore.esAdmin) {
    return '/'
  }
})

export default router
