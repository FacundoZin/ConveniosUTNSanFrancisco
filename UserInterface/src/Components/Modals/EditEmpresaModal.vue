<template>
  <div v-if="show" class="modal-backdrop fade show"></div>
  <div
    class="modal fade"
    :class="{ show: show }"
    :style="{ display: show ? 'block' : 'none' }"
    tabindex="-1"
    aria-labelledby="editEmpresaModalLabel"
    :aria-hidden="!show"
    role="dialog"
  >
    <div class="modal-dialog modal-dialog-centered modal-lg">
      <div class="modal-content rounded-4 shadow border-0">
        <div class="modal-header border-bottom-0 pb-0">
          <h5 class="modal-title fw-bold text-primary" id="editEmpresaModalLabel">
            Editar Empresa
          </h5>
          <button
            type="button"
            class="btn-close"
            @click="emit('close')"
            aria-label="Cerrar"
          ></button>
        </div>
        <div class="modal-body p-4">
          <form @submit.prevent="submitForm" class="row g-3">
            <div class="col-md-6">
              <label class="form-label">Nombre *</label>
              <input v-model="form.nombre" type="text" class="form-control" required />
            </div>

            <div class="col-md-6">
              <label class="form-label">Razón Social</label>
              <input v-model="form.razonSocial" type="text" class="form-control" />
            </div>

            <div class="col-md-6">
              <label class="form-label">CUIT</label>
              <input v-model="form.cuit" type="text" class="form-control" />
            </div>

            <div class="col-md-6">
              <label class="form-label">Teléfono</label>
              <input v-model="form.telefono" type="text" class="form-control" />
            </div>

            <div class="col-md-6">
              <label class="form-label">Email</label>
              <input v-model="form.email" type="email" class="form-control" />
            </div>

            <div class="col-md-6">
              <label class="form-label">Dirección</label>
              <input v-model="form.direccion" type="text" class="form-control" />
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
                <span>Guardar Cambios</span>
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
import type { EditEmpresaDto } from '@/Types/Empresa/EditEmpresaDto'
import type { EmpresaDto } from '@/Types/ViewModels/ViewModels'
import { ref, watch } from 'vue'
import { POSITION, useToast } from 'vue-toastification'

const props = defineProps<{
  show: boolean
  empresa: EmpresaDto
}>()

const emit = defineEmits<{
  (e: 'close'): void
  (e: 'success'): void
}>()

const toast = useToast()
const isSubmitting = ref(false)

const form = ref<EditEmpresaDto>({
  nombre: '',
  razonSocial: '',
  cuit: '',
  direccion: '',
  telefono: '',
  email: '',
})

watch(
  () => props.empresa,
  (newVal) => {
    if (newVal) {
      form.value = {
        nombre: newVal.nombre_Empresa,
        razonSocial: newVal.razonSocial || null,
        cuit: newVal.cuit || null,
        direccion: newVal.direccion_Empresa || null,
        telefono: newVal.telefono_Empresa || null,
        email: newVal.email_Empresa || null,
      }
    }
  },
  { immediate: true },
)

const submitForm = async () => {
  if (!form.value.nombre) {
    toast.error('El nombre es obligatorio', { position: POSITION.BOTTOM_CENTER })
    return
  }

  isSubmitting.value = true
  try {
    const response = await ApiService.EditarInfoEmpresa(props.empresa.id, form.value)
    if (response.isSuccess) {
      toast.success('Empresa actualizada con éxito')
      emit('success')
      emit('close')
    } else {
      toast.error(response.error?.message || 'Error al actualizar empresa', {
        position: POSITION.BOTTOM_CENTER,
      })
    }
  } catch (e) {
    toast.error('Ocurrió un error inesperado', { position: POSITION.BOTTOM_CENTER })
  } finally {
    isSubmitting.value = false
  }
}
</script>

<style scoped></style>
