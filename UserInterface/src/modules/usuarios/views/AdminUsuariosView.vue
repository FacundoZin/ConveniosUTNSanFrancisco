<script setup lang="ts">
import CambiarPasswordModal from '@/modules/usuarios/components/modals/CambiarPasswordModal.vue'
import CreateUsuarioModal from '@/modules/usuarios/components/modals/CreateUsuarioModal.vue'
import UsuarioService from '@/modules/usuarios/services/UsuarioService'
import type { RolUsuario } from '@/Types/Usuarios/UsuarioDto'
import type { UsuarioDto } from '@/Types/Usuarios/UsuarioDto'
import { onMounted, ref } from 'vue'
import { POSITION, useToast } from 'vue-toastification'

const toast = useToast()

const usuarios = ref<UsuarioDto[]>([])
const isLoading = ref(false)
const error = ref<string | null>(null)

const showCreateModal = ref(false)
const usuarioParaPassword = ref<UsuarioDto | null>(null)
const showPasswordModal = ref(false)
const usuarioAEliminar = ref<UsuarioDto | null>(null)
const isDeleting = ref(false)

const fetchUsuarios = async () => {
  isLoading.value = true
  error.value = null
  try {
    const response = await UsuarioService.listar()
    if (response.isSuccess) {
      usuarios.value = response.value
    } else {
      error.value = response.error?.message || 'Error al cargar los usuarios.'
    }
  } catch (e) {
    console.error(e)
    error.value = 'Error al cargar los usuarios.'
  } finally {
    isLoading.value = false
  }
}

const abrirCambiarPassword = (usuario: UsuarioDto) => {
  usuarioParaPassword.value = usuario
  showPasswordModal.value = true
}

const pedirConfirmacionEliminar = (usuario: UsuarioDto) => {
  usuarioAEliminar.value = usuario
}

const cancelarEliminacion = () => {
  usuarioAEliminar.value = null
}

const confirmarEliminar = async () => {
  if (!usuarioAEliminar.value) return

  isDeleting.value = true
  try {
    const response = await UsuarioService.eliminar(usuarioAEliminar.value.id)
    if (response.isSuccess) {
      toast.success('Usuario eliminado con éxito', { position: POSITION.BOTTOM_CENTER })
      usuarioAEliminar.value = null
      await fetchUsuarios()
    } else {
      // Mensajes del backend: autoeliminarse o eliminar al último administrador.
      toast.error(response.error?.message || 'No se pudo eliminar el usuario.', {
        position: POSITION.BOTTOM_CENTER,
      })
    }
  } catch {
    toast.error('Ocurrió un error inesperado al conectar con el servidor', {
      position: POSITION.BOTTOM_CENTER,
    })
  } finally {
    isDeleting.value = false
  }
}

const badgeRolClass = (rol: RolUsuario | string): string => {
  return rol === 'Administrador' ? 'bg-primary' : 'bg-secondary'
}

onMounted(() => {
  fetchUsuarios()
})
</script>

<template>
  <div class="container-fluid px-4 py-5">
    <div class="d-flex justify-content-between align-items-center mb-5">
      <div>
        <h2 class="fw-bold text-primary mb-0">Administración de Usuarios</h2>
      </div>
      <button
        class="btn btn-primary btn-lg rounded-pill shadow-sm px-4 d-flex align-items-center gap-2"
        @click="showCreateModal = true"
      >
        <i class="bi bi-person-plus-fill fs-5"></i>
        <span class="fw-semibold">Registrar Usuario</span>
      </button>
    </div>

    <!-- Loading State -->
    <div v-if="isLoading" class="d-flex justify-content-center align-items-center min-vh-50">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Cargando...</span>
      </div>
    </div>

    <!-- Error State -->
    <div v-else-if="error" class="alert alert-danger shadow-sm rounded-4" role="alert">
      <i class="bi bi-exclamation-triangle-fill me-2"></i>
      {{ error }}
    </div>

    <!-- Empty State -->
    <div v-else-if="usuarios.length === 0" class="text-center py-5">
      <div class="mb-3 text-muted">
        <i class="bi bi-people" style="font-size: 3rem"></i>
      </div>
      <h4 class="text-muted">No hay usuarios registrados</h4>
    </div>

    <!-- Tabla de usuarios -->
    <div v-else class="card border-0 shadow-sm rounded-4 overflow-hidden">
      <div class="table-responsive">
        <table class="table table-hover align-middle mb-0">
          <thead class="table-light">
            <tr>
              <th scope="col" class="ps-4 fw-semibold text-muted">Usuario</th>
              <th scope="col" class="fw-semibold text-muted">Nombre</th>
              <th scope="col" class="fw-semibold text-muted">Rol</th>
              <th scope="col" class="pe-4 text-end fw-semibold text-muted">Acciones</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="usuario in usuarios" :key="usuario.id">
              <td class="ps-4 fw-semibold">{{ usuario.username }}</td>
              <td>{{ usuario.nombre }}</td>
              <td>
                <span class="badge rounded-pill" :class="badgeRolClass(usuario.rol)">
                  {{ usuario.rol }}
                </span>
              </td>
              <td class="pe-4 text-end">
                <button
                  class="btn btn-sm btn-outline-primary rounded-pill me-2 d-inline-flex align-items-center gap-1"
                  title="Cambiar contraseña"
                  @click="abrirCambiarPassword(usuario)"
                >
                  <i class="bi bi-key-fill"></i>
                  <span>Contraseña</span>
                </button>
                <button
                  class="btn btn-sm btn-outline-danger rounded-pill d-inline-flex align-items-center gap-1"
                  title="Eliminar usuario"
                  @click="pedirConfirmacionEliminar(usuario)"
                >
                  <i class="bi bi-trash-fill"></i>
                  <span>Eliminar</span>
                </button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>

    <!-- Modal: crear usuario -->
    <CreateUsuarioModal :show="showCreateModal" @close="showCreateModal = false" @success="fetchUsuarios" />

    <!-- Modal: cambiar contraseña -->
    <CambiarPasswordModal
      :show="showPasswordModal"
      :usuario="usuarioParaPassword"
      @close="showPasswordModal = false; usuarioParaPassword = null"
      @success="fetchUsuarios"
    />

    <!-- Modal: confirmar eliminación -->
    <div v-if="usuarioAEliminar" class="modal-backdrop fade show"></div>
    <div
      class="modal fade"
      :class="{ show: !!usuarioAEliminar }"
      :style="{ display: usuarioAEliminar ? 'block' : 'none' }"
      tabindex="-1"
      aria-labelledby="confirmarEliminarLabel"
      :aria-hidden="!usuarioAEliminar"
      role="dialog"
    >
      <div class="modal-dialog modal-dialog-centered modal-sm">
        <div class="modal-content rounded-4 shadow border-0">
          <div class="modal-body p-4 text-center">
            <i class="bi bi-exclamation-triangle-fill text-danger" style="font-size: 2.5rem"></i>
            <h5 class="fw-bold mt-3 mb-2" id="confirmarEliminarLabel">Eliminar usuario</h5>
            <p class="text-muted mb-0" v-if="usuarioAEliminar">
              ¿Está seguro de que desea eliminar al usuario
              <strong>{{ usuarioAEliminar.username }}</strong
              >? Esta acción no se puede deshacer.
            </p>
          </div>
          <div class="modal-footer border-top-0 justify-content-center pb-4">
            <button type="button" class="btn btn-outline-secondary rounded-pill px-4" @click="cancelarEliminacion">
              Cancelar
            </button>
            <button
              type="button"
              class="btn btn-danger rounded-pill px-4 d-flex align-items-center gap-2"
              :disabled="isDeleting"
              @click="confirmarEliminar"
            >
              <span
                v-if="isDeleting"
                class="spinner-border spinner-border-sm"
                role="status"
                aria-hidden="true"
              ></span>
              <span>Eliminar</span>
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.min-vh-50 {
  min-height: 50vh;
}
</style>
