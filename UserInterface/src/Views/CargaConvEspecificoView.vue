<template>
    <div class="convenio-container">
        <h2 class="convenio-title">Cargar Convenio Específico</h2>

        <!-- FORM PRINCIPAL -->
        <form @submit.prevent="guardarConvenio" class="convenio-form">
            <div class="form-row">
                <div class="form-group">
                    <label>Número de Convenio</label>
                    <input v-model="convenio.numeroConvenio" type="number" required />
                </div>

                <div class="form-group">
                    <label>Título</label>
                    <input v-model="convenio.titulo" type="text" required />
                </div>
            </div>

            <div class="form-row">
                <div class="form-group">
                    <label>Fecha Firma</label>
                    <input v-model="convenio.fechaFirmaConvenio" type="date" required />
                </div>

                <div class="form-group">
                    <label>Inicio Actividades</label>
                    <input v-model="convenio.fechaInicioActividades" type="date" required />
                </div>

                <div class="form-group">
                    <label>Fin Convenio</label>
                    <input v-model="convenio.fechaFinConvenio" type="date" required />
                </div>
            </div>

            <div class="form-group">
                <label>Comentario (opcional)</label>
                <textarea v-model="convenio.comentarioOpcional" rows="3"></textarea>
            </div>
        </form>

        <!-- INVOLUCRADOS -->
        <div class="involucrados-section">
            <h3 class="involucrados-title">Involucrados</h3>
            <InvolucradoForm @agregar="agregarInvolucrado" />

            <div class="involucrados-list">
                <InvolucradosCard v-for="(inv, idx) in involucrados" :key="idx" :involucrado="inv"
                    @eliminar="eliminarInvolucrado(idx)" />
            </div>
        </div>

        <button class="guardar-btn" @click="guardarConvenio">Guardar Convenio</button>
    </div>
</template>

<script setup lang="ts">
import InvolucradoForm from '@/Components/InvolucradoForm.vue'
import InvolucradosCard from '@/Components/InvolucradosCard.vue'
import ApiService from '@/Services/ApiService'
import '@/Styles/FormCargaConvEspecifico.css'
import type { InsertConvenioEspecificoDto, InsertInvolucradosDto } from '@/Types/Api.Interface'
import { isAxiosError } from 'axios'
import { ref } from 'vue'
import { useRoute } from 'vue-router'
import { useToast } from 'vue-toastification'

const route = useRoute()
const GetIdConvenioMarco = async (): Promise<number> => {
    return Number(route.params.id);
}

const convenio = ref<InsertConvenioEspecificoDto>({
    numeroConvenio: 0,
    titulo: '',
    fechaFirmaConvenio: '',
    fechaInicioActividades: '',
    fechaFinConvenio: '',
    convenioMarcoId: 0,
    comentarioOpcional: ''
})

const involucrados = ref<InsertInvolucradosDto[]>([])
const toast = useToast();

const agregarInvolucrado = (nuevo: InsertInvolucradosDto) => {
    involucrados.value.push(nuevo)
}

const eliminarInvolucrado = (index: number) => {
    involucrados.value.splice(index, 1)
}

const guardarConvenio = async () => {

    convenio.value.convenioMarcoId = await GetIdConvenioMarco();
    try {

        const request = {
            convenioDto: convenio.value,
            involucradosDto: involucrados.value
        }

        await ApiService.CreateConvenioEspecifico(request);
        console.log(request.convenioDto.fechaFinConvenio,
            request.convenioDto.fechaInicioActividades, request.convenioDto.fechaFirmaConvenio);

        toast.success("Convenio cargado correctamente.")

    } catch (ex) {
        toast.error("Error al cargar el convenio.")
        if (isAxiosError(ex)) {
            console.log("Error en la solicitud:", ex.response?.data + ex.response?.status);
        } else {
            console.log("Error inesperado:", ex);
        }
    }
};

</script>