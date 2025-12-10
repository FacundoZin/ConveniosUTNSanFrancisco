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

const carreraSeleccionada = ref<string | null>(null)

const carreras: string[] = [
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
  if (!carreraSeleccionada.value) {
    return null
  }
  return {
    nombreCarrera: carreraSeleccionada.value,
    conveniotype: props.typeOfConvenio,
  }
})

const handleSelectChange = () => {
  if (carreraSeleccionada.value === null) {
    if (props.QueryObject.ByCarrera) {
      props.QueryObject.ByCarrera = null
    }
  }
}

const handleSearch = () => {
  if (carreraSeleccionada.value === null) {
    return
  }

  props.QueryObject.ByCarrera = objetoFiltroListo.value

  emit('SearchDone')
}
</script>

<template>
  <div
    class="card p-3 shadow-sm rounded-0 border-0 border-start border-4 border-primary custom-card-width"
  >
    <div class="row g-2 align-items-center">
      <div class="col-12">
        <h6 class="mb-0 card-title text-primary fw-bold">Filtrar por Carrera</h6>
      </div>

      <div class="col-auto">
        <select
          class="form-select form-select-sm"
          v-model="carreraSeleccionada"
          @change="handleSelectChange"
          aria-label="Seleccionar carrera"
        >
          <option :value="null" disabled selected>Seleccione una carrera</option>
          <option v-for="carrera in carreras" :key="carrera" :value="carrera">
            {{ carrera }}
          </option>
        </select>
      </div>

      <div class="col-auto">
        <button
          class="btn btn-sm btn-primary"
          @click="handleSearch"
          :disabled="carreraSeleccionada === null"
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
