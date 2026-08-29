<template>
  <div class="dropdown w-100">
    <button class="btn btn-light border w-100 text-start d-flex align-items-center justify-content-between" type="button" data-bs-toggle="dropdown">
      <span>{{ selectedCount > 0 ? `${selectedCount} seleccionado(s)` : 'Seleccionar involucrados...' }}</span>
      <i class="bi bi-chevron-down small text-muted"></i>
    </button>

    <ul class="dropdown-menu w-100 shadow-sm p-2" data-bs-auto-close="outside" style="max-height: 320px; overflow-y: auto;">
      <!-- Campo de Búsqueda Pegajoso -->
      <li class="px-1 mb-2 position-sticky top-0 bg-white z-1">
        <input
          v-model="searchTerm"
          type="text"
          class="form-control form-control-sm"
          placeholder="Buscar por nombre o legajo..."
          @click.stop
        />
      </li>

      <li v-if="isLoading" class="text-center py-2">
        <div class="spinner-border spinner-border-sm text-primary" role="status">
          <span class="visually-hidden">Cargando...</span>
        </div>
      </li>

      <li v-if="!isLoading && filteredInvolucrados.length === 0" class="dropdown-item text-muted text-center small">
        No se encontraron involucrados
      </li>

      <li v-if="errorMensaje" class="dropdown-item text-danger small">
        {{ errorMensaje }}
      </li>

      <li v-for="involucrado in filteredInvolucrados" :key="involucrado.id" @click.stop>
        <label class="dropdown-item d-flex align-items-center gap-2 rounded cursor-pointer small">
          <input type="checkbox" :value="involucrado.id" :checked="isSelected(involucrado.id)"
            @change="toggleSelection(involucrado.id)" />
          <span>{{ involucrado.fullName }}</span>
        </label>
      </li>
    </ul>
  </div>

  <div v-if="selectedCount > 0" class="mt-2 text-success small">
    <i class="bi bi-check-circle me-1"></i>
    {{ selectedCount }} involucrado{{ selectedCount > 1 ? 's' : '' }} seleccionado{{
      selectedCount > 1 ? 's' : ''
    }}
  </div>
</template>

<script setup lang="ts">
import InvolucradoService from '@/modules/involucrados/services/InvolucradoService'
import type { ComboBoxInvolucradosDto } from '@/Types/Involucrados/ComboBoxInvolucradosDto'
import { computed, onMounted, ref } from 'vue'

interface Props {
  modelValue: number[] | null
  idConvenioExcluded?: number
}

const props = defineProps<Props>()
const emit = defineEmits<{
  (e: 'update:modelValue', value: number[] | null): void
  (e: 'agregar-area', idCarrera: number): void
}>()

const involucrados = ref<ComboBoxInvolucradosDto[]>([])
const isLoading = ref(false)
const errorMensaje = ref('')
const searchTerm = ref('')

const filteredInvolucrados = computed(() => {
  if (!searchTerm.value.trim()) return involucrados.value
  const term = searchTerm.value.toLowerCase().trim()
  return involucrados.value.filter((inv) =>
    inv.fullName.toLowerCase().includes(term)
  )
})

const selectedCount = computed(() => {
  return props.modelValue?.length || 0
})

const isSelected = (id: number): boolean => {
  return props.modelValue?.includes(id) || false
}

const toggleSelection = (id: number) => {
  const currentSelection = props.modelValue || []

  if (currentSelection.includes(id)) {
    const newSelection = currentSelection.filter((selectedId) => selectedId !== id)
    emit('update:modelValue', newSelection.length > 0 ? newSelection : null)
  } else {
    emit('update:modelValue', [...currentSelection, id])

    const involucrado = involucrados.value.find((i) => i.id === id)
    if (involucrado?.idCarrera != null && involucrado.idCarrera !== 0) {
      emit('agregar-area', involucrado.idCarrera)
    }
  }
}

const fetchInvolucrados = async () => {
  isLoading.value = true
  errorMensaje.value = ''

  let result
  if (props.idConvenioExcluded) {
    result = await InvolucradoService.GetInvolucradosDisponibles(props.idConvenioExcluded)
  } else {
    result = await InvolucradoService.GetAllInvolucrados()
  }

  isLoading.value = false

  if (result.isSuccess) {
    involucrados.value = result.value
  } else {
    errorMensaje.value = result.error?.message || 'Error al cargar involucrados'
  }
}

onMounted(() => {
  fetchInvolucrados()
})
</script>
