<script setup lang="ts">
import type { ICountFirmadosByMesParams, IConvenioQueryObject } from '@/Types/Filters'
import { computed, ref } from 'vue'

const props = defineProps<{
  QueryObject: IConvenioQueryObject
  typeOfConvenio: 'marco' | 'especifico' | ''
}>()

const emit = defineEmits<{
  (e: 'SearchDone'): void
}>()

const mesSeleccionado = ref<number>(1)
const añoSeleccionado = ref<number>(new Date().getFullYear())
const mostrarAlerta = ref<boolean>(false)

const meses = [
  { value: 1, label: 'Enero' },
  { value: 2, label: 'Febrero' },
  { value: 3, label: 'Marzo' },
  { value: 4, label: 'Abril' },
  { value: 5, label: 'Mayo' },
  { value: 6, label: 'Junio' },
  { value: 7, label: 'Julio' },
  { value: 8, label: 'Agosto' },
  { value: 9, label: 'Septiembre' },
  { value: 10, label: 'Octubre' },
  { value: 11, label: 'Noviembre' },
  { value: 12, label: 'Diciembre' },
]

const objetoFiltroListo = computed<ICountFirmadosByMesParams | null>(() => {
  if (!mesSeleccionado.value || !añoSeleccionado.value) {
    return null
  }
  return {
    month: mesSeleccionado.value,
    year: añoSeleccionado.value,
    convenioType: props.typeOfConvenio,
  }
})

const handleBuscar = () => {
  if (!mesSeleccionado.value || !añoSeleccionado.value) {
    mostrarAlerta.value = true
    return
  }

  mostrarAlerta.value = false
  props.QueryObject.CountFirmadosByMesDto = objetoFiltroListo.value
  emit('SearchDone')
}

const handleInputChange = () => {
  if (mesSeleccionado.value && añoSeleccionado.value) {
    mostrarAlerta.value = false
  }
}
</script>

<template>
  <div
    class="card p-3 shadow-sm rounded-0 border-0 border-start border-4 border-primary custom-card-width"
  >
    <div class="row g-2 align-items-center">
      <div class="col-12">
        <h6 class="mb-0 card-title text-primary fw-bold">
          <i class="bi bi-calculator me-2"></i>Cantidad de Convenios Firmados por Mes
        </h6>
      </div>

      <div class="col-auto">
        <label for="selectMesCount" class="form-label visually-hidden">Mes</label>
        <select
          class="form-select form-select-sm"
          id="selectMesCount"
          v-model="mesSeleccionado"
          @change="handleInputChange"
          aria-label="Seleccionar mes"
          :class="{ 'is-invalid': mostrarAlerta }"
        >
          <option v-for="mes in meses" :key="mes.value" :value="mes.value">
            {{ mes.label }}
          </option>
        </select>
      </div>

      <div class="col-auto">
        <label for="inputAñoCount" class="form-label visually-hidden">Año</label>
        <input
          type="number"
          class="form-control form-control-sm"
          id="inputAñoCount"
          v-model.number="añoSeleccionado"
          @input="handleInputChange"
          aria-label="Ingresar año"
          :class="{ 'is-invalid': mostrarAlerta }"
          min="1900"
          :max="new Date().getFullYear() + 10"
          placeholder="Año"
        />
        <div class="invalid-feedback">Por favor, seleccione un mes y año válidos.</div>
      </div>

      <div class="col-auto">
        <button class="btn btn-sm btn-primary" @click="handleBuscar">
          <i class="bi bi-search"></i>
          Buscar
        </button>
      </div>
    </div>
  </div>
</template>

<style>
.custom-card-width {
  max-width: fit-content;
}
</style>
