<template>
  <div class="card mb-4 bg-light">
    <div class="row no-gutters align-items-center">
      <div class="col-md-10">
        <div class="card-body">
          <h5 class="card-title">{{ empresa.nombre_Empresa }}</h5>
          <p class="card-text"><strong>Razón social:</strong> {{ empresa.razonSocial || ' -' }}</p>
          <p class="card-text"><strong>CUIT:</strong> {{ empresa.cuit || ' -' }}</p>
          <p class="card-text">
            <strong>Dirección:</strong> {{ empresa.direccion_Empresa || ' -' }}
          </p>
          <p class="card-text"><strong>Teléfono:</strong> {{ empresa.telefono_Empresa || ' -' }}</p>
          <p class="card-text"><strong>Email:</strong> {{ empresa.email_Empresa || ' -' }}</p>
        </div>
      </div>
      <div class="col-md-2 text-center">
        <button class="btn btn-outline-danger m-2" @click="emitirDesvinculacion">
          Desvincular
        </button>
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
