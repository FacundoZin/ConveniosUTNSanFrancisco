<script setup lang="ts">
import AuthService from '@/modules/auth/services/AuthService'
import { useAuthStore } from '@/modules/auth/stores/authStore'
import { computed } from 'vue'
import { useRouter } from 'vue-router'

const authStore = useAuthStore()
const router = useRouter()

const cerrarSesion = async () => {
  await AuthService.logout()
  authStore.limpiarSesion()
  router.push('/login')
}

const iniciales = computed(() => {
  const fuente = authStore.nombre || authStore.username || ''
  if (!fuente) return 'U'
  const partes = fuente.trim().split(/\s+/)
  if (partes.length >= 2) {
    return (partes[0][0] + partes[1][0]).toUpperCase()
  }
  return fuente.substring(0, 2).toUpperCase()
})

const fechaFormatted = computed(() => {
  const hoy = new Date()
  return hoy.toLocaleDateString('es-AR', {
    day: 'numeric',
    month: 'short',
  })
})
</script>

<template>
  <header class="simple-header">
    <div class="header-content">
      <!-- Logo -->
      <router-link :to="authStore.esAdmin ? '/admin' : '/'">
        <img src="/Images/logoUTN.svg" alt="Logo UTN" class="logo" />
      </router-link>

      <!-- Usuario autenticado -->
      <div v-if="authStore.isAuthenticated" class="user-area">
        <div class="user-avatar" :title="authStore.nombre || authStore.username || ''">
          {{ iniciales }}
        </div>
        <div class="user-details">
          <span class="user-name" :title="authStore.nombre || authStore.username || ''">
            {{ authStore.nombre || authStore.username }}
          </span>
          <span class="user-date">{{ fechaFormatted }}</span>
        </div>
        <button
          type="button"
          class="logout-btn"
          title="Cerrar sesión"
          aria-label="Cerrar sesión"
          @click="cerrarSesion"
        >
          <i class="bi bi-box-arrow-right"></i>
        </button>
      </div>
    </div>
  </header>
</template>

<style scoped>
.simple-header {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  background: linear-gradient(
    to right,
    #ffffff 0%,
    #f8f8f8 50%,
    #ececec 100%
  );
  backdrop-filter: blur(8px);
  -webkit-backdrop-filter: blur(8px);
  z-index: 1000;
  height: 70px;
  display: flex;
  align-items: center;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
}

.header-content {
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 1rem;
  width: 100%;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.logo {
  height: 40px;
  object-fit: contain;
  display: block;
  transition: transform 0.3s ease;
}

.logo:hover {
  transform: scale(1.05);
}

.user-area {
  display: flex;
  align-items: center;
  gap: 0.65rem;
}

.user-avatar {
  width: 36px;
  height: 36px;
  border-radius: 50%;
  background: linear-gradient(135deg, #0d6efd 0%, #0a58ca 100%);
  color: #ffffff;
  display: flex;
  align-items: center;
  justify-content: center;
  font-weight: 700;
  font-size: 0.85rem;
  letter-spacing: 0.5px;
  box-shadow: 0 2px 4px rgba(13, 110, 253, 0.25);
  user-select: none;
  flex-shrink: 0;
}

.user-details {
  display: flex;
  flex-direction: column;
  line-height: 1.1;
}

.user-name {
  font-weight: 600;
  font-size: 0.88rem;
  color: rgb(69, 66, 66);
  max-width: 140px;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.user-date {
  font-size: 0.72rem;
  color: #888888;
  text-transform: capitalize;
}

.logout-btn {
  background: none;
  border: none;
  padding: 0.25rem 0.5rem;
  color: rgb(69, 66, 66);
  cursor: pointer;
  border-radius: 0.5rem;
  transition:
    color 0.3s ease,
    background-color 0.25s ease;
}

.logout-btn:hover {
  color: #dc3545;
  background-color: rgba(220, 53, 69, 0.1);
}
</style>
