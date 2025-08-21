<!-- src/Components/ConvenioList.vue -->
<template>
  <div>
    <div v-if="convenios.length === 0" class="p-4 text-gray-500">
      No hay convenios para mostrar
    </div>

    <table v-else class="w-full border-collapse">
      <thead>
        <tr class="bg-gray-200">
          <th class="border px-4 py-2 text-left">numero de convenio</th>
          <th class="border px-4 py-2 text-left">Título</th>
          <th class="border px-4 py-2 text-left">Fecha de firma</th>
          <th class="border px-4 py-2 text-left">Fecha de vencimiento</th>
          <th class="border px-4 py-2 text-left">Tipo</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="conv in convenios" :key="conv.Id" class="hover:bg-gray-100">
          <td class="border px-4 py-2">{{ conv.numeroconvenio }}</td>
          <td class="border px-4 py-2">{{ conv.Titulo }}</td>
          <td class="border px-4 py-2">{{ conv.FechaFirmaConvenio }}</td>
          <td class="border px-4 py-2">{{ conv.FechaFinConvenio }}</td>
          <td class="border px-4 py-2">{{ conv.ConvenioType }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import type { Convenioview } from '@/Types/Models';


const props = defineProps<{
  convenios: Convenioview[]
}>()
</script>