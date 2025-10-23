<template>
  <div class="d-flex align-items-end gap-2">
    <div class="flex-grow-1">
      <label for="numeroConvenio" class="form-label"
        >Ingrese el número de convenio marco que desea vincular</label
      >
      <input
        id="numeroConvenio"
        v-model="numeroConvenio"
        type="text"
        class="form-control"
        placeholder="Ej: CM-2025-001"
      />
    </div>

    <div>
      <button
        class="btn btn-outline-primary mt-4"
        @click="emitirConvenio"
        :disabled="!numeroConvenio.trim()"
      >
        <i class="bi bi-link-45deg"></i> Vincular Convenio Marco
      </button>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const numeroConvenio = ref('')

const emitirConvenio = () => {
  if (numeroConvenio.value.trim()) {
    emit('vincular-convenio-marco', numeroConvenio.value.trim())
  }
}

const emit = defineEmits(['vincular-convenio-marco'])
</script>
