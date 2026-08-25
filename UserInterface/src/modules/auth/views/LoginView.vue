<script setup lang="ts">
import AuthService from '@/modules/auth/services/AuthService'
import { useAuthStore } from '@/modules/auth/stores/authStore'
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const authStore = useAuthStore()

const username = ref('')
const password = ref('')
const isLoading = ref(false)
const error = ref<string | null>(null)

const handleSubmit = async () => {
  if (!username.value || !password.value) {
    error.value = 'Ingrese usuario y contraseña.'
    return
  }

  isLoading.value = true
  error.value = null

  const result = await AuthService.login(username.value, password.value)
  isLoading.value = false

  if (result.isSuccess) {
    authStore.establecerSesion(result.value)
    router.push(authStore.esAdmin ? '/admin' : '/')
  } else {
    error.value = result.error.message || 'Credenciales inválidas.'
  }
}
</script>

<template>
  <div class="container d-flex justify-content-center align-items-center min-vh-100">
    <div class="card border-0 shadow-sm rounded-4 p-4" style="max-width: 400px; width: 100%">
      <div class="card-body">
        <div class="text-center mb-4">
          <i class="bi bi-shield-lock-fill text-primary" style="font-size: 3rem"></i>
          <h3 class="fw-bold mt-3 mb-1">Iniciar Sesión</h3>
          <span class="text-muted">Sistema de Convenios UTN San Francisco</span>
        </div>

        <form @submit.prevent="handleSubmit">
          <div class="mb-3">
            <label for="username" class="form-label">Usuario</label>
            <input
              id="username"
              v-model="username"
              type="text"
              class="form-control"
              autocomplete="username"
              placeholder="Ingrese su usuario"
            />
          </div>

          <div class="mb-3">
            <label for="password" class="form-label">Contraseña</label>
            <input
              id="password"
              v-model="password"
              type="password"
              class="form-control"
              autocomplete="current-password"
              placeholder="Ingrese su contraseña"
            />
          </div>

          <div v-if="error" class="alert alert-danger shadow-sm rounded-3" role="alert">
            <i class="bi bi-exclamation-triangle-fill me-2"></i>
            {{ error }}
          </div>

          <button type="submit" class="btn btn-primary btn-lg rounded-pill w-100" :disabled="isLoading">
            <span v-if="isLoading" class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>
            Ingresar
          </button>
        </form>
      </div>
    </div>
  </div>
</template>
