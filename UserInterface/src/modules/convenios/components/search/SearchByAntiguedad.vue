<script setup lang="ts">
import type { IByAntiguedadDtoParams, IConvenioQueryObject } from '@/Types/Filters'
import { computed, ref } from 'vue'

const props = defineProps<{
  QueryObject: IConvenioQueryObject
  typeOfConvenio: 'marco' | 'especifico' | 'ambos' | ''
}>()

const emit = defineEmits<{
  (e: 'SearchDone'): void
}>()

const esAscendente = ref<boolean>(true)

const objetoFiltroListo = computed<IByAntiguedadDtoParams>(() => {
  return {
    ascendente: esAscendente.value,
    convenioType: props.typeOfConvenio,
  }
})

const handleSort = () => {
  props.QueryObject.ByAntiguedadDto = objetoFiltroListo.value

  emit('SearchDone')
}
</script>

<template>
  <div
    class="card p-3 shadow-sm rounded-0 border-0 border-start border-4 border-primary custom-card-width"
  >
    <div class="row g-3 align-items-center">
      <div class="col-12">
        <h6 class="mb-0 card-title text-primary fw-bold">Ordenar por Antigüedad</h6>
      </div>

      <div class="col-auto">
        <div class="form-check form-switch d-flex align-items-center">
          <input
            class="form-check-input"
            type="checkbox"
            role="switch"
            id="switchOrdenAntiguedad"
            v-model="esAscendente"
          />
          <label class="form-check-label ms-2" for="switchOrdenAntiguedad">
            Orden:
            <strong>{{ esAscendente ? 'Más antiguos primero' : 'Más nuevos primero' }}</strong>
          </label>
        </div>
      </div>

      <div class="col-auto">
        <button class="btn btn-sm btn-primary" @click="handleSort">
          <i class="bi" :class="esAscendente ? 'bi-sort-up' : 'bi-sort-down'"></i>
          Ordenar
        </button>
      </div>
    </div>
  </div>
</template>

<style>
.custom-card-width {
  max-width: fit-content;
}

/* Ajustes para que el switch se vea más robusto */
.form-check-input {
  width: 3em;
  height: 1.5em;
  cursor: pointer;
}
</style>
