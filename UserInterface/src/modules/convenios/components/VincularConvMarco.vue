<template>
  <div class="d-flex flex-column">
    <label class="form-label fw-semibold">
      Seleccione el convenio marco a vincular
    </label>
    <SearchableSelect
      v-model="selectedId"
      :options="convenioMarcoOptions"
      placeholder="Buscar o seleccionar convenio marco..."
      :disabled="isLoading"
    />

    <div v-if="isLoading" class="text-center py-2 mt-2">
      <div class="spinner-border spinner-border-sm text-primary" role="status">
        <span class="visually-hidden">Cargando...</span>
      </div>
    </div>

    <div v-if="errorMensaje" class="alert alert-danger mt-2" role="alert">
      {{ errorMensaje }}
    </div>

    <div v-if="!isLoading && conveniosMarcos.length === 0" class="alert alert-info mt-2" role="alert">
      No hay convenios marcos disponibles
    </div>

    <div v-if="selectedId" class="alert alert-success mt-2 py-2 small" role="alert">
      <i class="bi bi-check-circle me-2"></i>
      Convenio marco seleccionado
    </div>
  </div>
</template>

<script setup lang="ts">
import ConvenioService from '@/modules/convenios/services/ConvenioService'
import SearchableSelect from '@/modules/shared/components/SearchableSelect.vue'
import type { ComboBoxConvenioMarcoDto } from '@/Types/ConvenioMarco/ComboBoxConvenioMarcoDto'
import { computed, onMounted, ref, watch } from 'vue'

interface RequestWithConvenioMarco {
  idConvenioMarco?: number | null
  idMarcoVinculado?: number | null
}

const props = defineProps<{
  request: RequestWithConvenioMarco
}>()

const conveniosMarcos = ref<ComboBoxConvenioMarcoDto[]>([])
const isLoading = ref(false)
const errorMensaje = ref('')
const selectedId = ref<number | null>(
  props.request.idConvenioMarco ?? props.request.idMarcoVinculado ?? null,
)

const convenioMarcoOptions = computed(() => {
  return conveniosMarcos.value.map((cm) => ({
    id: cm.id,
    label: cm.titulo,
  }))
})

watch(selectedId, (newVal) => {
  const val = (newVal as number) || null

  if ('idConvenioMarco' in props.request) {
    props.request.idConvenioMarco = val
  }

  if ('idMarcoVinculado' in props.request) {
    props.request.idMarcoVinculado = val
  }
})

watch(
  () => props.request.idConvenioMarco ?? props.request.idMarcoVinculado,
  (newVal) => {
    if (newVal != null && newVal !== selectedId.value) {
      selectedId.value = newVal
    }
  },
)

const fetchConveniosMarcos = async () => {
  isLoading.value = true
  errorMensaje.value = ''

  const result = await ConvenioService.GetAllConveniosMarcos()

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
