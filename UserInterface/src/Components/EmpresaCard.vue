<template>
  <div class="card shadow-sm h-100">
    <div class="card-body position-relative">
      <button
        type="button"
        @click="emitirDesvinculacion"
        class="btn-close position-absolute top-0 end-0 m-2"
        aria-label="Desvincular"
      ></button>

      <h6 class="card-title text-primary mb-3 pe-4">{{ empresa.nombre_Empresa }}</h6>

      <div class="card-text">
        <div class="d-flex align-items-center mb-2">
          <i class="bi bi-building me-2 text-muted"></i>
          <small><strong>Razón Social:</strong> {{ empresa.razonSocial || ' -' }}</small>
        </div>

        <div class="d-flex align-items-center mb-2">
          <i class="bi bi-hash me-2 text-muted"></i>
          <small><strong>CUIT:</strong> {{ empresa.cuit || ' -' }}</small>
        </div>

        <div class="d-flex align-items-center mb-2">
          <i class="bi bi-geo-alt me-2 text-muted"></i>
          <small class="text-truncate" :title="empresa.direccion_Empresa || ''">
            <strong>Dirección:</strong> {{ empresa.direccion_Empresa || ' -' }}
          </small>
        </div>

        <div class="d-flex align-items-center mb-2">
          <i class="bi bi-telephone me-2 text-muted"></i>
          <small><strong>Teléfono:</strong> {{ empresa.telefono_Empresa || ' -' }}</small>
        </div>

        <div class="d-flex align-items-center mb-2">
          <i class="bi bi-envelope me-2 text-muted"></i>
          <small class="text-truncate" :title="empresa.email_Empresa || ''">
            <strong>Email:</strong> {{ empresa.email_Empresa || ' -' }}
          </small>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import type { EmpresaDto } from '@/Types/ViewModels/ViewModels'

const emit = defineEmits<{
  (e: 'desvincularEmpresa', id: number): void
}>()

const emitirDesvinculacion = () => {
  emit('desvincularEmpresa', props.empresa.id)
}

const props = defineProps<{
  empresa: EmpresaDto
}>()
</script>
