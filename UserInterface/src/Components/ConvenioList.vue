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
              <th>Número Convenio</th>
              <th>Nombre Empresa</th>
              <th>Fecha de Firma</th>
              <th>Inicio de Actividades</th>
              <th>Fecha de Finalización</th>
              <th>Tipo de convenio</th>
              <th>Estado</th>
              <th>Es Acta</th>
              <th>Refrendado</th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="conv in convenios.data"
              :key="conv.id"
              class="clickable-row"
              @click="VerConvenioCompleto(conv.id, conv.convenioType)"
            >
              <td>{{ conv.titulo || 'Sin Título' }}</td>
              <td>{{ conv.numeroconvenio || '_' }}</td>
              <td>{{ conv.nombreEmpresa || '-' }}</td>
              <td>{{ conv.fechaFirmaConvenio || '-' }}</td>
              <td>{{ conv.fechaInicioActividades || '-' }}</td>
              <td>{{ conv.fechaFin || '-' }}</td>
              <td>{{ conv.convenioType }}</td>
              <td>{{ EstadoConvenioTexto[conv.estado] }}</td>
              <td>
                <span :class="conv.esActa ? 'badge bg-success' : 'badge bg-secondary'">
                  {{ conv.esActa ? 'Sí' : 'No' }}
                </span>
              </td>
              <td>
                <span :class="conv.refrendado ? 'badge bg-success' : 'badge bg-secondary'">
                  {{ conv.refrendado ? 'Sí' : 'No' }}
                </span>
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
              <th>Número Convenio</th>
              <th>Nombre Empresa</th>
              <th>Fecha de Firma</th>
              <th>Fecha de Finalización</th>
              <th>Tipo de convenio</th>
              <th>Estado</th>
              <th>Refrendado</th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="conv in convenios.data"
              :key="conv.id"
              class="clickable-row"
              @click="VerConvenioCompleto(conv.id, conv.convenioType)"
            >
              <td>{{ conv.titulo || 'Sin Título' }}</td>
              <td>{{ conv.numeroconvenio || '_' }}</td>
              <td>{{ conv.nombreEmpresa || '-' }}</td>
              <td>{{ conv.fechaFirmaConvenio || '-' }}</td>
              <td>{{ conv.fechaFin || '-' }}</td>
              <td>{{ conv.convenioType || '_' }}</td>
              <td>{{ EstadoConvenioTexto[conv.estado] }}</td>
              <td>
                <span :class="conv.refrendado ? 'badge bg-success' : 'badge bg-secondary'">
                  {{ conv.refrendado ? 'Sí' : 'No' }}
                </span>
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
