<!-- src/Components/ConvenioList.vue -->
<template>
  <div class="mt-6 bg-white shadow-md rounded-lg overflow-hidden">
    <div v-if="convenios.length === 0" class="p-6 text-center text-gray-500">
      No hay convenios para mostrar.
    </div>

    <table v-else class="min-w-full divide-y divide-gray-200">
      <thead class="bg-gray-50">
        <tr>
          <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Número de Convenio</th>
          <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Título</th>
          <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Fecha de Firma</th>
          <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Fecha de Vencimiento</th>
          <th scope="col" class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Tipo</th>
        </tr>
      </thead>
      <tbody class="bg-white divide-y divide-gray-200">
        <tr v-for="conv in convenios" :key="conv.Id" class="hover:bg-gray-50 transition-colors">
          <td class="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{{ conv.numeroconvenio }}</td>
          <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-700">{{ conv.Titulo }}</td>
          <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-700">{{ conv.FechaFirmaConvenio }}</td>
          <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-700">{{ conv.FechaFinConvenio }}</td>
          <td class="px-6 py-4 whitespace-nowrap">
            <span :class="['px-3 py-1 inline-flex text-xs leading-5 font-semibold rounded-full', conv.ConvenioType === 'Marco' ? 'bg-blue-100 text-blue-800' : 'bg-green-100 text-green-800']">
              {{ conv.ConvenioType }}
            </span>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import type { Convenioview } from '@/Types/Models';


defineProps<{
  convenios: Convenioview[]
}>()
</script>