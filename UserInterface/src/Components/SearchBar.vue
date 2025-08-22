<template>
  <div class="bg-white p-4 rounded-lg shadow-md">
    <div class="flex items-center gap-4">
      <input
        v-model="Busqueda"
        type="text"
        placeholder="Escribí tu búsqueda..."
        class="flex-grow p-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
      />
      <div class="flex items-center gap-2">
        <button
          @click="PrametroBusqueda = 'titulo'"
          :class="[
            'px-4 py-2 rounded-lg transition-colors',
            PrametroBusqueda === 'titulo' ? 'bg-blue-600 text-white shadow' : 'bg-gray-200 text-gray-700 hover:bg-gray-300'
          ]"
        >
          Título
        </button>
        <button
          @click="PrametroBusqueda = 'empresa'"
          :class="[
            'px-4 py-2 rounded-lg transition-colors',
            PrametroBusqueda === 'empresa' ? 'bg-blue-600 text-white shadow' : 'bg-gray-200 text-gray-700 hover:bg-gray-300'
          ]"
        >
          Empresa
        </button>
      </div>
    </div>
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
