<template>
  <div :class="['offcanvas', 'offcanvas-end', { 'show': isPanelOpen }]" tabindex="-1" id="offcanvasRight"
    aria-labelledby="offcanvasRightLabel">
    <div class="offcanvas-header">
      <h5 class="offcanvas-title" id="offcanvasRightLabel">Buscar</h5>
      <button type="button" class="btn-close text-reset" @click="closePanel" aria-label="Cerrar"></button>
    </div>
    <div class="offcanvas-body">
      <div v-if="typeOfConvenio === 'marco'">
        <ul class="list-group">
          <div v-for="value in KeyFilters"></div>
          <li class="list-group-item" @click="selectFilter(KeyFilters.ByTitulo)">Titulo</li>
          <li class="list-group-item" @click="selectFilter(KeyFilters.ByEmpresa)">Empresa</li>
          <li class="list-group-item" @click="selectFilter(KeyFilters.ByEstado)">Estado</li>
          <li class="list-group-item" @click="selectFilter(KeyFilters.ByNumeroResolucion)">Numero de Resolucion</li>
          <li class="list-group-item" @click="selectFilter(KeyFilters.ByNumeroConvenio)">Numero de Convenio</li>
          <li class="list-group-item" @click="selectFilter(KeyFilters.ByCarrera)">Carrera Involucrada</li>
          <li class="list-group-item" @click="selectFilter(KeyFilters.ByFechaFirma)">Fecha de Firma</li>
          <li class="list-group-item" @click="selectFilter(KeyFilters.ByFechaFin)">Fecha de Finalizacion</li>
          <li class="list-group-item" @click="selectFilter(KeyFilters.ByAntiguedadDto)">Antiguedad</li>

          <li class="list-group-item" @click="HandleDirectSearch(props.typeOfConvenio, KeyFilters.ByProximosAvencer)">
            Proximos a vencer</li>
          <li class="list-group-item" @click="HandleDirectSearch(props.typeOfConvenio, KeyFilters.ByIsRefrendado)">
            Refrendados</li>
        </ul>
      </div>
      <div v-else-if="typeOfConvenio === 'especifico'">
        <ul class="list-group">
          <li class="list-group-item" @click="selectFilter(KeyFilters.ByTitulo)">Titulo</li>
          <li class="list-group-item" @click="selectFilter(KeyFilters.ByEmpresa)">Empresa</li>
          <li class="list-group-item" @click="selectFilter(KeyFilters.ByEstado)">Estado</li>
          <li class="list-group-item" @click="selectFilter(KeyFilters.ByNumeroResolucion)">Numero de Resolucion</li>
          <li class="list-group-item" @click="selectFilter(KeyFilters.ByNumeroConvenio)">Numero de Convenio</li>
          <li class="list-group-item" @click="selectFilter(KeyFilters.ByCarrera)">Carrera Involucrada</li>
          <li class="list-group-item" @click="selectFilter(KeyFilters.ByFechaFirma)">Fecha de Firma</li>
          <li class="list-group-item" @click="selectFilter(KeyFilters.ByFechaFin)">Fecha de Finalizacion</li>
          <li class="list-group-item" @click="selectFilter(KeyFilters.ByAntiguedadDto)">Antiguedad</li>

          <li class="list-group-item" @click="HandleDirectSearch(props.typeOfConvenio, KeyFilters.ByIsRefrendado)">
            Refrendados</li>
          <li class="list-group-item" @click="HandleDirectSearch(props.typeOfConvenio, KeyFilters.ByIsActa)">Actas</li>
          <li class="list-group-item" @click="HandleDirectSearch(props.typeOfConvenio, KeyFilters.ByProximosAvencer)">
            Proximos a vencer</li>
        </ul>
      </div>
      <div v-else>
        <p>Selecciona un tipo de convenio para ver los filtros.</p>
      </div>
    </div>
  </div>
  <div v-if="isPanelOpen" class="offcanvas-backdrop fade show" @click="closePanel"></div>
</template>

<script setup lang="ts">
import { KeyFilters } from '@/Common/KeyFilter';
import type { IByIsActaParams, IByIsRefrendadoParams, IByProximosAvencerParams, IConvenioQueryObject } from '@/Types/Filters';
import { defineEmits, defineProps } from 'vue';

const props = defineProps<{
  isPanelOpen: boolean;
  typeOfConvenio: 'marco' | 'especifico' | '';
  QueryObject: IConvenioQueryObject
}>();

const emit = defineEmits(['close-panel', 'filter-selected', 'DirectSearch']);

const closePanel = () => {
  emit('close-panel');
};

const selectFilter = (filterKey: string) => {
  emit('filter-selected', filterKey);
};

const HandleDirectSearch = (convenioType: string, filterKey: string) => {
  selectFilter(filterKey);
  if (filterKey === KeyFilters.ByIsRefrendado) {
    const filtro: IByIsRefrendadoParams = {
      refrendado: true,
      convenioType: convenioType,
    };
    props.QueryObject.ByIsRefrendado = filtro;
  }
  if (filterKey === KeyFilters.ByIsActa) {
    const filtro: IByIsActaParams = {
      IsActa: true,
      convenioType: convenioType,
    };
    props.QueryObject.ByIsActa = filtro;
  }
  else if (filterKey === KeyFilters.ByProximosAvencer) {
    const filtro: IByProximosAvencerParams = {
      convenioType: convenioType,
    };
    props.QueryObject.ByProximosAvencer = filtro;
  }
  emit('DirectSearch');
};
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