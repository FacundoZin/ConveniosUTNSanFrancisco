<script lang="ts" setup>
import type { ConvenioQueryObject } from '@/Types/Api.Interface';


const props = defineProps<{query: ConvenioQueryObject }>()

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

    <button @click="irPagina(props.query.PaginaActual - 1)" :disabled="props.query.PaginaActual <= 1">
      Prev
    </button>

    <button @click="irPagina(props.query.PaginaActual + 1)">
      Next
    </button>
  </div>
</template>

<style scoped>
.pagination-container {
  display: flex;
  gap: 5px;
  flex-wrap: wrap;
  margin-top: 15px;
}

.pagination-container button {
  padding: 5px 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
  background-color: white;
  cursor: pointer;
  transition: background 0.2s;
}

.pagination-container button:hover {
  background-color: #f0f0f0;
}

.pagination-container button:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}
</style>