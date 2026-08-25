<template>
  <div v-if="show" class="modal-backdrop fade show"></div>
  <div
    class="modal fade"
    :class="{ show: show }"
    :style="{ display: show ? 'block' : 'none' }"
    tabindex="-1"
    aria-labelledby="createUsuarioModalLabel"
    :aria-hidden="!show"
    role="dialog"
  >
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content rounded-4 shadow border-0">
        <div class="modal-header border-bottom-0 pb-0 px-4 pt-4">
          <h5 class="modal-title fw-bold text-primary" id="createUsuarioModalLabel">
            <i class="bi bi-person-plus-fill me-2"></i>Registrar Nuevo Usuario
          </h5>
          <button
            type="button"
            class="btn-close"
            @click="emit('close')"
            aria-label="Cerrar"
          ></button>
        </div>
        <div class="modal-body p-4">
          <p class="text-muted mb-4">
            Complete los datos del nuevo usuario y seleccione su rol.
          </p>
          <form @submit.prevent="submitForm" class="row g-3">
            <div class="col-12">
              <label class="form-label fw-semibold">Nombre de Usuario *</label>
              <input
                v-model="form.username"
                type="text"
                class="form-control shadow-sm border-2"
                placeholder="Ej: jperez"
                required
              />
            </div>

            <div class="col-12">
              <label class="form-label fw-semibold">Nombre Completo *</label>
              <input
                v-model="form.nombre"
                type="text"
                class="form-control shadow-sm border-2"
                placeholder="Ej: Juan Pérez"
                required
              />
            </div>

            <div class="col-md-6">
              <label class="form-label fw-semibold">Contraseña *</label>
              <input
                v-model="form.password"
                type="password"
                class="form-control shadow-sm border-2"
                placeholder="Mínimo 8 caracteres"
                autocomplete="new-password"
                required
              />
            </div>

            <div class="col-md-6">
              <label class="form-label fw-semibold">Confirmar Contraseña *</label>
              <input
                v-model="confirmarPassword"
                type="password"
                class="form-control shadow-sm border-2"
                placeholder="Repita la contraseña"
                autocomplete="new-password"
                required
              />
            </div>

            <div class="col-12">
              <label class="form-label fw-semibold">Rol *</label>
              <select v-model="form.rol" class="form-select shadow-sm border-2" required>
                <option value="Secretario">Secretario</option>
                <option value="Administrador">Administrador</option>
              </select>
            </div>

            <div v-if="errorValidacion" class="col-12">
              <div class="alert alert-danger py-2 mb-0 rounded-3" role="alert">
                <i class="bi bi-exclamation-triangle-fill me-2"></i>
                {{ errorValidacion }}
              </div>
            </div>

            <div class="col-12 mt-4 d-flex justify-content-end gap-2">
              <button
                type="button"
                class="btn btn-outline-secondary rounded-pill px-4"
                @click="emit('close')"
              >
                Cancelar
              </button>
              <button
                type="submit"
                class="btn btn-primary rounded-pill px-4 d-flex align-items-center gap-2"
                :disabled="isSubmitting"
              >
                <span
                  v-if="isSubmitting"
                  class="spinner-border spinner-border-sm"
                  role="status"
                  aria-hidden="true"
                ></span>
                <i v-else class="bi bi-check-lg"></i>
                <span>Registrar Usuario</span>
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import UsuarioService from '@/modules/usuarios/services/UsuarioService'
import type { InsertUsuarioDto } from '@/Types/Usuarios/InsertUsuarioDto'
import { ref } from 'vue'
import { POSITION, useToast } from 'vue-toastification'

defineProps<{
  show: boolean
}>()

const emit = defineEmits<{
  (e: 'close'): void
  (e: 'success'): void
}>()

const toast = useToast()
const isSubmitting = ref(false)
const errorValidacion = ref<string | null>(null)
const confirmarPassword = ref('')

const form = ref<InsertUsuarioDto>({
  username: '',
  nombre: '',
  password: '',
  rol: 'Secretario',
})

const validarForm = (): string | null => {
  if (!form.value.username || !form.value.nombre || !form.value.password) {
    return 'Complete todos los campos obligatorios.'
  }
  if (form.value.password.length < 8) {
    return 'La contraseña debe tener al menos 8 caracteres.'
  }
  if (form.value.password !== confirmarPassword.value) {
    return 'Las contraseñas no coinciden.'
  }
  return null
}

const submitForm = async () => {
  errorValidacion.value = validarForm()
  if (errorValidacion.value) {
    return
  }

  isSubmitting.value = true
  try {
    const response = await UsuarioService.crear(form.value)
    if (response.isSuccess) {
      toast.success('Usuario registrado con éxito', { position: POSITION.BOTTOM_CENTER })
      resetForm()
      emit('success')
      emit('close')
    } else {
      // Incluye el 409 de nombre de usuario duplicado.
      errorValidacion.value = response.error?.message || 'Error al registrar el usuario.'
    }
  } catch {
    errorValidacion.value = 'Ocurrió un error inesperado al conectar con el servidor.'
  } finally {
    isSubmitting.value = false
  }
}

const resetForm = () => {
  form.value = {
    username: '',
    nombre: '',
    password: '',
    rol: 'Secretario',
  }
  confirmarPassword.value = ''
  errorValidacion.value = null
}
</script>

<style scoped>
.modal-backdrop {
  background-color: rgba(0, 0, 0, 0.5);
  backdrop-filter: blur(4px);
}
</style>
