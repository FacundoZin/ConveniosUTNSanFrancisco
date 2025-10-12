<script setup lang="ts">
import type { IByNumeroResolucionParams, IConvenioQueryObject } from '@/Types/Filters';
import { computed, ref } from 'vue';

const props = defineProps<{
  QueryObject: IConvenioQueryObject,
  typeOfConvenio: 'marco' | 'especifico' | '',
}>();

const emit = defineEmits<{
  (e: 'SearchDone'): void;
}>();

const numeroResolucion = ref<string>('');
const mostrarAlerta = ref<boolean>(false);

const objetoFiltroListo = computed<IByNumeroResolucionParams | null>(() => {
  const valorLimpio = numeroResolucion.value.trim();

  if (!valorLimpio) {
    return null;
  }
  return {
    NumeroResolucion: valorLimpio,
    convenioType: props.typeOfConvenio,
  };
});


const handleSearch = () => {
  if (!numeroResolucion.value.trim()) {
    mostrarAlerta.value = true;
    return;
  }

  mostrarAlerta.value = false;

  props.QueryObject.ByNumeroResolucion = objetoFiltroListo.value;

  emit('SearchDone');
};

const handleInputChange = () => {
  if (numeroResolucion.value.trim()) {
    mostrarAlerta.value = false;
  }
}
</script>

<template>
  <div class="card p-3 shadow-sm rounded-0 border-0 border-start border-4 border-primary custom-card-width">
    <div class="row g-2 align-items-center">

      <div class="col-12">
        <h6 class="mb-0 card-title text-primary fw-bold">Filtrar por Número de Resolución</h6>
      </div>

      <div class="col-auto">
        <label for="inputNumeroResolucion" class="form-label visually-hidden">Número de Resolución</label>
        <input type="text" class="form-control form-control-sm" id="inputNumeroResolucion" v-model="numeroResolucion"
          @input="handleInputChange" placeholder="Ej: 1234/2023" aria-label="Ingresar número de resolución"
          :class="{ 'is-invalid': mostrarAlerta }">
        <div class="invalid-feedback">
          Por favor, ingrese un número de resolución.
        </div>
      </div>

      <div class="col-auto">
        <button class="btn btn-sm btn-primary" @click="handleSearch" :disabled="false">
          <i class="bi bi-search"></i>
          Buscar
        </button>
      </div>

    </div>
  </div>
</template>

<style>
/* Estilo mínimo para que el componente sea un "rectángulo fino" que se ajuste a su contenido */
.custom-card-width {
  max-width: fit-content;
}
</style>