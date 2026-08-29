<template>
  <div class="searchable-select-container position-relative w-100" ref="containerRef">
    <!-- Botón / Trigger similar a form-select -->
    <div
      class="form-select d-flex align-items-center justify-content-between cursor-pointer"
      :class="{ disabled: disabled, 'is-invalid': isInvalid }"
      @click="toggleDropdown"
      tabindex="0"
      @keydown.space.prevent="toggleDropdown"
      @keydown.down.prevent="openDropdown"
      @keydown.escape="closeDropdown"
    >
      <span :class="{ 'text-muted': !selectedOption }">
        {{ selectedOption ? selectedOption.label : placeholder }}
      </span>
      <i class="bi bi-chevron-down ms-2 text-secondary small"></i>
    </div>

    <!-- Menú desplegable -->
    <div
      v-if="isOpen"
      class="dropdown-menu show w-100 shadow-sm p-2 mt-1 border"
      style="max-height: 280px; overflow-y: auto; z-index: 1055;"
    >
      <!-- Campo de Búsqueda -->
      <div class="mb-2 position-relative">
        <input
          ref="searchInputRef"
          v-model="searchTerm"
          type="text"
          class="form-control form-control-sm pe-4"
          placeholder="Buscar..."
          @click.stop
          @keydown.escape="closeDropdown"
        />
        <i
          v-if="searchTerm"
          class="bi bi-x-circle-fill position-absolute top-50 end-0 translate-middle-y me-2 text-muted cursor-pointer"
          @click.stop="searchTerm = ''"
        ></i>
      </div>

      <!-- Lista de Opciones -->
      <ul class="list-unstyled mb-0">
        <li
          v-if="filteredOptions.length === 0"
          class="text-muted p-2 text-center small"
        >
          No se encontraron resultados
        </li>

        <li
          v-for="option in filteredOptions"
          :key="option.id"
          class="dropdown-item rounded d-flex align-items-center justify-content-between p-2 cursor-pointer small"
          :class="{ active: option.id === modelValue }"
          @click="selectOption(option)"
        >
          <span class="text-truncate">{{ option.label }}</span>
          <i v-if="option.id === modelValue" class="bi bi-check2"></i>
        </li>
      </ul>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, nextTick, onBeforeUnmount, onMounted, ref } from 'vue'

export interface SelectOption {
  id: number | string
  label: string
}

const props = withDefaults(
  defineProps<{
    options: SelectOption[]
    modelValue: number | string | null
    placeholder?: string
    disabled?: boolean
    isInvalid?: boolean
  }>(),
  {
    placeholder: 'Seleccionar...',
    disabled: false,
    isInvalid: false,
  }
)

const emit = defineEmits<{
  (e: 'update:modelValue', value: number | string | null): void
}>()

const isOpen = ref(false)
const searchTerm = ref('')
const containerRef = ref<HTMLElement | null>(null)
const searchInputRef = ref<HTMLInputElement | null>(null)

const selectedOption = computed(() => {
  return props.options.find((opt) => opt.id === props.modelValue) || null
})

const filteredOptions = computed(() => {
  if (!searchTerm.value.trim()) return props.options
  const term = searchTerm.value.toLowerCase().trim()
  return props.options.filter((opt) =>
    opt.label.toLowerCase().includes(term)
  )
})

const toggleDropdown = () => {
  if (props.disabled) return
  if (isOpen.value) {
    closeDropdown()
  } else {
    openDropdown()
  }
}

const openDropdown = () => {
  if (props.disabled) return
  isOpen.value = true
  searchTerm.value = ''
  nextTick(() => {
    searchInputRef.value?.focus()
  })
}

const closeDropdown = () => {
  isOpen.value = false
  searchTerm.value = ''
}

const selectOption = (option: SelectOption) => {
  emit('update:modelValue', option.id)
  closeDropdown()
}

const handleClickOutside = (event: MouseEvent) => {
  if (containerRef.value && !containerRef.value.contains(event.target as Node)) {
    closeDropdown()
  }
}

onMounted(() => {
  document.addEventListener('click', handleClickOutside)
})

onBeforeUnmount(() => {
  document.removeEventListener('click', handleClickOutside)
})
</script>

<style scoped>
.searchable-select-container .form-select {
  user-select: none;
  cursor: pointer;
  background-image: none;
}

.searchable-select-container .form-select.disabled {
  background-color: var(--bs-secondary-bg);
  opacity: 0.65;
  cursor: not-allowed;
}

.cursor-pointer {
  cursor: pointer;
}

.dropdown-item.active {
  background-color: var(--bs-primary);
  color: white;
}
</style>
