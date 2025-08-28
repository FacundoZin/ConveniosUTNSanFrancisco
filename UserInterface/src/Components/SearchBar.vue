<template>
  <div class="searchbar-container">
    <input v-model="Busqueda" type="text" placeholder="Escribí tu búsqueda..." class="search-input" />

    <button @click="PrametroBusqueda = 'titulo'" :class="['toggle-btn', PrametroBusqueda === 'titulo' ? 'active' : '']">
      Título
    </button>

    <button @click="PrametroBusqueda = 'empresa'"
      :class="['toggle-btn', PrametroBusqueda === 'empresa' ? 'active' : '']">
      Empresa
    </button>
  </div>
</template>

<script setup>
import '@/Styles/SearchBar.css'
import { ref, watch } from 'vue'

const emit = defineEmits(['update'])

const Busqueda = ref('')
const PrametroBusqueda = ref('titulo')

// Cada vez que term o tipo cambian, se emite automáticamente
watch([Busqueda, PrametroBusqueda], () => {
  emit('update', { Busqueda: Busqueda.value, Parametro: PrametroBusqueda.value })
})
</script>
