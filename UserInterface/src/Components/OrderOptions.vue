<script setup lang="ts">
import type { ConvenioQueryObject } from '@/Types/Api.Interface';
import { ref, watch } from 'vue';

defineProps<{ query: ConvenioQueryObject }>();
const emit = defineEmits(['update:query']);

const ordenSeleccionado = ref('ProximosAterminar');

watch(ordenSeleccionado, (nuevoValor) => {
  const newQuery: Partial<ConvenioQueryObject> = {
    ProximosAterminar: false,
    AntiguedadAscendente: false,
    AntiguedadDescendente: false,
  };

  if (nuevoValor === 'ProximosAterminar') {
    newQuery.ProximosAterminar = true;
  } else if (nuevoValor === 'AntiguedadAscendente') {
    newQuery.AntiguedadAscendente = true;
  } else if (nuevoValor === 'AntiguedadDescendente') {
    newQuery.AntiguedadDescendente = true;
  }

  emit('update:query', newQuery);
});
</script>

<template>
  <div class="bg-white p-4 rounded-lg shadow-md mt-4">
    <h3 class="text-lg font-semibold text-gray-800 mb-3">Ordenar por:</h3>
    <div class="flex items-center gap-6">
      <label class="flex items-center gap-2 cursor-pointer">
        <input type="radio" name="orden" value="ProximosAterminar" v-model="ordenSeleccionado" class="form-radio text-blue-600" />
        <span class="text-gray-700">Próximos a terminar</span>
      </label>

      <label class="flex items-center gap-2 cursor-pointer">
        <input type="radio" name="orden" value="AntiguedadAscendente" v-model="ordenSeleccionado" class="form-radio text-blue-600" />
        <span class="text-gray-700">Más actuales</span>
      </label>

      <label class="flex items-center gap-2 cursor-pointer">
        <input type="radio" name="orden" value="AntiguedadDescendente" v-model="ordenSeleccionado" class="form-radio text-blue-600" />
        <span class="text-gray-700">Más viejos</span>
      </label>
    </div>
  </div>
</template>
