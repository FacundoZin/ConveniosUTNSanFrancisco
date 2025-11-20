<template>
    <div class="card shadow-sm h-100" style="width: 18rem;">
        <div class="card-body position-relative">
            <button 
                @click="$emit('eliminar')" 
                class="btn-close position-absolute top-0 end-0 m-2" 
                aria-label="Close"
            ></button>
            
            <h5 class="card-title text-primary mb-3">{{ involucrado.nombre }} {{ involucrado.apellido }}</h5>
            
            <div class="card-text">
                <div class="d-flex align-items-center mb-2">
                    <i class="bi bi-envelope me-2 text-muted"></i>
                    <small class="text-truncate" :title="involucrado.email || ''">{{ involucrado.email || 'Sin email' }}</small>
                </div>
                
                <div class="d-flex align-items-center mb-2">
                    <i class="bi bi-telephone me-2 text-muted"></i>
                    <small>{{ involucrado.telefono || 'Sin teléfono' }}</small>
                </div>
                
                <div class="d-flex align-items-center mb-2">
                    <i class="bi bi-person-badge me-2 text-muted"></i>
                    <small>Legajo: {{ involucrado.legajo || 'N/A' }}</small>
                </div>

                <div class="mt-3 pt-2 border-top">
                    <span class="badge bg-light text-dark border">
                        {{ rolesMap[involucrado.rolInvolucrado] }}
                    </span>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import type { InsertInvolucradosDto } from '@/Types/Involucrados/InsertInvolucrados';

defineProps<{ involucrado: InsertInvolucradosDto }>()

const rolesMap = ['Docente', 'Alumno', 'Secretario', 'Externo']
</script>
