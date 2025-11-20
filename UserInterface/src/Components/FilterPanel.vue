<template>
  <div
    :class="['offcanvas', 'offcanvas-end', { show: isPanelOpen }]"
    tabindex="-1"
    id="offcanvasRight"
    aria-labelledby="offcanvasRightLabel"
  >
    <div class="offcanvas-header">
      <h5 class="offcanvas-title" id="offcanvasRightLabel">
        Buscar convenios {{ typeOfConvenio }}s por:
      </h5>
      <button
        type="button"
        class="btn-close text-reset"
        @click="closePanel"
        aria-label="Cerrar"
      ></button>
    </div>
    <div class="offcanvas-body">
      <div v-if="typeOfConvenio === 'marco'">
        <ul class="list-group list-group-flush">
          <li class="list-group-item list-group-item-action d-flex align-items-center gap-2" @click="selectFilter(KeyFilters.ByTitulo)">
            <i class="bi bi-type"></i> Título
          </li>
          <li class="list-group-item list-group-item-action d-flex align-items-center gap-2" @click="selectFilter(KeyFilters.ByEmpresa)">
            <i class="bi bi-building"></i> Empresa
          </li>
          <li class="list-group-item list-group-item-action d-flex align-items-center gap-2" @click="selectFilter(KeyFilters.ByEstado)">
            <i class="bi bi-flag"></i> Estado
          </li>
          <li class="list-group-item list-group-item-action d-flex align-items-center gap-2" @click="selectFilter(KeyFilters.ByNumeroResolucion)">
            <i class="bi bi-file-text"></i> Número de Resolución
          </li>
          <li class="list-group-item list-group-item-action d-flex align-items-center gap-2" @click="selectFilter(KeyFilters.ByNumeroConvenio)">
            <i class="bi bi-hash"></i> Número de Convenio
          </li>
          <li class="list-group-item list-group-item-action d-flex align-items-center gap-2" @click="selectFilter(KeyFilters.ByCarrera)">
            <i class="bi bi-mortarboard"></i> Carrera Involucrada
          </li>
          <li class="list-group-item list-group-item-action d-flex align-items-center gap-2" @click="selectFilter(KeyFilters.ByFechaFirma)">
            <i class="bi bi-pen"></i> Fecha de Firma
          </li>
          <li class="list-group-item list-group-item-action d-flex align-items-center gap-2" @click="selectFilter(KeyFilters.ByFechaFin)">
            <i class="bi bi-calendar-x"></i> Fecha de Finalización
          </li>
          <li class="list-group-item list-group-item-action d-flex align-items-center gap-2" @click="selectFilter(KeyFilters.ByAntiguedadDto)">
            <i class="bi bi-hourglass-split"></i> Antigüedad
          </li>

          <li
            class="list-group-item list-group-item-action d-flex align-items-center gap-2 text-danger"
            @click="HandleDirectSearch(props.typeOfConvenio, KeyFilters.ByProximosAvencer)"
          >
            <i class="bi bi-exclamation-triangle"></i> Próximos a vencer
          </li>
          <li
            class="list-group-item list-group-item-action d-flex align-items-center gap-2 text-success"
            @click="HandleDirectSearch(props.typeOfConvenio, KeyFilters.ByIsRefrendado)"
          >
            <i class="bi bi-check2-circle"></i> Refrendados
          </li>
        </ul>
      </div>
      <div v-else-if="typeOfConvenio === 'especifico'">
        <ul class="list-group list-group-flush">
          <li class="list-group-item list-group-item-action d-flex align-items-center gap-2" @click="selectFilter(KeyFilters.ByTitulo)">
            <i class="bi bi-type"></i> Título
          </li>
          <li class="list-group-item list-group-item-action d-flex align-items-center gap-2" @click="selectFilter(KeyFilters.ByEmpresa)">
            <i class="bi bi-building"></i> Empresa
          </li>
          <li class="list-group-item list-group-item-action d-flex align-items-center gap-2" @click="selectFilter(KeyFilters.ByEstado)">
            <i class="bi bi-flag"></i> Estado
          </li>
          <li class="list-group-item list-group-item-action d-flex align-items-center gap-2" @click="selectFilter(KeyFilters.ByNumeroResolucion)">
            <i class="bi bi-file-text"></i> Número de Resolución
          </li>
          <li class="list-group-item list-group-item-action d-flex align-items-center gap-2" @click="selectFilter(KeyFilters.ByNumeroConvenio)">
            <i class="bi bi-hash"></i> Número de Convenio
          </li>
          <li class="list-group-item list-group-item-action d-flex align-items-center gap-2" @click="selectFilter(KeyFilters.ByCarrera)">
            <i class="bi bi-mortarboard"></i> Carrera Involucrada
          </li>
          <li class="list-group-item list-group-item-action d-flex align-items-center gap-2" @click="selectFilter(KeyFilters.ByFechaFirma)">
            <i class="bi bi-pen"></i> Fecha de Firma
          </li>
          <li class="list-group-item list-group-item-action d-flex align-items-center gap-2" @click="selectFilter(KeyFilters.ByFechaFin)">
            <i class="bi bi-calendar-x"></i> Fecha de Finalización
          </li>
          <li class="list-group-item list-group-item-action d-flex align-items-center gap-2" @click="selectFilter(KeyFilters.ByAntiguedadDto)">
            <i class="bi bi-hourglass-split"></i> Antigüedad
          </li>

          <li
            class="list-group-item list-group-item-action d-flex align-items-center gap-2 text-success"
            @click="HandleDirectSearch(props.typeOfConvenio, KeyFilters.ByIsRefrendado)"
          >
            <i class="bi bi-check2-circle"></i> Refrendados
          </li>
          <li
            class="list-group-item list-group-item-action d-flex align-items-center gap-2 text-info"
            @click="HandleDirectSearch(props.typeOfConvenio, KeyFilters.ByIsActa)"
          >
            <i class="bi bi-journal-text"></i> Actas
          </li>
          <li
            class="list-group-item list-group-item-action d-flex align-items-center gap-2 text-danger"
            @click="HandleDirectSearch(props.typeOfConvenio, KeyFilters.ByProximosAvencer)"
          >
            <i class="bi bi-exclamation-triangle"></i> Próximos a vencer
          </li>
        </ul>
      </div>
      <div v-else>
        <p class="text-muted text-center mt-4">Selecciona un tipo de convenio para ver los filtros.</p>
      </div>
    </div>
  </div>
  <div v-if="isPanelOpen" class="offcanvas-backdrop fade show" @click="closePanel"></div>
</template>

<script setup lang="ts">
import { KeyFilters } from '@/Common/KeyFilter'
import type {
  IByIsActaParams,
  IByIsRefrendadoParams,
  IByProximosAvencerParams,
  IConvenioQueryObject,
} from '@/Types/Filters'
import { defineEmits, defineProps } from 'vue'

const props = defineProps<{
  isPanelOpen: boolean
  typeOfConvenio: 'marco' | 'especifico' | ''
  QueryObject: IConvenioQueryObject
}>()

const emit = defineEmits(['close-panel', 'filter-selected', 'DirectSearch'])

const closePanel = () => {
  emit('close-panel')
}

const selectFilter = (filterKey: string) => {
  emit('filter-selected', filterKey)
}

const HandleDirectSearch = (convenioType: string, filterKey: string) => {
  selectFilter(filterKey)
  if (filterKey === KeyFilters.ByIsRefrendado) {
    const filtro: IByIsRefrendadoParams = {
      refrendado: true,
      convenioType: convenioType,
    }
    props.QueryObject.ByIsRefrendado = filtro
  }
  if (filterKey === KeyFilters.ByIsActa) {
    const filtro: IByIsActaParams = {
      IsActa: true,
      convenioType: convenioType,
    }
    props.QueryObject.ByIsActa = filtro
  } else if (filterKey === KeyFilters.ByProximosAvencer) {
    const filtro: IByProximosAvencerParams = {
      convenioType: convenioType,
    }
    props.QueryObject.ByProximosAvencer = filtro
  }
  emit('DirectSearch')
}
</script>

<style scoped>
/* Estilos para el overlay/sombra cuando el panel está abierto */
.offcanvas-backdrop {
  position: fixed;
  top: 0;
  left: 0;
  z-index: 1040;
  /* Z-index estándar de Bootstrap para overlays */
  width: 100vw;
  height: 100vh;
  background-color: #000;
  opacity: 0.5;
  transition: opacity 0.15s linear;
}

/* Ajuste del offcanvas para que se vea bien */
.offcanvas {
  max-width: 400px;
  /* Limita el ancho del panel */
  background-color: #f8f9fa;
  /* Color de fondo claro para que se distinga */
}

.list-group-item {
  cursor: pointer;
  /* Hace que se vea clickable */
}
</style>
