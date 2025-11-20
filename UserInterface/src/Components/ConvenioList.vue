<template>
  <div v-if="isloading" class="d-flex justify-content-center my-5">
    <div class="spinner-border text-primary" role="status">
      <span class="visually-hidden">Cargando...</span>
    </div>
  </div>

  <Transition name="slide-fade">
    <div v-if="!isloading && convenios.Type !== ''" class="convenios-container container-animada">
      <div v-if="convenios.data.length === 0" class="alert alert-info text-center" role="alert">
        No hay convenios que coincidan con la búsqueda.
      </div>

      <template v-else>
        <table
          v-if="convenios.Type === 'especifico'"
          class="table table-striped table-hover table-bordered shadow-sm"
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
              v-for="conv in convenios.data"
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

        <table
          v-if="convenios.Type === 'marco'"
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
              v-for="conv in convenios.data"
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

const getEstadoBadgeClass = (estado: number) => {
  switch (estado) {
    case 0: return 'badge bg-warning text-dark' // Borrador/Pendiente
    case 1: return 'badge bg-success' // Vigente
    case 2: return 'badge bg-secondary' // Finalizado
    default: return 'badge bg-light text-dark border'
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
</style>
