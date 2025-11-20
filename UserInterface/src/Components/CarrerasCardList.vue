<template>
  <div class="carreras-container">
    <div v-if="carreras && carreras.length > 0" class="d-flex flex-wrap gap-3">
      <div
        v-for="carrera in carreras"
        :key="carrera.id"
        class="carrera-card card shadow-sm border-0"
      >
        <div class="card-body p-3 d-flex align-items-center">
          <div class="icon-container me-3">
            <i class="bi bi-mortarboard-fill text-primary"></i>
          </div>
          <div class="carrera-info flex-grow-1">
            <h6 class="card-title mb-0 text-dark">{{ carrera.nombre }}</h6>
          </div>
        </div>
      </div>
    </div>
    <div v-else class="text-muted">
      <div class="card shadow-sm p-3 text-center" style="background-color: #f8f9fa">
        <div class="card-body">
          <h6 class="card-title mb-2">Sin carreras involucradas</h6>
          <p class="text-muted mb-0">Aún no hay carreras involucradas en este convenio</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import type { Carrera } from '@/Types/CarrerasInvolucradas/CarrerasInvolucradas'

defineProps<{
  carreras: Carrera[]
}>()
</script>

<style scoped>
.carreras-container {
  margin-bottom: 1.5rem;
}

.carrera-card {
  min-width: 280px;
  max-width: 320px;
  transition: all 0.3s ease;
  background: linear-gradient(135deg, #ffffff 0%, #f8f9fa 100%);
  border-left: 4px solid #0d6efd !important;
}

.carrera-card:hover {
  transform: translateY(-4px);
  box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15) !important;
  border-left-color: #0a58ca !important;
}

.icon-container {
  width: 45px;
  height: 45px;
  display: flex;
  align-items: center;
  justify-content: center;
  background: linear-gradient(135deg, #0d6efd15 0%, #0d6efd25 100%);
  border-radius: 12px;
  flex-shrink: 0;
}

.icon-container i {
  font-size: 1.5rem;
}

.carrera-info h6 {
  font-weight: 600;
  font-size: 0.95rem;
  line-height: 1.3;
}

.carrera-info small {
  font-size: 0.75rem;
}
</style>
