<template>
  <div v-if="isloading" class="d-flex justify-content-center my-5">
    <div class="spinner-border text-primary" role="status">
      <span class="visually-hidden">Cargando...</span>
    </div>
  </div>

  <Transition name="slide-fade">
    <div v-if="!isloading && convenios.Type !== ''" class="convenios-container container-animada">
      <div
        v-if="
          (convenios.Type !== 'ambos' && convenios.data.length === 0) ||
          (convenios.Type === 'ambos' &&
            convenios.conveniosMarcos.length === 0 &&
            convenios.conveniosEspecificos.length === 0)
        "
        class="empty-state"
      >
        <div class="empty-state-icon">
          <i class="bi bi-inbox"></i>
        </div>
        <h4 class="empty-state-title">No se encontraron convenios</h4>
        <p class="empty-state-text text-muted">
          No hay convenios {{ convenios.Type === 'marco' ? 'marco' : 'específicos' }} que coincidan
          con los criterios de búsqueda.
        </p>
        <div class="empty-state-suggestions">
          <p class="text-muted mb-2"><i class="bi bi-lightbulb me-2"></i>Sugerencias:</p>
          <ul class="list-unstyled text-muted mb-0">
            <li><i class="bi bi-check2 me-2 text-primary"></i>Verifica los filtros aplicados</li>
            <li>
              <i class="bi bi-check2 me-2 text-primary"></i>Intenta con otros parámetros de búsqueda
            </li>
            <li>
              <i class="bi bi-check2 me-2 text-primary"></i>Busca en
              {{ convenios.Type === 'marco' ? 'Convenios Específicos' : 'Convenios Marco' }}
            </li>
          </ul>
        </div>

        <div class="mt-4">
          <button class="btn btn-primary" @click="$emit('reset-search')">
            <i class="bi bi-arrow-counterclockwise me-2"></i>Volver a realizar la búsqueda
          </button>
        </div>
      </div>

      <template v-else>
        <!-- TABLA ESPECIFICOS (Solo si es especifico o ambos) -->
        <table
          v-if="
            convenios.Type === 'especifico' ||
            (convenios.Type === 'ambos' && convenios.conveniosEspecificos.length > 0)
          "
          class="table table-striped table-hover table-bordered shadow-sm mb-5"
        >
          <caption class="caption-top text-center fs-5 p-2 bg-light rounded-top">
            <strong>Convenios Específicos</strong>
          </caption>
          <thead class="table-dark">
            <tr>
              <th>Título</th>
              <th>N° Convenio</th>
              <th>Empresa</th>
              <th>Firma</th>
              <th>Inicio</th>
              <th>Fin</th>
              <th>Estado</th>
              <th class="text-center">Acta</th>
              <th class="text-center">Refrendado</th>
              <th class="text-center">Acciones</th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="conv in convenios.Type === 'ambos'
                ? convenios.conveniosEspecificos
                : convenios.data"
              :key="conv.id"
              class="align-middle"
            >
              <td>{{ conv.titulo || 'Sin Título' }}</td>
              <td>{{ conv.numeroconvenio || '_' }}</td>
              <td>{{ conv.nombreEmpresa || '-' }}</td>
              <td>{{ conv.fechaFirmaConvenio || '-' }}</td>
              <td>{{ conv.fechaInicioActividades || '-' }}</td>
              <td>{{ conv.fechaFin || '-' }}</td>
              <td>
                <span :class="getEstadoBadgeClass(conv.estado)">
                  {{ EstadoConvenioTexto[conv.estado] }}
                </span>
              </td>
              <td class="text-center">
                <i v-if="conv.esActa" class="bi bi-check-circle-fill text-success fs-5"></i>
                <i v-else class="bi bi-x-circle-fill text-secondary fs-5"></i>
              </td>
              <td class="text-center">
                <i v-if="conv.refrendado" class="bi bi-check-circle-fill text-success fs-5"></i>
                <i v-else class="bi bi-x-circle-fill text-secondary fs-5"></i>
              </td>
              <td class="text-center">
                <button
                  class="btn btn-sm btn-outline-primary"
                  @click="VerConvenioCompleto(conv.id, conv.convenioType)"
                  title="Ver detalles"
                >
                  <i class="bi bi-eye"></i>
                </button>
              </td>
            </tr>
          </tbody>
        </table>

        <!-- TABLA MARCOS (Solo si es marco o ambos) -->
        <table
          v-if="
            convenios.Type === 'marco' ||
            (convenios.Type === 'ambos' && convenios.conveniosMarcos.length > 0)
          "
          class="table table-striped table-hover table-bordered shadow-sm"
        >
          <caption class="caption-top text-center fs-5 p-2 bg-light rounded-top">
            <strong>Convenios Marco</strong>
          </caption>
          <thead class="table-dark">
            <tr>
              <th>Título</th>
              <th>N° Convenio</th>
              <th>Empresa</th>
              <th>Firma</th>
              <th>Fin</th>
              <th>Estado</th>
              <th class="text-center">Refrendado</th>
              <th class="text-center">Acciones</th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="conv in convenios.Type === 'ambos'
                ? convenios.conveniosMarcos
                : convenios.data"
              :key="conv.id"
              class="align-middle"
            >
              <td>{{ conv.titulo || 'Sin Título' }}</td>
              <td>{{ conv.numeroconvenio || '_' }}</td>
              <td>{{ conv.nombreEmpresa || '-' }}</td>
              <td>{{ conv.fechaFirmaConvenio || '-' }}</td>
              <td>{{ conv.fechaFin || '-' }}</td>
              <td>
                <span :class="getEstadoBadgeClass(conv.estado)">
                  {{ EstadoConvenioTexto[conv.estado] }}
                </span>
              </td>
              <td class="text-center">
                <i v-if="conv.refrendado" class="bi bi-check-circle-fill text-success fs-5"></i>
                <i v-else class="bi bi-x-circle-fill text-secondary fs-5"></i>
              </td>
              <td class="text-center">
                <button
                  class="btn btn-sm btn-outline-primary"
                  @click="VerConvenioCompleto(conv.id, conv.convenioType)"
                  title="Ver detalles"
                >
                  <i class="bi bi-eye"></i>
                </button>
              </td>
            </tr>
          </tbody>
        </table>
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
      return 'badge bg-warning text-dark' // Borrador/Pendiente
    case 1:
      return 'badge bg-success' // Vigente
    case 2:
      return 'badge bg-secondary' // Finalizado
    default:
      return 'badge bg-light text-dark border'
  }
}
</script>

<style scoped>
.clickable-row {
  cursor: pointer;
}

.caption-top {
  caption-side: top;
}

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

/* Hace que el contenedor ocupe casi todo el ancho */
.container-animada {
  width: 95%;
  margin: 0 auto;
  transition: width 0.6s ease;
}

.slide-fade-enter-from .container-animada {
  width: 60%;
}

.slide-fade-enter-to .container-animada {
  width: 95%;
}

/* Estilos para el estado vacío - Estilo minimalista institucional */
.empty-state {
  background: #ffffff;
  border: 1px solid #dee2e6;
  border-radius: 8px;
  padding: 3rem 2rem;
  text-align: center;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.05);
  max-width: 600px;
  margin: 2rem auto;
}

.empty-state-icon {
  font-size: 4rem;
  margin-bottom: 1.5rem;
  color: #6c757d;
}

.empty-state-title {
  font-size: 1.5rem;
  font-weight: 600;
  margin-bottom: 1rem;
  color: #212529;
}

.empty-state-text {
  font-size: 1rem;
  margin-bottom: 1.5rem;
  line-height: 1.6;
}

.empty-state-suggestions {
  background: #f8f9fa;
  border: 1px solid #e9ecef;
  border-radius: 6px;
  padding: 1.5rem;
  margin-top: 1.5rem;
  text-align: left;
}

.empty-state-suggestions p {
  margin-bottom: 0.8rem;
  font-weight: 600;
  font-size: 0.95rem;
}

.empty-state-suggestions ul {
  margin: 0;
}

.empty-state-suggestions li {
  padding: 0.5rem 0;
  font-size: 0.9rem;
}

/* Responsive */
@media (max-width: 768px) {
  .empty-state {
    padding: 2rem 1.5rem;
  }

  .empty-state-icon {
    font-size: 3rem;
  }

  .empty-state-title {
    font-size: 1.25rem;
  }

  .empty-state-text {
    font-size: 0.95rem;
  }
}
</style>
