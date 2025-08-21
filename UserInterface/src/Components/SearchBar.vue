<template>
  <div class="w-full bg-black p-3 flex items-center justify-center gap-3">
    <!-- Input -->
    <input
      v-model="Busqueda"
      type="text"
      placeholder="Escribí tu búsqueda..."
      class="p-2 rounded w-1/2 text-black"
    />

    <!-- Botones toggle -->
    <button
      @click="PrametroBusqueda = 'titulo'"
      :class="[
        'px-4 py-2 rounded',
        PrametroBusqueda === 'titulo' ? 'bg-blue-500 text-white' : 'bg-white text-black'
      ]"
    >
      Título
    </button>

    <button
      @click="PrametroBusqueda = 'empresa'"
      :class="[
        'px-4 py-2 rounded',
        PrametroBusqueda === 'empresa' ? 'bg-blue-500 text-white' : 'bg-white text-black'
      ]"
    >
      Empresa
    </button>
  </div>
</template>

<script setup>
import { ref, watch } from 'vue'

const emit = defineEmits(['update'])

const Busqueda = ref('')
const PrametroBusqueda = ref('titulo')

// Cada vez que term o tipo cambian, se emite automáticamente
watch([Busqueda, PrametroBusqueda], () => {
  emit('update', { Busqueda: Busqueda.value, Parametro: PrametroBusqueda.value })
})
</script>
