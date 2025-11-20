<template>
  <div class="convenios-especificos-section">
    <div v-if="convenios && convenios.length > 0">
      <div class="table-responsive">
        <table class="table table-striped table-hover align-middle">
          <thead class="table-primary">
            <tr>
              <th>N° Convenio</th>
              <th>Título</th>
              <th>Empresa</th>
              <th>Fecha Fin</th>
              <th>Estado</th>
              <th>Tipo</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="convenio in convenios" :key="convenio.id">
              <td class="text-nowrap">{{ convenio.numeroconvenio || 'N/A' }}</td>
              <td>{{ convenio.titulo || 'Sin título' }}</td>
              <td>{{ convenio.nombreEmpresa || 'Desconocida' }}</td>
              <td class="text-nowrap">{{ convenio.fechaFin || 'Indefinida' }}</td>
              <td>
                <span
                  :class="{
                    'badge rounded-pill': true,
                    'bg-secondary': convenio.estado === 0,
                    'bg-success': convenio.estado === 1,
                    'bg-danger': convenio.estado === 2,
                  }"
                >
                  {{
                    convenio.estado === 1
                      ? 'Vigente'
                      : convenio.estado === 2
                        ? 'Finalizado'
                        : 'Borrador'
                  }}
                </span>
              </td>
              <td>
                <span
                  :class="{
                    'badge bg-info text-dark': !convenio.esActa,
                    'badge bg-secondary': convenio.esActa,
                  }"
                >
                  {{ convenio.esActa ? 'Acta' : 'Convenio' }}
                </span>
              </td>

              <td class="text-nowrap">
                <button
                  type="button"
                  @click="$emit('ir-a-convenio', convenio.id)"
                  class="btn btn-sm btn-outline-primary me-2"
                  title="Ir al Convenio Específico"
                >
                  <i class="bi bi-box-arrow-up-right"></i>
                </button>

                <button
                  type="button"
                  @click="$emit('desvincular', convenio.id, convenio.titulo || '(sin nombre)')"
                  class="btn btn-sm btn-outline-danger"
                  title="Desvincular convenio específico"
                >
                  <i class="bi bi-x-circle"></i>
                </button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>

    <div v-else class="card shadow-sm p-3 text-center" style="background-color: #f8f9fa">
      <div class="card-body">
        <h6 class="card-title mb-2">Sin convenios específicos</h6>
        <p class="text-muted mb-0">
          Aún no hay convenios específicos asociados a este convenio marco.
        </p>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import type { ConvenioEspecificoDto } from '@/Types/ViewModels/ViewModels'

defineProps<{
  convenios: ConvenioEspecificoDto[] | undefined
}>()

defineEmits<{
  (e: 'ir-a-convenio', id: number): void
  (e: 'desvincular', id: number, tituloConvenio: string): void
}>()
</script>

<style scoped>
.convenios-especificos-section {
  width: 100%;
}
</style>
