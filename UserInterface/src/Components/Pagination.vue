<script lang="ts" setup>
import '@/Styles/Pagination.css';
import type { ConvenioQueryObject } from '@/Types/Api.Interface';


const props = defineProps<{ query: ConvenioQueryObject }>()

const emit = defineEmits(["paginaCambiada"]) // un solo evento

function irPagina(nuevaPagina: number) {
  props.query.PaginaActual = 1;
  emit("paginaCambiada", nuevaPagina)
}
</script>

<template>
  <div class="pagination-container">
    <button @click="irPagina(1)">1</button>
    <button @click="irPagina(2)">2</button>
    <button @click="irPagina(3)">3</button>
    <button @click="irPagina(4)">4</button>
    <button @click="irPagina(5)">5</button>

    <button @click="irPagina(props.query.PaginaActual! - 1)" :disabled="props.query.PaginaActual! <= 1">
      Prev
    </button>

    <button @click="irPagina(props.query.PaginaActual! + 1)">
      Next
    </button>
  </div>
</template>
