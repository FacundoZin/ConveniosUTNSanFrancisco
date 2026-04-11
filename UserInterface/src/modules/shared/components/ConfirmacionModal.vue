<template>
  <div
    v-if="show"
    class="modal fade show d-block"
    tabindex="-1"
    style="background-color: rgba(0, 0, 0, 0.5)"
    @click.self="cancelar"
  >
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content border-0 shadow-lg">
        <!-- Header -->
        <div class="modal-header border-0 pb-0">
          <h5 class="modal-title d-flex align-items-center gap-2">
            <i :class="iconClass" class="text-warning fs-4"></i>
            {{ titulo }}
          </h5>
          <button
            type="button"
            class="btn-close"
            @click="cancelar"
            aria-label="Cerrar"
          ></button>
        </div>

        <!-- Body -->
        <div class="modal-body pt-2">
          <p class="mb-0">{{ mensaje }}</p>
        </div>

        <!-- Footer -->
        <div class="modal-footer border-0 pt-0">
          <button type="button" class="btn btn-secondary" @click="cancelar">
            {{ textoCancelar }}
          </button>
          <button
            type="button"
            :class="botonConfirmarClass"
            @click="confirmar"
          >
            {{ textoConfirmar }}
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'

interface Props {
  show: boolean
  titulo?: string
  mensaje: string
  textoConfirmar?: string
  textoCancelar?: string
  tipo?: 'danger' | 'warning' | 'info'
}

const props = withDefaults(defineProps<Props>(), {
  titulo: 'Confirmar acción',
  textoConfirmar: 'Confirmar',
  textoCancelar: 'Cancelar',
  tipo: 'warning',
})

const emit = defineEmits<{
  (e: 'confirmar'): void
  (e: 'cancelar'): void
}>()

const iconClass = computed(() => {
  const baseClass = 'bi'
  switch (props.tipo) {
    case 'danger':
      return `${baseClass} bi-exclamation-triangle-fill text-danger`
    case 'warning':
      return `${baseClass} bi-exclamation-circle-fill text-warning`
    case 'info':
      return `${baseClass} bi-info-circle-fill text-info`
    default:
      return `${baseClass} bi-exclamation-circle-fill text-warning`
  }
})

const botonConfirmarClass = computed(() => {
  switch (props.tipo) {
    case 'danger':
      return 'btn btn-danger'
    case 'warning':
      return 'btn btn-warning'
    case 'info':
      return 'btn btn-primary'
    default:
      return 'btn btn-warning'
  }
})

const confirmar = () => {
  emit('confirmar')
}

const cancelar = () => {
  emit('cancelar')
}
</script>

<style scoped>
.modal {
  display: block;
  overflow-y: auto;
}

.modal-content {
  border-radius: 12px;
}

.modal-header,
.modal-footer {
  padding: 1.25rem 1.5rem;
}

.modal-body {
  padding: 0.5rem 1.5rem 1.25rem;
}
</style>
