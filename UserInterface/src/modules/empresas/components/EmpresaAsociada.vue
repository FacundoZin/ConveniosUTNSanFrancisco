<template>
  <div class="card shadow-sm mb-3">
    <div class="card-body d-flex justify-content-between align-items-center">
      <div>
        <h5 class="card-title mb-1">{{ empresa.nombre_Empresa }}</h5>
        <p class="mb-1 text-muted">Razón social: {{ empresa.razonSocial || '-' }}</p>
        <p class="mb-1">CUIT: {{ empresa.cuit || '-' }}</p>
        <p class="mb-1">Dirección: {{ empresa.direccion_Empresa || '-' }}</p>
        <p class="mb-1">Teléfono: {{ empresa.telefono_Empresa || '-' }}</p>
        <p class="mb-0">Email: {{ empresa.email_Empresa || '-' }}</p>
      </div>

      <div class="ms-3">
        <button
          class="btn btn-outline-danger btn-sm"
          @click="emit('onDesvincularEmpresa')"
        >
          <i class="bi bi-x-circle me-1"></i> Desvincular
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import type { EmpresaDto } from '@/Types/ViewModels/ViewModels';
import { defineProps, defineEmits } from 'vue'

const props = defineProps<{ empresa: EmpresaDto }>()
const emit = defineEmits<{ (e: 'onDesvincularEmpresa'): void }>()
</script>
