<script setup lang="ts">
import type { ICountFirmadosByRangoParams, IConvenioQueryObject } from '@/Types/Filters'
import { computed, ref } from 'vue'

const props = defineProps<{
  QueryObject: IConvenioQueryObject
  typeOfConvenio: 'marco' | 'especifico' | ''
}>()

const emit = defineEmits<{
  (e: 'SearchDone'): void
}>()

const fechaDesde = ref<string>('')
const fechaHasta = ref<string>('')
const mostrarAlerta = ref<boolean>(false)

const objetoFiltroListo = computed<ICountFirmadosByRangoParams | null>(() => {
  if (!fechaDesde.value || !fechaHasta.value) {
    return null
  }
  return {
    desde: fechaDesde.value,
    hasta: fechaHasta.value,
    convenioType: props.typeOfConvenio,
  }
})

const handleBuscar = () => {
  if (!fechaDesde.value || !fechaHasta.value) {
    mostrarAlerta.value = true
    return
  }

  // Validar que la fecha desde sea anterior a la fecha hasta
  if (new Date(fechaDesde.value) > new Date(fechaHasta.value)) {
    mostrarAlerta.value = true
    return
  }

  mostrarAlerta.value = false
  props.QueryObject.countFirmadosByRangoDto = objetoFiltroListo.value
  emit('SearchDone')
}

const handleFechaChange = () => {
  if (fechaDesde.value && fechaHasta.value) {
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
          <i class="bi bi-calculator me-2"></i>Cantidad de Convenios Firmados por Rango
        </h6>
      </div>

      <div class="col-auto">
        <label for="inputFechaDesdeCount" class="form-label visually-hidden">Fecha Desde</label>
        <input
          type="date"
          class="form-control form-control-sm"
          id="inputFechaDesdeCount"
          v-model="fechaDesde"
          @input="handleFechaChange"
          aria-label="Seleccionar fecha desde"
          :class="{ 'is-invalid': mostrarAlerta }"
          placeholder="Desde"
        />
      </div>

      <div class="col-auto d-flex align-items-center">
        <span class="text-muted">-</span>
      </div>

      <div class="col-auto">
        <label for="inputFechaHastaCount" class="form-label visually-hidden">Fecha Hasta</label>
        <input
          type="date"
          class="form-control form-control-sm"
          id="inputFechaHastaCount"
          v-model="fechaHasta"
          @input="handleFechaChange"
          aria-label="Seleccionar fecha hasta"
          :class="{ 'is-invalid': mostrarAlerta }"
          placeholder="Hasta"
        />
        <div class="invalid-feedback">Por favor, seleccione un rango de fechas válido.</div>
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
