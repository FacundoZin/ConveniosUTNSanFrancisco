<script setup lang="ts">
import type { ConvenioQueryObject } from '@/Types/Api.Interface';

const props = defineProps<{ query: ConvenioQueryObject }>();

import { ref, watch } from 'vue';

// estado del radio button
const ordenSeleccionado = ref('ProximosAterminar');

// cada vez que cambia el radio, actualizamos los booleanos
watch(ordenSeleccionado, (nuevoValor) => {
  if(nuevoValor==='ProximosAterminar') {
      props.query.ProximosAterminar = true;
      props.query.AntiguedadAscendente = false;
      props.query.AntiguedadDescendente = false;
  };
  if(nuevoValor==="AntiguedadAscendente") {
      props.query.ProximosAterminar = false;
      props.query.AntiguedadAscendente = true;
      props.query.AntiguedadDescendente = false;
  };
  if(nuevoValor==="AntiguedadDescendente") {
      props.query.ProximosAterminar = false;
      props.query.AntiguedadAscendente = false;
      props.query.AntiguedadDescendente = false;
  }
  
});
</script>

<template>
  <div class="order-options">
    <label class="radio-label">
      <input type="radio" name="orden" value="ProximosAterminar" v-model="ordenSeleccionado" />
      Próximos a terminar
    </label>

    <label class="radio-label">
      <input type="radio" name="orden" value="AntiguedadAscendente" v-model="ordenSeleccionado" />
      Más actuales
    </label>

    <label class="radio-label">
      <input type="radio" name="orden" value="AntiguedadDescendente" v-model="ordenSeleccionado" />
      Más viejos
    </label>
  </div>
</template>

<style scoped>
.order-options {
  display: flex;
  gap: 15px;
  flex-wrap: wrap;
  color: white;
}

.radio-label {
  display: flex;
  align-items: center;
  gap: 5px;
  cursor: pointer;
}

.radio-label input[type="radio"] {
  accent-color: #00a1e4; /* celeste UTN */
  cursor: pointer;
}

.radio-label:hover {
  opacity: 0.8;
}
</style>
