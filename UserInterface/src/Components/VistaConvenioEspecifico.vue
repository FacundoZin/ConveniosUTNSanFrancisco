<script setup lang="ts">
import ApiService from '@/Services/ApiService';
import type { ConvenioEspecificoCompleto } from '@/Types/Models';
import { isAxiosError } from 'axios';
import { onMounted, ref } from 'vue';
import { useRoute } from 'vue-router';


const router = useRoute();
const idparam= router.params.id;
let id = 0;

if (Array.isArray(idparam)) {
  id = parseInt(idparam[0]);
} else {
  id = parseInt(idparam)
}

const ConvenioMarco = ref<ConvenioEspecificoCompleto | null>(null);
const errorMensaje = ref('');

onMounted( async() => {
    try{
        const response = await ApiService.GetConvenioEspecificoCompleto(id);
        ConvenioMarco.value = response.data;
    }catch(error){
        if(isAxiosError(error) && error.response){
        errorMensaje.value = ` ${error.response.data}`;
    }else{
        errorMensaje.value = "Lo sentimos, algo a salido mal"
    }
    }
})
</script>

<template>
  <div class="involucrados-container">
    <h3 class="section-title">Involucrados</h3>
    <div class="involucrados-list">
      <div 
        v-for="involucrado in convenio.involucrados" 
        :key="involucrado.id" 
        class="involucrado-card"
      >
        <h4 class="involucrado-nombre">{{ involucrado.nombre }}</h4>
        <p class="involucrado-rol">{{ involucrado.rol }}</p>
      </div>
    </div>
  </div>
</template>

<style scoped>
.involucrados-container {
  margin-top: 1.5rem;
}

.section-title {
  font-size: 1.2rem;
  font-weight: 600;
  margin-bottom: 1rem;
  color: #2c3e50;
}

.involucrados-list {
  display: flex;
  flex-wrap: wrap;
  gap: 1rem;
}

.involucrado-card {
  background-color: #f9fafb;
  border: 1px solid #e5e7eb;
  border-radius: 0.75rem;
  padding: 1rem;
  width: 220px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.08);
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}

.involucrado-card:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 8px rgba(0,0,0,0.12);
}

.involucrado-nombre {
  font-size: 1rem;
  font-weight: 600;
  color: #111827;
  margin-bottom: 0.25rem;
}

.involucrado-rol {
  font-size: 0.9rem;
  color: #6b7280;
}
</style>