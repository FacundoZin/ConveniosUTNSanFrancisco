<template>
  <div v-if="isLoading" class="d-flex justify-content-center my-4">
    <div class="spinner-border spinner-border-sm text-primary" role="status">
      <span class="visually-hidden">Cargando...</span>
    </div>
  </div>

  <div v-else-if="tieneDatos" class="mt-4">

    <!-- Últimos Convenios Marco -->
    <div v-if="ultimos.conveniosMarcos.length > 0" class="card border-0 shadow-sm mb-3">
      <div class="card-header bg-white border-0 py-2 px-3">
        <span class="fw-semibold text-primary small">
          <i class="bi bi-folder me-1"></i>Últimos Convenios Marco
        </span>
      </div>
      <div class="list-group list-group-flush">
        <button v-for="conv in ultimos.conveniosMarcos" :key="'marco-' + conv.id"
          class="list-group-item list-group-item-action d-flex align-items-center justify-content-between py-2 px-3 border-0"
          @click="verConvenio(conv.id, 'marco')">
          <div class="d-flex align-items-center gap-2 text-start min-width-0">
            <span :class="getEstadoBadgeClass(conv.estado)" class="badge flex-shrink-0" style="font-size: 0.6rem">
              {{ EstadoConvenioTexto[conv.estado] }}
            </span>
            <div class="text-truncate">
              <span class="small fw-medium">{{ conv.titulo || 'Sin Título' }}</span>
              <span v-if="conv.nombreEmpresa" class="text-muted small d-block text-truncate" style="font-size: 0.75rem">
                {{ conv.nombreEmpresa }}
              </span>
            </div>
          </div>
          <i class="bi bi-arrow-right-short text-primary flex-shrink-0 ms-2 fs-5"></i>
        </button>
      </div>
    </div>

    <!-- Últimos Convenios Específicos -->
    <div v-if="ultimos.conveniosEspecificos.length > 0" class="card border-0 shadow-sm">
      <div class="card-header bg-white border-0 py-2 px-3">
        <span class="fw-semibold text-primary small">
          <i class="bi bi-file-earmark-text me-1"></i>Últimos Convenios Específicos
        </span>
      </div>
      <div class="list-group list-group-flush">
        <button v-for="conv in ultimos.conveniosEspecificos" :key="'esp-' + conv.id"
          class="list-group-item list-group-item-action d-flex align-items-center justify-content-between py-2 px-3 border-0"
          @click="verConvenio(conv.id, 'especifico')">
          <div class="d-flex align-items-center gap-2 text-start min-width-0">
            <span :class="getEstadoBadgeClass(conv.estado)" class="badge flex-shrink-0" style="font-size: 0.6rem">
              {{ EstadoConvenioTexto[conv.estado] }}
            </span>
            <div class="text-truncate">
              <span class="small fw-medium">{{ conv.titulo || 'Sin Título' }}</span>
              <span v-if="conv.nombreEmpresa" class="text-muted small d-block text-truncate" style="font-size: 0.75rem">
                {{ conv.nombreEmpresa }}
              </span>
            </div>
          </div>
          <i class="bi bi-arrow-right-short text-primary flex-shrink-0 ms-2 fs-5"></i>
        </button>
      </div>
    </div>
  </div>

  <div v-else class="text-center py-4 text-muted small">
    <i class="bi bi-inbox" style="font-size: 1.5rem"></i>
    <p class="mt-2 mb-0">No hay convenios cargados aún.</p>
  </div>
</template>

<script setup lang="ts">
import type { UltimosConveniosDto } from '@/Types/Convenios/ConvenioUltimoDto'
import { EstadoConvenioTexto } from '@/Types/Enums/Enums'
import ConvenioService from '@/modules/convenios/services/ConvenioService'
import { computed, onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const isLoading = ref(true)
const ultimos = ref<UltimosConveniosDto>({
  conveniosMarcos: [],
  conveniosEspecificos: [],
})

const tieneDatos = computed(
  () => ultimos.value.conveniosMarcos.length > 0 || ultimos.value.conveniosEspecificos.length > 0,
)

onMounted(async () => {
  const result = await ConvenioService.GetUltimosConvenios(5)
  if (result.isSuccess && result.value) {
    ultimos.value = result.value
  }
  isLoading.value = false
})

function verConvenio(id: number, tipo: string) {
  if (tipo === 'marco') {
    router.push({ name: 'VistaConvenioMarco', params: { id } })
  } else {
    router.push({ name: 'VistaConvenioEspecifico', params: { id } })
  }
}

function getEstadoBadgeClass(estado: number) {
  switch (estado) {
    case 0:
      return 'bg-warning-subtle text-warning border border-warning-subtle'
    case 1:
      return 'bg-success-subtle text-success border border-success-subtle'
    case 2:
      return 'bg-secondary-subtle text-secondary border border-secondary-subtle'
    default:
      return 'bg-light text-dark border'
  }
}
</script>
