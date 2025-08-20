<template>
  <div class="w-full bg-black p-3 flex items-center justify-center gap-3">
    <!-- Input -->
    <input
      v-model="term"
      type="text"
      placeholder="Escribí tu búsqueda..."
      class="p-2 rounded w-1/2 text-black"
    />

    <!-- Botones toggle -->
    <button
      @click="tipo = 'titulo'"
      :class="[
        'px-4 py-2 rounded',
        tipo === 'titulo' ? 'bg-blue-500 text-white' : 'bg-white text-black'
      ]"
    >
      Título
    </button>

    <button
      @click="tipo = 'empresa'"
      :class="[
        'px-4 py-2 rounded',
        tipo === 'empresa' ? 'bg-blue-500 text-white' : 'bg-white text-black'
      ]"
    >
      Empresa
    </button>
  </div>
</template>

<script setup>
import { ref, watch } from 'vue'

const emit = defineEmits(['update'])

const term = ref('')
const tipo = ref('titulo')

// Cada vez que term o tipo cambian, se emite automáticamente
watch([term, tipo], () => {
  emit('update', { tipo: tipo.value, valor: term.value.trim() })
})
</script>
