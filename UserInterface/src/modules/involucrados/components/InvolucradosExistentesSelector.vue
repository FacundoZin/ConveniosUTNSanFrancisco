<template>
  <div class="dropdown w-100">
    <button class="btn btn-light border w-100 text-start" type="button" data-bs-toggle="dropdown">
      Seleccionar...
    </button>

    <ul class="dropdown-menu w-100" data-bs-auto-close="outside">
      <li v-if="isLoading" class="text-center py-2">
        <div class="spinner-border spinner-border-sm text-primary" role="status">
          <span class="visually-hidden">Cargando...</span>
        </div>
      </li>

      <li v-if="!isLoading && involucrados.length === 0" class="dropdown-item text-muted">
        No hay involucrados disponibles
      </li>

      <li v-if="errorMensaje" class="dropdown-item text-danger">
        {{ errorMensaje }}
      </li>

      <li v-for="involucrado in involucrados" :key="involucrado.id" @click.stop>
        <label class="dropdown-item d-flex align-items-center gap-2">
          <input type="checkbox" :value="involucrado.id" :checked="isSelected(involucrado.id)"
            @change="toggleSelection(involucrado.id)" />
          {{ involucrado.fullName }}
        </label>
      </li>
    </ul>
  </div>

  <div v-if="selectedCount > 0" class="mt-2 text-success">
    <i class="bi bi-check-circle me-2"></i>
    {{ selectedCount }} involucrado{{ selectedCount > 1 ? 's' : '' }} seleccionado{{
      selectedCount > 1 ? 's' : ''
    }}
  </div>
</template>

<script setup lang="ts">
import ApiService from '@/Services/ApiService'
import type { ComboBoxInvolucradosDto } from '@/Types/Involucrados/ComboBoxInvolucradosDto'
import { computed, onMounted, ref } from 'vue'

interface Props {
  modelValue: number[] | null
  idConvenioExcluded?: number
}

const props = defineProps<Props>()
const emit = defineEmits<{
  (e: 'update:modelValue', value: number[] | null): void
}>()

const involucrados = ref<ComboBoxInvolucradosDto[]>([])
const isLoading = ref(false)
const errorMensaje = ref('')

const selectedCount = computed(() => {
  return props.modelValue?.length || 0
})

const isSelected = (id: number): boolean => {
  return props.modelValue?.includes(id) || false
}

const toggleSelection = (id: number) => {
  const currentSelection = props.modelValue || []

  if (currentSelection.includes(id)) {
    // Remover de la selección
    const newSelection = currentSelection.filter((selectedId) => selectedId !== id)
    emit('update:modelValue', newSelection.length > 0 ? newSelection : null)
  } else {
    // Agregar a la selección
    emit('update:modelValue', [...currentSelection, id])
  }
}

const fetchInvolucrados = async () => {
  isLoading.value = true
  errorMensaje.value = ''

  let result
  if (props.idConvenioExcluded) {
    result = await ApiService.GetInvolucradosDisponibles(props.idConvenioExcluded)
  } else {
    result = await ApiService.GetAllInvolucrados()
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
