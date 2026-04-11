<script setup lang="ts">
import { EstadoConvenio } from '@/Types/Enums/Enums'
import type { IConvenioQueryObject } from '@/Types/Filters' // Asume que esta interfaz existe
import { computed, ref } from 'vue'

const props = defineProps<{
  QueryObject: IConvenioQueryObject
  typeOfConvenio: 'marco' | 'especifico' | 'ambos' | ''
}>()

const emit = defineEmits<{
  (e: 'SearchDone'): void
}>()

const estadoSeleccionado = ref<EstadoConvenio | null>(null)

const estados = computed(() => {
  return Object.keys(EstadoConvenio)
    .filter((key) => isNaN(Number(key)))
    .map((key) => ({
      value: EstadoConvenio[key as keyof typeof EstadoConvenio],
      label: key,
    }))
})

const handleSelectChange = () => {
  if (estadoSeleccionado.value === null) {
    if (props.QueryObject.ByEstado) {
      props.QueryObject.ByEstado = null
    }
  }
}

const handleSearch = () => {
  if (estadoSeleccionado.value === null) {
    return
  }

  props.QueryObject.ByEstado = {
    Estado: estadoSeleccionado.value,
    convenioType: props.typeOfConvenio,
  }

  emit('SearchDone')
}
</script>

<template>
  <div
    class="card p-3 shadow-sm rounded-0 border-0 border-start border-4 border-primary custom-card-width"
  >
    <div class="row g-2 align-items-center">
      <div class="col-12">
        <h6 class="mb-0 card-title text-primary fw-bold">Filtrar por Estado</h6>
      </div>

      <div class="col-auto">
        <select
          class="form-select form-select-sm"
          v-model="estadoSeleccionado"
          @change="handleSelectChange"
          aria-label="Seleccionar estado de convenio"
        >
          <option :value="null" disabled selected>Seleccione un estado</option>
          <option v-for="estado in estados" :key="estado.value" :value="estado.value">
            {{ estado.label }}
          </option>
        </select>
      </div>

      <div class="col-auto">
        <button
          class="btn btn-sm btn-primary"
          @click="handleSearch"
          :disabled="estadoSeleccionado === null"
        >
          <i class="bi bi-search"></i>
          Buscar
        </button>
      </div>
    </div>
  </div>
</template>

<style scoped>
.custom-card-width {
  max-width: fit-content;
}
</style>
