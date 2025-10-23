<template>
  <div class="archivo-uploader">
    <!-- Zona de arrastre -->
    <div
      class="dropzone border border-2 border-primary rounded p-4 text-center mb-4"
      :class="{ 'bg-light': isDragging }"
      @dragover.prevent="onDragOver"
      @dragleave.prevent="onDragLeave"
      @drop.prevent="onDrop"
    >
      <i class="bi bi-cloud-upload fs-1 text-primary"></i>
      <p class="mt-2 mb-0 fw-semibold">Arrastrá un archivo aquí o hacé clic para seleccionar</p>

      <input type="file" class="d-none" ref="fileInput" @change="onFileSelect" />
      <button class="btn btn-outline-primary mt-3" @click="fileInput?.click()">
        Seleccionar archivo
      </button>
    </div>

    <!-- Lista de archivos cargados -->
    <div v-if="archivos && archivos.length" class="row g-3">
      <div v-for="archivo in archivos" :key="archivo.idArchivo" class="col-md-4 col-sm-6">
        <div
          class="card h-100 shadow-sm position-relative archivo-card"
          @mouseenter="hoveredArchivo = archivo.idArchivo"
          @mouseleave="hoveredArchivo = null"
        >
          <div class="card-body d-flex flex-column justify-content-center align-items-center">
            <i class="bi bi-file-earmark-text fs-1 text-primary"></i>
            <h6 class="mt-2 text-center text-truncate" :title="archivo.nombreArchivo">
              {{ archivo.nombreArchivo }}
            </h6>
          </div>

          <!-- Botón eliminar (visible al pasar el mouse) -->
          <button
            v-if="hoveredArchivo === archivo.idArchivo"
            class="btn btn-sm btn-danger position-absolute top-0 end-0 m-2"
            @click="emitirEliminado(archivo.idArchivo)"
            title="Eliminar archivo"
          >
            <i class="bi bi-x-lg"></i>
          </button>
        </div>
      </div>
    </div>

    <div v-else class="text-muted fst-italic text-center">
      Este convenio todavía no tiene documentos cargados.
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'

export interface ViewArchivoDto {
  idArchivo: number
  nombreArchivo: string
}

defineProps<{
  archivos?: ViewArchivoDto[]
}>()

const emit = defineEmits(['archivo-cargado', 'archivo-eliminado'])

const isDragging = ref(false)
const hoveredArchivo = ref<number | null>(null)
const fileInput = ref<HTMLInputElement | null>(null)

// Drag & Drop handlers
const onDragOver = () => (isDragging.value = true)
const onDragLeave = () => (isDragging.value = false)
const onDrop = (event: DragEvent) => {
  isDragging.value = false
  const file = event.dataTransfer?.files[0]
  if (file) emit('archivo-cargado', file)
}

// Selector manual
const onFileSelect = (event: Event) => {
  const target = event.target as HTMLInputElement
  const file = target.files?.[0]
  if (file) emit('archivo-cargado', file)
  if (fileInput.value) fileInput.value.value = '' // reset
}

// Emit eliminación
const emitirEliminado = (id: number) => {
  emit('archivo-eliminado', id)
}
</script>

<style scoped>
.dropzone {
  cursor: pointer;
  transition: background-color 0.2s ease;
}

.dropzone.bg-light {
  background-color: rgba(13, 110, 253, 0.1) !important;
}

.archivo-card {
  transition:
    transform 0.2s ease,
    box-shadow 0.2s ease;
}

.archivo-card:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
}
</style>
