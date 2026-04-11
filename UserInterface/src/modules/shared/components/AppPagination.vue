<template>
  <nav aria-label="Navegación de páginas" v-if="totalPages > 1" class="mt-4">
    <ul class="pagination justify-content-center">
      <li class="page-item" :class="{ disabled: currentPage === 1 }">
        <button
          class="page-link shadow-sm border-0"
          @click="changePage(currentPage - 1)"
          :disabled="currentPage === 1"
        >
          <i class="bi bi-chevron-left"></i> Anterior
        </button>
      </li>

      <li
        v-for="page in pages"
        :key="page"
        class="page-item"
        :class="{ active: currentPage === page }"
      >
        <button
          class="page-link shadow-sm border-0 mx-1 rounded"
          @click="changePage(page)"
        >
          {{ page }}
        </button>
      </li>

      <li class="page-item" :class="{ disabled: currentPage === totalPages }">
        <button
          class="page-link shadow-sm border-0"
          @click="changePage(currentPage + 1)"
          :disabled="currentPage === totalPages"
        >
          Siguiente <i class="bi bi-chevron-right"></i>
        </button>
      </li>
    </ul>
  </nav>
</template>

<script setup lang="ts">
import { computed } from 'vue'

const props = defineProps<{
  currentPage: number
  totalPages: number
}>()

const emit = defineEmits<{
  (e: 'page-changed', page: number): void
}>()

const changePage = (page: number) => {
  if (page >= 1 && page <= props.totalPages && page !== props.currentPage) {
    emit('page-changed', page)
  }
}

const pages = computed(() => {
  const range = []
  const maxVisiblePages = 5
  let start = Math.max(1, props.currentPage - Math.floor(maxVisiblePages / 2))
  let end = start + maxVisiblePages - 1

  if (end > props.totalPages) {
    end = props.totalPages
    start = Math.max(1, end - maxVisiblePages + 1)
  }

  for (let i = start; i <= end; i++) {
    range.push(i)
  }
  return range
})
</script>

<style scoped>
.page-link {
  cursor: pointer;
  color: var(--bs-primary);
  transition: all 0.2s ease-in-out;
}
.page-link:hover:not(:disabled) {
  background-color: var(--bs-primary);
  color: white;
  transform: translateY(-1px);
}
.page-item.active .page-link {
  background-color: var(--bs-primary);
  color: white;
  font-weight: bold;
}
.page-item.disabled .page-link {
  color: var(--bs-secondary);
  background-color: var(--bs-light);
  cursor: not-allowed;
  opacity: 0.7;
}
</style>
