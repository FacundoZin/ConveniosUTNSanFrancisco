import axios from 'axios'

import { useAuthStore } from '@/modules/auth/stores/authStore'

export function setupAxiosInterceptors(): void {
  // La autenticación es por cookie HttpOnly: en todas las peticiones
  // deben fluir las cookies de/ hacia el backend.
  axios.defaults.withCredentials = true

  axios.interceptors.response.use(
    (response) => response,
    (error) => {
      const status = error.response?.status
      const url: string = error.config?.url ?? ''

      // El login y el sondeo de sesión manejan su propio 401
      // (credenciales inválidas / sin sesión previa).
      const esLogin = url.includes('/Auth/login')
      const esSondeoSesion = url.includes('/Auth/me')

      if (status === 401 && !esLogin && !esSondeoSesion) {
        const authStore = useAuthStore()
        authStore.limpiarSesion()
        // Sin redirección manual: el guard del router redirige a /login.
      }

      // Los 403 se informan por el camino estándar de la app:
      // los servicios devuelven Result con el mensaje del backend
      // y cada vista lo muestra (toast/alerta), igual que el resto
      // de los errores de negocio.

      return Promise.reject(error)
    }
  )
}
