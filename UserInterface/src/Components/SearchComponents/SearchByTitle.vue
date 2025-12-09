<script setup lang="ts">
import type { IByTituloParams, IConvenioQueryObject } from '@/Types/Filters'
import { computed, ref } from 'vue'

const props = defineProps<{
  QueryObject: IConvenioQueryObject
  typeOfConvenio: 'marco' | 'especifico' | 'ambos' | ''
}>()

const emit = defineEmits<{
  (e: 'SearchDone'): void
}>()

const Titulo = ref<string>('')
const mostrarAlerta = ref<boolean>(false)

const objetoFiltroListo = computed<IByTituloParams | null>(() => {
  const valorLimpio = Titulo.value.trim()

  if (!valorLimpio) {
    return null
  }
  return {
    Title: valorLimpio,
    convenioType: props.typeOfConvenio,
  }
})

const handleSearch = () => {
  if (!Titulo.value.trim()) {
    mostrarAlerta.value = true
    return
  }

  mostrarAlerta.value = false

  props.QueryObject.ByTitulo = objetoFiltroListo.value

  emit('SearchDone')
}

const handleInputChange = () => {
  if (Titulo.value.trim()) {
    mostrarAlerta.value = false
  }
}
</script>

<template>
  <div class="card p-3 shadow-sm rounded-0 border-0 border-start border-4 border-primary">
    <div class="row g-2 align-items-center">
      <div class="col-12 col-md-auto">
        <h6 class="mb-0 card-title text-primary fw-bold">
          <i class="bi bi-search me-2"></i>Filtrar por Título
        </h6>
      </div>

      <div class="col-12 col-md">
        <label for="inputNumeroResolucion" class="form-label visually-hidden"
          >Titulo de Convenio</label
        >
        <input
          type="text"
          class="form-control form-control-sm"
          id="inputNumeroResolucion"
          v-model="Titulo"
          @input="handleInputChange"
          placeholder="Ingrese el título..."
          aria-label="Ingresar título de convenio"
          :class="{ 'is-invalid': mostrarAlerta }"
        />
        <div class="invalid-feedback">Por favor, ingrese un titulo.</div>
      </div>

      <div class="col-12 col-md-auto">
        <button class="btn btn-sm btn-primary w-100" @click="handleSearch" :disabled="false">
          Buscar
        </button>
      </div>
    </div>
  </div>
</template>
