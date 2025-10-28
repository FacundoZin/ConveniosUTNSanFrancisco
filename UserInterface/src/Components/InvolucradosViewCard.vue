<template>
    <div class="involucrado-card">
        <h4>{{ involucrado.nombre }} {{ involucrado.apellido }}</h4>
        <p>📧 {{ involucrado.email }}</p>
        <p>📞 {{ involucrado.telefono }}</p>
        <p>🎓 Legajo: {{ involucrado.legajo || 'N/A' }}</p>
        <p class="rol">Rol: {{ involucrado.rolInvolucrado }}</p>
    </div>
</template>


<script setup lang="ts">
import '@/Styles/InvolucradosCard.css';
import type { InvolucradosDto } from '@/Types/ViewModels/ViewModels';

defineProps<{ involucrado: InvolucradosDto }>()


</script>
