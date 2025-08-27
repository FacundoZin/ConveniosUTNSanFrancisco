<template>
    <div class="involucrado-card">
        <h4>{{ involucrado.nombre }} {{ involucrado.apellido }}</h4>
        <p>📧 {{ involucrado.email }}</p>
        <p>📞 {{ involucrado.telefono }}</p>
        <p>🎓 Legajo: {{ involucrado.legajo || 'N/A' }}</p>
        <p class="rol">Rol: {{ rolesMap[involucrado.rolInvolucrado] }}</p>
        <button @click="$emit('eliminar')" class="delete-btn">✕</button>
    </div>
</template>

<style scoped>
.involucrado-card {
    background: #fff;
    border: 1px solid #e5e7eb;
    border-radius: 12px;
    padding: 1rem;
    position: relative;
    box-shadow: 0 1px 5px rgba(0, 0, 0, 0.08);
}

.involucrado-card h4 {
    font-weight: 600;
    margin-bottom: 0.5rem;
    color: #0b2242;
}

.involucrado-card p {
    font-size: 0.9rem;
    color: #444;
    margin: 0.2rem 0;
}

.rol {
    font-weight: 600;
    color: #143d73;
}

.delete-btn {
    position: absolute;
    top: 8px;
    right: 8px;
    background: none;
    border: none;
    color: #c53030;
    font-size: 1.2rem;
    cursor: pointer;
}

.delete-btn:hover {
    color: #9b2c2c;
}
</style>


<script setup lang="ts">
import type { InsertInvolucradosDto } from '@/Types/Api.Interface';

defineProps<{ involucrado: InsertInvolucradosDto }>()

const rolesMap = ['Docente', 'Alumno', 'Secretario', 'Externo']

</script>
