<script setup lang="ts">
import type { IConvenioQueryObject } from '@/Types/Filters'
import { computed, ref } from 'vue'

const props = defineProps<{
  QueryObject: IConvenioQueryObject
  typeOfConvenio: 'marco' | 'especifico' | 'ambos' | ''
}>()

const emit = defineEmits<{
  (e: 'SearchDone'): void
}>()

const areaSeleccionada = ref<string | null>(null)

const areas: string[] = [
  'Ingeniería Química',
  'Ingeniería en Sistemas',
  'Ingeniería Electrónica',
  'Ingeniería Electromecánica',
  'Tecnicatura en Programación',
  'Materias Basicas',
  'SEU',
  'Vinculación Tecnológica',
]

const objetoFiltroListo = computed(() => {
  if (!areaSeleccionada.value) {
    return null
  }
  return {
    nombreArea: areaSeleccionada.value,
    conveniotype: props.typeOfConvenio,
  }
})

const handleSelectChange = () => {
  if (areaSeleccionada.value === null) {
    if (props.QueryObject.ByArea) {
      props.QueryObject.ByArea = null
    }
  }
}

const handleSearch = () => {
  if (areaSeleccionada.value === null) {
    return
  }

  props.QueryObject.ByArea = objetoFiltroListo.value

  emit('SearchDone')
}
</script>

<template>
  <div
    class="card p-3 shadow-sm rounded-0 border-0 border-start border-4 border-primary custom-card-width"
  >
    <div class="row g-2 align-items-center">
      <div class="col-12">
        <h6 class="mb-0 card-title text-primary fw-bold">Filtrar por Área</h6>
      </div>

      <div class="col-auto">
        <select
          class="form-select form-select-sm"
          v-model="areaSeleccionada"
          @change="handleSelectChange"
          aria-label="Seleccionar área"
        >
          <option :value="null" disabled selected>Seleccione un área</option>
          <option v-for="area in areas" :key="area" :value="area">
            {{ area }}
          </option>
        </select>
      </div>

      <div class="col-auto">
        <button
          class="btn btn-sm btn-primary"
          @click="handleSearch"
          :disabled="areaSeleccionada === null"
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
