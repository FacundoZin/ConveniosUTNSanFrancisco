<template>
  <div v-if="show" class="modal-backdrop fade show"></div>
  <div
    class="modal fade"
    :class="{ show: show }"
    :style="{ display: show ? 'block' : 'none' }"
    tabindex="-1"
    aria-labelledby="createEmpresaModalLabel"
    :aria-hidden="!show"
    role="dialog"
  >
    <div class="modal-dialog modal-dialog-centered modal-lg">
      <div class="modal-content rounded-4 shadow border-0">
        <div class="modal-header border-bottom-0 pb-0 px-4 pt-4">
          <h5 class="modal-title fw-bold text-primary" id="createEmpresaModalLabel">
            <i class="bi bi-building-plus me-2"></i>Registrar Nueva Empresa
          </h5>
          <button
            type="button"
            class="btn-close"
            @click="emit('close')"
            aria-label="Cerrar"
          ></button>
        </div>
        <div class="modal-body p-4">
          <p class="text-muted mb-4">Complete los datos de la empresa para registrarla en el sistema.</p>
          <form @submit.prevent="submitForm" class="row g-3">
            <div class="col-md-6">
              <label class="form-label fw-semibold">Nombre de la Empresa *</label>
              <input 
                v-model="form.nombre" 
                type="text" 
                class="form-control shadow-sm border-2" 
                placeholder="Ej: Tech Solutions S.A."
                required 
              />
            </div>

            <div class="col-md-6">
              <label class="form-label fw-semibold">Razón Social</label>
              <input 
                v-model="form.razonSocial" 
                type="text" 
                class="form-control shadow-sm border-2" 
                placeholder="Nombre legal completo"
              />
            </div>

            <div class="col-md-6">
              <label class="form-label fw-semibold">CUIT</label>
              <input 
                v-model="form.cuit" 
                type="text" 
                class="form-control shadow-sm border-2" 
                placeholder="00-00000000-0"
              />
            </div>

            <div class="col-md-6">
              <label class="form-label fw-semibold">Teléfono de Contacto</label>
              <input 
                v-model="form.telefono" 
                type="text" 
                class="form-control shadow-sm border-2" 
                placeholder="+54 3564 000000"
              />
            </div>

            <div class="col-md-6">
              <label class="form-label fw-semibold">Email de Contacto</label>
              <input 
                v-model="form.email" 
                type="email" 
                class="form-control shadow-sm border-2" 
                placeholder="contacto@empresa.com"
              />
            </div>

            <div class="col-md-6">
              <label class="form-label fw-semibold">Dirección</label>
              <input 
                v-model="form.direccion" 
                type="text" 
                class="form-control shadow-sm border-2" 
                placeholder="Calle, Número, Ciudad"
              />
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
                <span>Registrar Empresa</span>
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import ApiService from '@/Services/ApiService'
import type { InsertEmpresaDto } from '@/Types/Empresa/InsertEmpresa'
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

const form = ref<InsertEmpresaDto>({
  id: null,
  nombre: '',
  razonSocial: '',
  cuit: '',
  direccion: '',
  telefono: '',
  email: '',
})

const submitForm = async () => {
  if (!form.value.nombre) {
    toast.error('El nombre es obligatorio', { position: POSITION.BOTTOM_CENTER })
    return
  }

  isSubmitting.value = true
  try {
    const response = await ApiService.CrearEmpresa(form.value)
    if (response.isSuccess) {
      toast.success('Empresa registrada con éxito')
      resetForm()
      emit('success')
      emit('close')
    } else {
      toast.error(response.error?.message || 'Error al registrar la empresa', {
        position: POSITION.BOTTOM_CENTER,
      })
    }
  } catch (e) {
    toast.error('Ocurrió un error inesperado al conectar con el servidor', { position: POSITION.BOTTOM_CENTER })
  } finally {
    isSubmitting.value = false
  }
}

const resetForm = () => {
  form.value = {
    id: null,
    nombre: '',
    razonSocial: '',
    cuit: '',
    direccion: '',
    telefono: '',
    email: '',
  }
}
</script>

<style scoped>
.modal-backdrop {
  background-color: rgba(0, 0, 0, 0.5);
  backdrop-filter: blur(4px);
}

.form-control:focus {
  border-color: var(--bs-primary);
  box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.15);
}

.transition-all {
  transition: all 0.3s ease;
}
</style>
