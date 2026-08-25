import { defineStore } from 'pinia'
import { computed, ref } from 'vue'

import AuthService from '@/modules/auth/services/AuthService'
import type { SesionUsuarioDto } from '@/Types/Auth/SesionUsuarioDto'

export const useAuthStore = defineStore('auth', () => {
  const username = ref<string | null>(null)
  const nombre = ref<string | null>(null)
  const rol = ref<string | null>(null)

  // Indica si ya se consultó al servidor el estado real de la sesión,
  // para no repetir la llamada a /Auth/me en cada navegación.
  const sesionVerificada = ref(false)

  const isAuthenticated = computed(() => !!username.value)
  const esAdmin = computed(() => rol.value === 'Administrador')

  function establecerSesion(sesion: SesionUsuarioDto) {
    username.value = sesion.username
    nombre.value = sesion.nombre
    rol.value = sesion.rol
    sesionVerificada.value = true
  }

  function limpiarSesion() {
    username.value = null
    nombre.value = null
    rol.value = null
    sesionVerificada.value = true
  }

  /**
   * Restaura la sesión desde la cookie del servidor.
   * Si Pinia ya tiene sesión devuelve true sin consultar;
   * si no, consulta /Auth/me una única vez: 200 popula el store,
   * 401 limpia el estado y devuelve false.
   */
  async function restaurarSesion(): Promise<boolean> {
    if (isAuthenticated.value) {
      return true
    }

    if (!sesionVerificada.value) {
      const result = await AuthService.obtenerSesionActual()
      if (result.isSuccess) {
        establecerSesion(result.value)
      } else {
        limpiarSesion()
      }
    }

    return isAuthenticated.value
  }

  return {
    username,
    nombre,
    rol,
    sesionVerificada,
    isAuthenticated,
    esAdmin,
    establecerSesion,
    limpiarSesion,
    restaurarSesion
  }
})
