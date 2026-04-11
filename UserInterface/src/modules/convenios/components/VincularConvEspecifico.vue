<template>
  <div class="d-flex flex-column">
    <label for="numeroConvenio" class="form-label"
      >Ingrese el número del convenio especifico que desea vincular</label
    >
    <input
      id="numeroConvenio"
      v-model="props.request.numeroConvEspecificoParaVincular"
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
import type { CargarConvenioMarcoRequestDto } from '@/Types/ConvenioMarco/CreateConvenioMarco'

const props = defineProps<{
  request: CargarConvenioMarcoRequestDto
}>()
</script>
