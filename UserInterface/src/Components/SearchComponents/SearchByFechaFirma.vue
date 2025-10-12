<script setup lang="ts">
import type { IByFechaFirmaParams, IConvenioQueryObject } from '@/Types/Filters';
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


const objetoFiltroListo = computed<IByFechaFirmaParams | null>(() => {
  if (!fechaSeleccionada.value) {
    return null;
  }
  return {
    FechaInicio: fechaSeleccionada.value,
    convenioType: props.typeOfConvenio,
  };
});


const handleBuscar = () => {
  if (!fechaSeleccionada.value) {
    mostrarAlerta.value = true;
    return;
  }

  mostrarAlerta.value = false;

  props.QueryObject.ByFechaFirma = objetoFiltroListo.value;

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
        <h6 class="mb-0 card-title text-primary fw-bold">Filtrar por Fecha de Firma de Convenio</h6>
      </div>

      <div class="col-auto">
        <label for="inputFechaFirma" class="form-label visually-hidden">Fecha de Firma</label>
        <input type="date" class="form-control form-control-sm" id="inputFechaFirma" v-model="fechaSeleccionada"
          @input="handleFechaChange" aria-label="Seleccionar fecha de firma" :class="{ 'is-invalid': mostrarAlerta }">
        <div class="invalid-feedback">
          Por favor, seleccione una fecha.
        </div>
      </div>

      <div class="col-auto">
        <button class="btn btn-sm btn-primary" @click="handleBuscar" :disabled="false">
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