import DashBoardView from '@/Views/DashBoardView.vue'
import { createRouter, createWebHashHistory, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHashHistory(import.meta.env.BASE_URL),
  routes: [
    { path: '/', component: DashBoardView},
  ],
})

export default router
