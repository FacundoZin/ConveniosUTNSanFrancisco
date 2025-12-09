<template>
  <div v-if="isloading" class="d-flex justify-content-center my-5">
    <div class="spinner-border text-primary" role="status">
      <span class="visually-hidden">Cargando...</span>
    </div>
  </div>

  <Transition name="slide-fade">
    <div v-if="!isloading && convenios.Type !== ''" class="convenios-container">
      <div
        v-if="
          (convenios.Type !== 'ambos' && convenios.data.length === 0) ||
          (convenios.Type === 'ambos' &&
            convenios.conveniosMarcos.length === 0 &&
            convenios.conveniosEspecificos.length === 0)
        "
        class="card border-0 shadow-sm text-center py-5 mx-auto"
        style="max-width: 600px"
      >
        <div class="card-body">
          <div class="mb-3 text-secondary opacity-25">
            <i class="bi bi-inbox-fill" style="font-size: 4rem"></i>
          </div>
          <h4 class="fw-bold text-dark mb-2">No se encontraron convenios</h4>
          <p class="text-muted mb-4">
            No hay convenios {{ convenios.Type === 'marco' ? 'marco' : 'específicos' }} que
            coincidan con los criterios de búsqueda.
          </p>
          <div
            class="bg-light rounded-3 p-4 text-start d-inline-block w-100 mb-4 border border-light-subtle"
          >
            <p class="fw-bold text-muted mb-3 small text-uppercase">
              <i class="bi bi-lightbulb-fill me-2 text-warning"></i>Sugerencias
            </p>
            <ul class="list-unstyled text-muted mb-0 small">
              <li class="mb-2">
                <i class="bi bi-check-circle-fill me-2 text-success-subtle"></i>Verifica los filtros
                aplicados
              </li>
              <li class="mb-2">
                <i class="bi bi-check-circle-fill me-2 text-success-subtle"></i>Intenta con otros
                parámetros de búsqueda
              </li>
              <li>
                <i class="bi bi-check-circle-fill me-2 text-success-subtle"></i>Busca en
                {{ convenios.Type === 'marco' ? 'Convenios Específicos' : 'Convenios Marco' }}
              </li>
            </ul>
          </div>

          <div>
            <button class="btn btn-primary px-4 py-2" @click="$emit('reset-search')">
              <i class="bi bi-arrow-counterclockwise me-2"></i>Restablecer búsqueda
            </button>
          </div>
        </div>
      </div>

      <template v-else>
        <!-- TABLA ESPECIFICOS -->
        <div
          v-if="
            convenios.Type === 'especifico' ||
            (convenios.Type === 'ambos' && convenios.conveniosEspecificos.length > 0)
          "
          class="card border-0 shadow-sm mb-5"
        >
          <div class="card-header bg-white border-0 pt-4 px-4 pb-0">
            <h5 class="fw-bold text-primary mb-0">
              <i class="bi bi-file-earmark-text me-2"></i>Convenios Específicos
            </h5>
          </div>
          <div class="card-body p-0">
            <div class="table-responsive">
              <table class="table table-hover align-middle mb-0">
                <thead class="bg-light">
                  <tr>
                    <th class="py-3 ps-4">Título</th>
                    <th class="py-3">N° Convenio</th>
                    <th class="py-3">Empresa</th>
                    <th class="py-3">Firma</th>
                    <th class="py-3">Inicio</th>
                    <th class="py-3">Fin</th>
                    <th class="py-3">Estado</th>
                    <th class="text-center py-3">Acta</th>
                    <th class="text-center py-3">Refrendado</th>
                    <th class="text-center py-3">Acciones</th>
                  </tr>
                </thead>
                <tbody>
                  <tr
                    v-for="conv in convenios.Type === 'ambos'
                      ? convenios.conveniosEspecificos
                      : convenios.data"
                    :key="conv.id"
                  >
                    <td class="ps-4 fw-bold text-dark">{{ conv.titulo || 'Sin Título' }}</td>
                    <td class="text-muted small">
                      <span v-if="conv.numeroconvenio">{{ conv.numeroconvenio }}</span>
                      <span v-else class="text-muted fst-italic">-</span>
                    </td>
                    <td>
                      <span v-if="conv.nombreEmpresa">{{ conv.nombreEmpresa }}</span>
                      <span v-else class="text-muted fst-italic">-</span>
                    </td>
                    <td>
                      <span v-if="conv.fechaFirmaConvenio">{{ conv.fechaFirmaConvenio }}</span>
                      <span v-else class="text-muted fst-italic">-</span>
                    </td>
                    <td>
                      <span v-if="conv.fechaInicioActividades">{{
                        conv.fechaInicioActividades
                      }}</span>
                      <span v-else class="text-muted fst-italic">-</span>
                    </td>
                    <td>
                      <span v-if="conv.fechaFin">{{ conv.fechaFin }}</span>
                      <span v-else class="text-muted fst-italic">-</span>
                    </td>
                    <td>
                      <span :class="getEstadoBadgeClass(conv.estado)">
                        {{ EstadoConvenioTexto[conv.estado] }}
                      </span>
                    </td>
                    <td class="text-center">
                      <i
                        v-if="conv.esActa"
                        class="bi bi-check-circle-fill text-success opacity-75"
                      ></i>
                      <i v-else class="bi bi-circle text-muted opacity-25"></i>
                    </td>
                    <td class="text-center">
                      <i
                        v-if="conv.refrendado"
                        class="bi bi-check-circle-fill text-success opacity-75"
                      ></i>
                      <i v-else class="bi bi-circle text-muted opacity-25"></i>
                    </td>
                    <td class="text-center">
                      <button
                        class="btn btn-sm btn-light text-primary border-0"
                        @click="VerConvenioCompleto(conv.id, conv.convenioType)"
                        title="Ver detalles"
                      >
                        <i class="bi bi-eye-fill fs-5"></i>
                      </button>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>

        <!-- TABLA MARCOS -->
        <div
          v-if="
            convenios.Type === 'marco' ||
            (convenios.Type === 'ambos' && convenios.conveniosMarcos.length > 0)
          "
          class="card border-0 shadow-sm"
        >
          <div class="card-header bg-white border-0 pt-4 px-4 pb-0">
            <h5 class="fw-bold text-primary mb-0">
              <i class="bi bi-folder me-2"></i>Convenios Marco
            </h5>
          </div>
          <div class="card-body p-0">
            <div class="table-responsive">
              <table class="table table-hover align-middle mb-0">
                <thead class="bg-light">
                  <tr>
                    <th class="py-3 ps-4">Título</th>
                    <th class="py-3">N° Convenio</th>
                    <th class="py-3">Empresa</th>
                    <th class="py-3">Firma</th>
                    <th class="py-3">Fin</th>
                    <th class="py-3">Estado</th>
                    <th class="text-center py-3">Refrendado</th>
                    <th class="text-center py-3">Acciones</th>
                  </tr>
                </thead>
                <tbody>
                  <tr
                    v-for="conv in convenios.Type === 'ambos'
                      ? convenios.conveniosMarcos
                      : convenios.data"
                    :key="conv.id"
                  >
                    <td class="ps-4 fw-bold text-dark">{{ conv.titulo || 'Sin Título' }}</td>
                    <td class="text-muted small">
                      <span v-if="conv.numeroconvenio">{{ conv.numeroconvenio }}</span>
                      <span v-else class="text-muted fst-italic">-</span>
                    </td>
                    <td>
                      <span v-if="conv.nombreEmpresa">{{ conv.nombreEmpresa }}</span>
                      <span v-else class="text-muted fst-italic">-</span>
                    </td>
                    <td>
                      <span v-if="conv.fechaFirmaConvenio">{{ conv.fechaFirmaConvenio }}</span>
                      <span v-else class="text-muted fst-italic">-</span>
                    </td>
                    <td>
                      <span v-if="conv.fechaFin">{{ conv.fechaFin }}</span>
                      <span v-else class="text-muted fst-italic">-</span>
                    </td>
                    <td>
                      <span :class="getEstadoBadgeClass(conv.estado)">
                        {{ EstadoConvenioTexto[conv.estado] }}
                      </span>
                    </td>
                    <td class="text-center">
                      <i
                        v-if="conv.refrendado"
                        class="bi bi-check-circle-fill text-success opacity-75"
                      ></i>
                      <i v-else class="bi bi-circle text-muted opacity-25"></i>
                    </td>
                    <td class="text-center">
                      <button
                        class="btn btn-sm btn-light text-primary border-0"
                        @click="VerConvenioCompleto(conv.id, conv.convenioType)"
                        title="Ver detalles"
                      >
                        <i class="bi bi-eye-fill fs-5"></i>
                      </button>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </template>
    </div>
  </Transition>
</template>

<script setup lang="ts">
import router from '@/router'
import { EstadoConvenioTexto } from '@/Types/Enums/Enums'
import type { ListConveniosDto } from '@/Types/ViewModels/ViewModels'

function VerConvenioCompleto(id: number, TypeConvenio: string) {
  if (TypeConvenio === 'marco') {
    router.push({ name: 'VistaConvenioMarco', params: { id } })
  } else {
    router.push({ name: 'VistaConvenioEspecifico', params: { id } })
  }
}

const props = defineProps<{
  convenios: ListConveniosDto
  isloading?: boolean
}>()

defineEmits(['reset-search'])

const getEstadoBadgeClass = (estado: number) => {
  switch (estado) {
    case 0:
      return 'badge bg-warning-subtle text-warning border border-warning-subtle' // Borrador/Pendiente
    case 1:
      return 'badge bg-success-subtle text-success border border-success-subtle' // Vigente
    case 2:
      return 'badge bg-secondary-subtle text-secondary border border-secondary-subtle' // Finalizado
    default:
      return 'badge bg-light text-dark border'
  }
}
</script>

<style scoped>
/* Transición suave de entrada */
.slide-fade-enter-from,
.slide-fade-leave-to {
  transform: translateX(2%);
  opacity: 0;
}

.slide-fade-enter-active,
.slide-fade-leave-active {
  transition: all 0.6s ease;
}

.slide-fade-enter-to,
.slide-fade-leave-from {
  transform: translateX(0);
  opacity: 1;
}
</style>
