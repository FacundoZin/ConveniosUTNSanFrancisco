<template>
  <div class="card h-100 bg-light">
    <div class="card-body">
      <h6 class="card-title">{{ convenio.titulo || 'Sin título' }}</h6>
      <p class="card-text"><strong>Número:</strong> {{ convenio.numeroConvenio || '-' }}</p>
      <p class="card-text"><strong>Empresa:</strong> {{ convenio.nombreEmpresa || '-' }}</p>
      <p class="card-text">
        <strong>Fecha firma:</strong> {{ convenio.fechaFirmaConvenio || '-' }}
      </p>
      <p class="card-text">
        <strong>Fecha Firma:</strong> {{ convenio.fechaFirmaConvenio || '-' }}
      </p>
      <p class="card-text"><strong>Fecha fin:</strong> {{ convenio.fechaFin || '-' }}</p>
      <p class="card-text">
        <strong>Estado:</strong> {{ EstadoConvenioTexto[convenio.estado] || '-' }}
      </p>
      <p class="card-text"><strong>Refrendado:</strong> {{ convenio.refrendado ? 'Sí' : 'No' }}</p>
    </div>
    <div class="card-footer text-center">
      <button class="btn btn-sm btn-danger" @click="emitirEliminacion">Eliminar</button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { EstadoConvenioTexto } from '@/Types/Enums/Enums'
import type { ConvenioMarcoDto } from '@/Types/ViewModels/ViewModels'

const props = defineProps<{
  convenio: ConvenioMarcoDto
}>()

const emit = defineEmits<{
  (e: 'desvincularMarco'): void
}>()

const emitirEliminacion = () => {
  emit('desvincularMarco')
}
</script>
