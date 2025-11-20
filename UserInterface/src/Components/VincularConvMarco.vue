<template>
  <div class="d-flex flex-column">
    <label for="numeroConvenioMarco" class="form-label"
      >Ingrese el número del convenio marco que desea vincular</label
    >
    <input
      id="numeroConvenioMarco"
      v-model="props.request.numeroConvenioMarcoVinculado"
      type="text"
      class="form-control"
      placeholder="Ej: CM-2025-001"
    />
    <div class="form-text text-muted">
      El convenio se vinculará automáticamente al ingresar el número.
    </div>
  </div>
</template>

<script setup lang="ts">
import type { CargarConvenioEspecificoRequestDto } from '@/Types/ConvenioEspecifico/CreateConvenioEspecifico'

const props = defineProps<{
  request: CargarConvenioEspecificoRequestDto
}>()
</script>
