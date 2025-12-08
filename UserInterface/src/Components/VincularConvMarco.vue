<template>
  <div class="d-flex flex-column">
    <label for="convenioMarcoSelect" class="form-label">
      Seleccione el convenio marco a vincular
    </label>
    <select
      id="convenioMarcoSelect"
      v-model="selectedId"
      @change="handleChange"
      class="form-select"
    >
      <option value="" disabled>Seleccione un convenio marco...</option>
      <option v-for="convenio in conveniosMarcos" :key="convenio.id" :value="convenio.id">
        {{ convenio.titulo }}
      </option>
    </select>

    <div v-if="isLoading" class="text-center py-2 mt-2">
      <div class="spinner-border spinner-border-sm text-primary" role="status">
        <span class="visually-hidden">Cargando...</span>
      </div>
    </div>

    <div v-if="errorMensaje" class="alert alert-danger mt-2" role="alert">
      {{ errorMensaje }}
    </div>

    <div
      v-if="!isLoading && conveniosMarcos.length === 0"
      class="alert alert-info mt-2"
      role="alert"
    >
      No hay convenios marcos disponibles
    </div>

    <div v-if="selectedId" class="alert alert-success mt-2" role="alert">
      <i class="bi bi-check-circle me-2"></i>
      Convenio marco seleccionado
    </div>
  </div>
</template>

<script setup lang="ts">
import ApiService from '@/Services/ApiService'
import type { ComboBoxConvenioMarcoDto } from '@/Types/ConvenioMarco/ComboBoxConvenioMarcoDto'
import { ref, onMounted } from 'vue'

interface RequestWithConvenioMarco {
  idConvenioMarco?: number | null
}

const props = defineProps<{
  request: RequestWithConvenioMarco
}>()

const conveniosMarcos = ref<ComboBoxConvenioMarcoDto[]>([])
const isLoading = ref(false)
const errorMensaje = ref('')
const selectedId = ref<number | null>(props.request.idConvenioMarco || null)

const handleChange = () => {
  if (selectedId.value) {
    props.request.idConvenioMarco = selectedId.value
  } else {
    props.request.idConvenioMarco = null
  }
}

const fetchConveniosMarcos = async () => {
  isLoading.value = true
  errorMensaje.value = ''

  const result = await ApiService.GetAllConveniosMarcos()

  isLoading.value = false

  if (result.isSuccess) {
    conveniosMarcos.value = result.value
  } else {
    errorMensaje.value = result.error?.message || 'Error al cargar convenios marcos'
  }
}

onMounted(() => {
  fetchConveniosMarcos()
})
</script>
