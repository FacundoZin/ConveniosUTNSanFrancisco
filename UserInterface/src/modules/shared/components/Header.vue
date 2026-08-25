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
  <header class="main-header">
    <div class="header-content">
      <!-- Logo -->
      <router-link to="/">
        <img src="/Images/logoUTN.svg" alt="Logo UTN" class="logo" />
      </router-link>

      <!-- Links (a la derecha) -->
      <nav class="nav-links">
        <router-link to="/">Panel de búsqueda</router-link>
        <router-link to="/empresas">Empresas</router-link>
        <router-link to="/CargarConvenioMarco">Cargar Marco</router-link>
        <router-link to="/CargarConvenioEspecifico">Cargar Especifico</router-link>
        <router-link to="/involucrados-por-area">Involucrados</router-link>
      </nav>

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
.main-header {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  background-color: #f5f5f58d;
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
  transition:
    transform 0.3s ease,
    filter 0.25s ease;
}

.logo:hover {
  transform: scale(1.05);
  filter: brightness(0) invert(0.4);
}

.nav-links {
  display: flex;
  gap: 2rem;
  align-items: center;
  margin-left: auto;
}

.nav-links a {
  position: relative;
  color: rgb(69, 66, 66);
  text-decoration: none;
  font-weight: 500;
  font-size: 1rem;
  line-height: 1;
  transition: color 0.3s ease;
}

.nav-links a::after {
  content: '';
  position: absolute;
  bottom: -6px;
  left: 0;
  width: 0%;
  height: 2px;
  background: rgb(118, 113, 113);
  transition: width 0.3s ease;
}

.nav-links a:hover::after {
  width: 100%;
}

.nav-links a.router-link-active::after,
.nav-links a.router-link-exact-active::after {
  width: 100%;
  background: rgb(69, 66, 66);
}

/* Área de usuario */
.user-area {
  display: flex;
  align-items: center;
  gap: 0.65rem;
  margin-left: 1.5rem;
  padding-left: 1.5rem;
  border-left: 1px solid #d1d5db;
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
