<template>
  <div class="searchbar-container">
    <input
      v-model="Busqueda"
      type="text"
      placeholder="Escribí tu búsqueda..."
      class="search-input"
    />

    <button
      @click="PrametroBusqueda = 'titulo'"
      :class="['toggle-btn', PrametroBusqueda === 'titulo' ? 'active' : '']"
    >
      Título
    </button>

    <button
      @click="PrametroBusqueda = 'empresa'"
      :class="['toggle-btn', PrametroBusqueda === 'empresa' ? 'active' : '']"
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

<style scoped>
.searchbar-container {
  display: flex;
  gap: 10px;
  flex-wrap: wrap;
}

.search-input {
  padding: 8px 12px;
  border-radius: 6px;
  border: 1px solid #ccc;
  flex: 1;
  min-width: 200px;
}

.toggle-btn {
  padding: 8px 15px;
  border-radius: 6px;
  border: none;
  cursor: pointer;
  background-color: #e0e0e0;
  color: #000;
  transition: background 0.3s;
}

.toggle-btn.active {
  background-color: #00a1e4; /* celeste UTN */
  color: white;
}

.toggle-btn:hover {
  background-color: #0091cc;
  color: white;
}
</style>