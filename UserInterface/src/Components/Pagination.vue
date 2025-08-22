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
  <div class="flex items-center gap-2">
    <button @click="irPagina(1)" class="px-3 py-1 border rounded">1</button>
    <button @click="irPagina(2)" class="px-3 py-1 border rounded">2</button>
    <button @click="irPagina(3)" class="px-3 py-1 border rounded">3</button>
    <button @click="irPagina(4)" class="px-3 py-1 border rounded">4</button>
    <button @click="irPagina(5)" class="px-3 py-1 border rounded">5</button>

    <button @click="irPagina(props.query.PaginaActual - 1)" :disabled="props.query.PaginaActual <= 1">
      Prev
    </button>

    <button @click="irPagina(props.query.PaginaActual + 1)">
      Next
    </button>
  </div>
</template>
