<template>
    <div class="carreras-container">
        <h5 class="mb-3">Carreras Involucradas</h5>

        <div v-if="carreras && carreras.length > 0" class="d-flex flex-wrap gap-3">

            <div v-for="carrera in carreras" :key="carrera.Id" class="card bg-light shadow-sm"
                style="min-width: 250px;">
                <div class="card-body p-3">
                    <h6 class="card-title mb-0">{{ carrera.Nombre }}</h6>
                    <small class="text-muted">ID: {{ carrera.Id }}</small>
                </div>
            </div>

        </div>
        <div v-else class="text-muted">
            No hay carreras asociadas.
        </div>
    </div>
</template>

<script setup lang="ts">
// 1. Definición del tipo para la prop
interface Carrera {
    Id: number;
    Nombre: string;
}

// 2. Definición de las props
// Se espera un array de Carrera. Puede ser opcional o nulo.
defineProps<{
    carreras: Carrera[];
}>();
</script>

<style scoped>
/* Estilos adicionales si fueran necesarios, pero las clases de Bootstrap ya hacen el trabajo */
.carreras-container {
    /* Margen inferior para separarlo de otros elementos */
    margin-bottom: 1.5rem;
}
</style>