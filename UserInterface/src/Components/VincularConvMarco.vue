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
interface RequestWithConvenioMarco {
  numeroConvenioMarcoVinculado: string | null
}

const props = defineProps<{
  request: RequestWithConvenioMarco
}>()
</script>
