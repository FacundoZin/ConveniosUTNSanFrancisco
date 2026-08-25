<template>
  <div v-if="show" class="modal-backdrop fade show"></div>
  <div
    class="modal fade"
    :class="{ show: show }"
    :style="{ display: show ? 'block' : 'none' }"
    tabindex="-1"
    aria-labelledby="cambiarPasswordModalLabel"
    :aria-hidden="!show"
    role="dialog"
  >
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content rounded-4 shadow border-0">
        <div class="modal-header border-bottom-0 pb-0 px-4 pt-4">
          <h5 class="modal-title fw-bold text-primary" id="cambiarPasswordModalLabel">
            <i class="bi bi-key-fill me-2"></i>Cambiar Contraseña
          </h5>
          <button
            type="button"
            class="btn-close"
            @click="emit('close')"
            aria-label="Cerrar"
          ></button>
        </div>
        <div class="modal-body p-4">
          <p class="text-muted mb-4" v-if="usuario">
            Ingrese la nueva contraseña para el usuario
            <strong>{{ usuario.nombre || usuario.username }}</strong>.
          </p>
          <form @submit.prevent="submitForm" class="row g-3">
            <div class="col-12">
              <label class="form-label fw-semibold">Nueva Contraseña *</label>
              <input
                v-model="newPassword"
                type="password"
                class="form-control shadow-sm border-2"
                placeholder="Mínimo 8 caracteres"
                autocomplete="new-password"
                required
              />
            </div>

            <div class="col-12">
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
                <span>Guardar</span>
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
import type { UsuarioDto } from '@/Types/Usuarios/UsuarioDto'
import { computed, ref } from 'vue'
import { POSITION, useToast } from 'vue-toastification'

const props = defineProps<{
  show: boolean
  usuario: UsuarioDto | null
}>()

const emit = defineEmits<{
  (e: 'close'): void
  (e: 'success'): void
}>()

const toast = useToast()
const isSubmitting = ref(false)
const errorValidacion = ref<string | null>(null)
const newPassword = ref('')
const confirmarPassword = ref('')

const usuarioId = computed(() => props.usuario?.id ?? null)

const validarForm = (): string | null => {
  if (!newPassword.value || !confirmarPassword.value) {
    return 'Complete ambos campos.'
  }
  if (newPassword.value.length < 8) {
    return 'La contraseña debe tener al menos 8 caracteres.'
  }
  if (newPassword.value !== confirmarPassword.value) {
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
    const response = await UsuarioService.cambiarPassword(usuarioId.value!, newPassword.value)
    if (response.isSuccess) {
      toast.success('Contraseña actualizada con éxito', { position: POSITION.BOTTOM_CENTER })
      resetForm()
      emit('success')
      emit('close')
    } else {
      errorValidacion.value = response.error?.message || 'Error al cambiar la contraseña.'
    }
  } catch {
    errorValidacion.value = 'Ocurrió un error inesperado al conectar con el servidor.'
  } finally {
    isSubmitting.value = false
  }
}

const resetForm = () => {
  newPassword.value = ''
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
