<!-- src/Components/ConvenioList.vue -->
<template>
  <div class="convenio-list-container">
    <div v-if="convenios.length === 0" class="empty-msg">
      No hay convenios para mostrar
    </div>

    <table v-else class="convenio-table">
      <thead>
        <tr>
          <th>Numero de convenio</th>
          <th>Título</th>
          <th>Fecha de firma</th>
          <th>Fecha de vencimiento</th>
          <th>Tipo</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="conv in convenios" :key="conv.id"  @click="VerConvenioCompleto(conv.id,conv.convenioType)">
          <td>{{ conv.numeroconvenio }}</td>
          <td>{{ conv.titulo }}</td>
          <td>{{ conv.fechaFirmaConvenio }}</td>
          <td>{{ conv.fechaFin }}</td>
          <td>{{ conv.convenioType }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<style scoped>
.convenio-list-container {
  background-color: white;
  padding: 20px;
  border-radius: 10px;
}

.empty-msg {
  color: #777;
  padding: 10px;
}

.convenio-table {
  width: 100%;
  border-collapse: collapse;
}

.convenio-table th {
  background-color: #00a1e4;
  color: white;
  text-align: left;
  padding: 10px;
}

.convenio-table td {
  border: 1px solid #ddd;
  padding: 10px;
}

.convenio-table tr:hover {
  background-color: #e6f7ff;
}
</style>

<script setup lang="ts">
import router from '@/router';
import type { Convenioview } from '@/Types/Models';

function VerConvenioCompleto(id: number, TypeConvenio: string) {

  if(TypeConvenio === "marco"){
    router.push({ name:'VistaConvenioMarco', params: { id,  } })
  } 
  else {
    router.push({ name:'VistaConvenioEspecifico' , params: {id}})
  }
}

const props = defineProps<{
  convenios: Convenioview[]
}>()
</script>