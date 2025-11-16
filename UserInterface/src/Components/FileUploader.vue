<template>
  <div class="archivo-uploader">
    <!-- Botón principal -->
    <div class="text-center mb-3">
      <button class="btn btn-primary" @click="mostrarModal = true">
        <i class="bi bi-cloud-upload me-2"></i>Subir archivo
      </button>
    </div>

    <!-- Lista de archivos -->
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

          <!-- Botón descargar -->
          <button
            v-if="hoveredArchivo === archivo.idArchivo"
            class="btn btn-sm btn-success position-absolute bottom-0 start-0 m-2"
            @click="emitirDescarga(archivo.idArchivo, archivo.nombreArchivo)"
            title="Descargar archivo"
          >
            <i class="bi bi-download"></i>
          </button>

          <!-- Botón eliminar -->
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

    <div v-else class="col-12">
      <div class="card shadow-sm border rounded-3 p-4 text-center bg-light">
        <h6 class="mb-0 text-muted fst-italic">No hay convenios documentos cargados aún</h6>
      </div>
    </div>

    <!-- Modal para subir archivo -->
    <div
      class="modal fade show d-block"
      v-if="mostrarModal"
      tabindex="-1"
      style="background-color: rgba(0, 0, 0, 0.5)"
    >
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Subir nuevo archivo</h5>
            <button type="button" class="btn-close" @click="cerrarModal"></button>
          </div>

          <div class="modal-body">
            <!-- Input nombre -->
            <div class="mb-3">
              <label class="form-label">Nombre del documento</label>
              <input
                type="text"
                class="form-control"
                placeholder="Ej: Contrato firmado"
                v-model="nombreArchivo"
              />
            </div>

            <!-- Zona de arrastre -->
            <div
              class="dropzone border border-2 border-primary rounded p-4 text-center"
              :class="{ 'bg-light': isDragging }"
              @dragover.prevent="onDragOver"
              @dragleave.prevent="onDragLeave"
              @drop.prevent="onDrop"
            >
              <i class="bi bi-cloud-arrow-up fs-1 text-primary"></i>
              <p class="mt-2 mb-0 fw-semibold">
                Arrastrá un archivo aquí o hacé clic para seleccionar
              </p>

              <input type="file" class="d-none" ref="fileInput" @change="onFileSelect" />
              <button class="btn btn-outline-primary mt-3" @click="fileInput?.click()">
                Seleccionar archivo
              </button>

              <div v-if="archivoSeleccionado" class="mt-3">
                <small class="text-success fw-semibold">
                  <i class="bi bi-check-circle me-1"></i>{{ archivoSeleccionado.name }}
                </small>
              </div>
            </div>
          </div>

          <div class="modal-footer">
            <button class="btn btn-secondary" @click="cerrarModal">Cancelar</button>
            <button
              class="btn btn-primary"
              :disabled="!archivoSeleccionado || !nombreArchivo.trim()"
              @click="confirmarCarga"
            >
              Confirmar
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import type { ViewArchivoDto } from '@/Types/ViewModels/ViewModels'
import { ref } from 'vue'

defineProps<{
  archivos?: ViewArchivoDto[]
}>()

const emit = defineEmits(['archivo-cargado', 'archivo-eliminado', 'archivo-descargado'])

const mostrarModal = ref(false)
const nombreArchivo = ref('')
const archivoSeleccionado = ref<File | null>(null)

const hoveredArchivo = ref<number | null>(null)
const isDragging = ref(false)
const fileInput = ref<HTMLInputElement | null>(null)

// Drag & Drop
const onDragOver = () => (isDragging.value = true)
const onDragLeave = () => (isDragging.value = false)
const onDrop = (event: DragEvent) => {
  isDragging.value = false
  const file = event.dataTransfer?.files[0]
  if (file) archivoSeleccionado.value = file
}

// Selector manual
const onFileSelect = (event: Event) => {
  const target = event.target as HTMLInputElement
  const file = target.files?.[0]
  if (file) archivoSeleccionado.value = file
  if (fileInput.value) fileInput.value.value = ''
}

const emitirEliminado = (id: number) => emit('archivo-eliminado', id)

const emitirDescarga = (id: number, nombre: string) => {
  emit('archivo-descargado', id, nombre)
}

// Confirmar carga
const confirmarCarga = () => {
  if (archivoSeleccionado.value && nombreArchivo.value.trim()) {
    emit('archivo-cargado', {
      file: archivoSeleccionado.value,
      nombre: nombreArchivo.value.trim(),
    })
    cerrarModal()
  }
}

// Cerrar y limpiar modal
const cerrarModal = () => {
  mostrarModal.value = false
  nombreArchivo.value = ''
  archivoSeleccionado.value = null
  isDragging.value = false
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
