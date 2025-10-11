<script setup lang="ts">
import { ref, computed } from 'vue';
import type { IByTituloParams, IConvenioQueryObject } from '@/Types/Filters'; 

const props = defineProps<{
  QueryObject: IConvenioQueryObject,
  typeOfConvenio: 'marco' | 'especifico' | '',
}>();

const emit = defineEmits<{
  (e: 'SearchDone'): void; 
}>();

const Titulo = ref<string>('');
const mostrarAlerta = ref<boolean>(false); 

const objetoFiltroListo = computed<IByTituloParams | null>(() => {
  const valorLimpio = Titulo.value.trim();
  
  if (!valorLimpio) {
    return null; 
  }
  return {
    Title: valorLimpio,
    'ConvenioType.Type': props.typeOfConvenio,
  };
});


const handleSearch = () => {
  if (!Titulo.value.trim()) {
    mostrarAlerta.value = true;
    return;
  }
  
  mostrarAlerta.value = false;

  props.QueryObject.ByTitulo = objetoFiltroListo.value;

  emit('SearchDone');
};

const handleInputChange = () => {
  if (Titulo.value.trim()) {
    mostrarAlerta.value = false;
  }
}
</script>

<template>
  <div class="card p-3 shadow-sm rounded-0 border-0 border-start border-4 border-primary custom-card-width">
    <div class="row g-2 align-items-center">
      
      <div class="col-12">
        <h6 class="mb-0 card-title text-primary fw-bold">Filtrar por Titulo de convenio</h6>
      </div>
      
      <div class="col-auto">
        <label for="inputNumeroResolucion" class="form-label visually-hidden">Titulo de Convenio</label>
        <input 
          type="text" 
          class="form-control form-control-sm" 
          id="inputNumeroResolucion" 
          v-model="Titulo"
          @input="handleInputChange"
          placeholder="Ej: 1234/2023"
          aria-label="Ingresar número de resolución"
          :class="{'is-invalid': mostrarAlerta}" 
        >
        <div class="invalid-feedback">
            Por favor, ingrese un titulo.
        </div>
      </div>

      <div class="col-auto">
        <button 
          class="btn btn-sm btn-primary"
          @click="handleSearch"
          :disabled="false"
        >
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