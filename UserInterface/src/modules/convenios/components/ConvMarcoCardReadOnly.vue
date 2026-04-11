<template>
  <div class="card shadow-sm h-100">
    <div class="card-body">
      <h6 class="card-title text-primary mb-3">{{ convenio.titulo || 'Sin título' }}</h6>

      <div class="card-text">
        <div class="d-flex align-items-center mb-2">
          <i class="bi bi-hash me-2 text-muted"></i>
          <small><strong>Número:</strong> {{ convenio.numeroconvenio || ' -' }}</small>
        </div>

        <div class="d-flex align-items-center mb-2">
          <i class="bi bi-building me-2 text-muted"></i>
          <small class="text-truncate" :title="convenio.nombreEmpresa || ''">
            <strong>Empresa:</strong> {{ convenio.nombreEmpresa || ' -' }}
          </small>
        </div>

        <div class="d-flex align-items-center mb-2">
          <i class="bi bi-calendar-event me-2 text-muted"></i>
          <small>
            <strong>Vigencia:</strong>
            ({{ convenio.fechaFirmaConvenio || '-' }}) - ({{ convenio.fechaFin || '-' }})
          </small>
        </div>

        <div class="mt-3 pt-2 border-top d-flex flex-wrap gap-2">
          <span class="badge bg-info text-dark border">
            {{ EstadoConvenioTexto[convenio.estado] || 'Desconocido' }}
          </span>
          <span v-if="convenio.refrendado" class="badge bg-success text-white border"
            >Refrendado</span
          >
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { EstadoConvenioTexto } from '@/Types/Enums/Enums'
import type { ConvenioMarcoDto } from '@/Types/ViewModels/ViewModels'

defineProps<{
  convenio: ConvenioMarcoDto
}>()
</script>
