<script setup lang="ts">
import type { IByFechaFinParams, IConvenioQueryObject } from '@/Types/Filters';
import { computed, ref } from 'vue';


const props = defineProps<{
  QueryObject: IConvenioQueryObject,
  typeOfConvenio: 'marco' | 'especifico' | '',
}>();

const emit = defineEmits<{
  (e: 'SearchDone'): void;
}>();

const fechaSeleccionada = ref<string>('');
const mostrarAlerta = ref<boolean>(false);

const objetoFiltroListo = computed<IByFechaFinParams | null>(() => {
  if (!fechaSeleccionada.value) {
    return null;
  }
  return {
    FechaFin: fechaSeleccionada.value,
    convenioType: props.typeOfConvenio,
  };
});

const handleBuscar = () => {
  if (!fechaSeleccionada.value) {
    mostrarAlerta.value = true;
    return;
  }

  mostrarAlerta.value = false;

  props.QueryObject.ByFechaFin = objetoFiltroListo.value;

  emit('SearchDone');
};

const handleFechaChange = () => {
  if (fechaSeleccionada.value) {
    mostrarAlerta.value = false;
  }
}
</script>

<template>
  <div class="card p-3 shadow-sm rounded-0 border-0 border-start border-4 border-primary custom-card-width">
    <div class="row g-2 align-items-center">

      <div class="col-12">
        <h6 class="mb-0 card-title text-primary fw-bold">Filtrar por Fecha de Fin de Convenio</h6>
      </div>

      <div class="col-auto">
        <label for="inputFechaFin" class="form-label visually-hidden">Fecha de Fin</label>
        <input type="date" class="form-control form-control-sm" id="inputFechaFin" v-model="fechaSeleccionada"
          @input="handleFechaChange" aria-label="Seleccionar fecha de fin" :class="{ 'is-invalid': mostrarAlerta }">
      </div>

      <div class="col-auto">
        <button class="btn btn-sm btn-primary" @click="handleBuscar" :disabled="false">
          <i class="bi bi-search"></i>
          Buscar
        </button>
      </div>

      <div v-if="mostrarAlerta" class="col-12 mt-2">
        <div class="alert alert-warning p-2 mb-0 small" role="alert">
          <strong>Atención:</strong> Por favor, seleccione una fecha.
        </div>
      </div>

    </div>
  </div>
</template>

<style>
.custom-card-width {
  max-width: fit-content;
}
</style>