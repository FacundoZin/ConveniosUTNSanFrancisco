<script setup lang="ts">
import { computed } from 'vue'

const props = defineProps<{
  count: number | null
  typeOfConvenio: 'marco' | 'especifico'
  searchType: 'mes' | 'rango'
  month?: number
  year?: number
  fechaDesde?: string
  fechaHasta?: string
}>()

const emit = defineEmits<{
  (e: 'close'): void
}>()

const meses = [
  'Enero',
  'Febrero',
  'Marzo',
  'Abril',
  'Mayo',
  'Junio',
  'Julio',
  'Agosto',
  'Septiembre',
  'Octubre',
  'Noviembre',
  'Diciembre',
]

const formatearFecha = (fecha: string) => {
  const date = new Date(fecha)
  return date.toLocaleDateString('es-AR', { day: '2-digit', month: '2-digit', year: 'numeric' })
}

const tipoConvenio = computed(() => {
  return props.typeOfConvenio === 'marco' ? 'Convenios Marco' : 'Convenios Específicos'
})

const periodoTexto = computed(() => {
  if (props.searchType === 'mes' && props.month && props.year) {
    const nombreMes = meses[props.month - 1]
    return `${nombreMes} de ${props.year}`
  } else if (props.searchType === 'rango' && props.fechaDesde && props.fechaHasta) {
    return `${formatearFecha(props.fechaDesde)} - ${formatearFecha(props.fechaHasta)}`
  }
  return ''
})

const iconoConvenio = computed(() => {
  return props.typeOfConvenio === 'marco' ? 'bi-folder-fill' : 'bi-file-earmark-text-fill'
})

const mensajePrincipal = computed(() => {
  if (props.count === null) return ''
  if (props.count === 0) {
    return `No se firmaron ${tipoConvenio.value.toLowerCase()}`
  }
  return `${props.count === 1 ? 'Se ha firmado' : 'Se han firmado'} ${props.count} ${tipoConvenio.value.toLowerCase()}`
})

const showSuggestions = computed(() => {
  return props.count === 0
})
</script>

<template>
  <Transition name="slide-fade">
    <div v-if="count !== null" class="result-container container-animada">
      <div class="count-result-card">
        <div class="result-header">
          <div class="result-icon">
            <i :class="['bi', iconoConvenio]"></i>
          </div>
          <button
            type="button"
            class="btn-close btn-close-custom"
            @click="emit('close')"
            aria-label="Cerrar"
          ></button>
        </div>

        <h4 class="result-title">{{ mensajePrincipal }}</h4>

        <div class="result-details">
          <p class="result-period text-muted mb-2">
            <i class="bi bi-calendar3 me-2"></i>{{ periodoTexto }}
          </p>
          <p class="result-type text-muted mb-0">
            <i class="bi bi-tag me-2"></i>{{ tipoConvenio }}
          </p>
        </div>

        <div v-if="showSuggestions" class="result-suggestions">
          <p class="text-muted mb-2"><i class="bi bi-lightbulb me-2"></i>Sugerencias:</p>
          <ul class="list-unstyled text-muted mb-0">
            <li>
              <i class="bi bi-check2 me-2 text-primary"></i>Intenta con otro mes o rango de fechas
            </li>
            <li>
              <i class="bi bi-check2 me-2 text-primary"></i>Verifica el tipo de convenio
              seleccionado
            </li>
            <li>
              <i class="bi bi-check2 me-2 text-primary"></i>Busca en
              {{ typeOfConvenio === 'marco' ? 'Convenios Específicos' : 'Convenios Marco' }}
            </li>
          </ul>
        </div>
      </div>
    </div>
  </Transition>
</template>

<style scoped>
/* Transición suave */
.slide-fade-enter-from,
.slide-fade-leave-to {
  transform: translateY(-10px);
  opacity: 0;
}

.slide-fade-enter-active,
.slide-fade-leave-active {
  transition: all 0.4s ease;
}

.slide-fade-enter-to,
.slide-fade-leave-from {
  transform: translateY(0);
  opacity: 1;
}

/* Contenedor principal */
.result-container {
  width: 95%;
  max-width: 700px;
  margin: 2rem auto;
}

.container-animada {
  transition: all 0.4s ease;
}

/* Card de resultado */
.count-result-card {
  background: #ffffff;
  border: 1px solid #dee2e6;
  border-radius: 8px;
  padding: 2rem;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.08);
  position: relative;
}

/* Header con icono y botón cerrar */
.result-header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 1.5rem;
}

.result-icon {
  font-size: 3rem;
  color: var(--bs-primary);
  display: flex;
  align-items: center;
  justify-content: center;
  width: 70px;
  height: 70px;
  background: rgba(13, 110, 253, 0.1);
  border-radius: 12px;
}

.btn-close-custom {
  opacity: 0.5;
  transition: opacity 0.2s ease;
}

.btn-close-custom:hover {
  opacity: 1;
}

/* Título principal */
.result-title {
  font-size: 1.5rem;
  font-weight: 600;
  margin-bottom: 1rem;
  color: #212529;
  text-align: center;
}

/* Detalles del período */
.result-details {
  text-align: center;
  margin-bottom: 1.5rem;
  padding-bottom: 1.5rem;
  border-bottom: 1px solid #e9ecef;
}

.result-period,
.result-type {
  font-size: 0.95rem;
}

/* Badge de conteo */
.result-count-badge {
  display: flex;
  justify-content: center;
  margin-bottom: 1.5rem;
}

.count-display {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 0.5rem;
  padding: 1.5rem 3rem;
  border-radius: 12px;
  transition: all 0.3s ease;
}

.count-positive {
  background: linear-gradient(135deg, #d4edda 0%, #c3e6cb 100%);
  border: 2px solid #28a745;
}

.count-zero {
  background: linear-gradient(135deg, #f8f9fa 0%, #e9ecef 100%);
  border: 2px solid #6c757d;
}

.count-number {
  font-size: 3rem;
  font-weight: 700;
  line-height: 1;
}

.count-positive .count-number {
  color: #155724;
}

.count-zero .count-number {
  color: #6c757d;
}

.count-label {
  font-size: 1rem;
  font-weight: 500;
  text-transform: uppercase;
  letter-spacing: 0.5px;
}

.count-positive .count-label {
  color: #155724;
}

.count-zero .count-label {
  color: #6c757d;
}

/* Sugerencias */
.result-suggestions {
  background: #f8f9fa;
  border: 1px solid #e9ecef;
  border-radius: 6px;
  padding: 1.5rem;
  margin-bottom: 1.5rem;
  text-align: left;
}

.result-suggestions p {
  margin-bottom: 0.8rem;
  font-weight: 600;
  font-size: 0.95rem;
}

.result-suggestions li {
  padding: 0.4rem 0;
  font-size: 0.9rem;
}

/* Footer */
.result-footer {
  text-align: center;
  padding-top: 1rem;
  border-top: 1px solid #e9ecef;
}

/* Responsive */
@media (max-width: 768px) {
  .count-result-card {
    padding: 1.5rem;
  }

  .result-icon {
    font-size: 2.5rem;
    width: 60px;
    height: 60px;
  }

  .result-title {
    font-size: 1.25rem;
  }

  .count-display {
    padding: 1rem 2rem;
  }

  .count-number {
    font-size: 2.5rem;
  }

  .count-label {
    font-size: 0.9rem;
  }
}
</style>
