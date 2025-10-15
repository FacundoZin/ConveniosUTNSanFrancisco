<template>
    <div class="container mt-4 position-relative">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h3 class="text-primary">Cargar Convenio Especifico</h3>

            <button v-if="ConvenioCreado" @click="irAlConvenio"
                class="btn btn-outline-success d-flex align-items-center gap-2" title="Ver convenio creado">
                <i class="bi bi-arrow-right-circle"></i>
                Ver Convenio
            </button>
        </div>

        <div v-if="errorMensaje" class="alert alert-danger" role="alert">
            {{ errorMensaje }}
        </div>

        <form @submit.prevent="guardarConvenio" class="row g-3">

            <div class="col-md-6">
                <label class="form-label">Número de Convenio</label>
                <input v-model="ConvenioEspecificoRequest.insertConvenioDto.numeroConvenio" type="text"
                    class="form-control" />
            </div>

            <div class="col-md-6">
                <label class="form-label">Título</label>
                <input v-model="ConvenioEspecificoRequest.insertConvenioDto.titulo" type="text" class="form-control" />
            </div>

            <div class="col-md-6">
                <label class="form-label">Fecha de Firma</label>
                <input v-model="ConvenioEspecificoRequest.insertConvenioDto.fechaFirmaConvenio" type="date"
                    class="form-control" />
            </div>

            <div class="col-md-6">
                <label class="form-label">Fecha de Inicio de Actividades</label>
                <input v-model="ConvenioEspecificoRequest.insertConvenioDto.fechaInicioActividades" type="date"
                    class="form-control" />
            </div>

            <div class="col-md-6">
                <label class="form-label">Fecha de Fin</label>
                <input v-model="ConvenioEspecificoRequest.insertConvenioDto.fechaFinConvenio" type="date"
                    class="form-control" />
            </div>

            <div class="col-md-6">
                <label class="form-label">Carrera</label>
                <select v-model="ConvenioEspecificoRequest.idCarreras" class="form-select" multiple>
                    <option value="" disabled>Seleccionar</option>
                    <option v-for="carrera in Carreras" :key="carrera.Id" :value="carrera.Id">
                        {{ carrera.Nombre }}
                    </option>
                </select>
            </div>

            <div class="col-12">
                <label class="form-label">Comentario Opcional</label>
                <textarea v-model="ConvenioEspecificoRequest.insertConvenioDto.comentarioOpcional" class="form-control"
                    rows="2"></textarea>
            </div>

            <div class="col-md-4">
                <label class="form-label">Estado</label>
                <select v-model="ConvenioEspecificoRequest.insertConvenioDto.estado" class="form-select" required>
                    <option value="" disabled>Seleccionar...</option>
                    <option value="Vigente">Vigente</option>
                    <option value="Finalizado">Finalizado</option>
                    <option value="Suspendido">Suspendido</option>
                </select>
            </div>

            <div class="col-md-4">
                <label class="form-label">Número de Resolución</label>
                <input v-model="ConvenioEspecificoRequest.insertConvenioDto.numeroResolucion" type="text"
                    class="form-control" />
            </div>

            <div class="col-md-4">
                <label class="form-label">Refrendado</label>
                <select v-model="ConvenioEspecificoRequest.insertConvenioDto.refrendado" class="form-select" required>
                    <option :value="true">Sí</option>
                    <option :value="false">No</option>
                </select>
            </div>

            <div class="col-md-4">
                <label class="form-label">Es acta</label>
                <select v-model="ConvenioEspecificoRequest.insertConvenioDto.esActa" class="form-select" required>
                    <option :value="true">Sí</option>
                    <option :value="false">No</option>
                </select>
            </div>

            <hr class="my-4" />

            <div class="col-12 mb-3">
                <div class="form-check form-switch">
                    <input class="form-check-input" type="checkbox" id="switchNuevaEmpresa"
                        v-model="cargarNuevaEmpresa" />
                    <label class="form-check-label" for="switchNuevaEmpresa">
                        Cargar nueva empresa
                    </label>
                </div>
            </div>

            <div v-if="!cargarNuevaEmpresa" class="col-md-6">
                <label class="form-label">Seleccionar Empresa</label>
                <select v-model="empresaForm.id" class="form-select" required>
                    <option value="" disabled>Seleccionar...</option>
                    <option v-for="empresa in empresas" :key="empresa.idEmpresa" :value="empresa.idEmpresa">
                        {{ empresa.nombreEmpresa }}
                    </option>
                </select>
            </div>

            <div v-else class="col-12 border rounded p-3 bg-light">
                <h5 class="mb-3">Nueva Empresa</h5>

                <div class="row g-3">
                    <div class="col-md-6">
                        <label class="form-label">Nombre</label>
                        <input v-model="empresaForm.nombre" type="text" class="form-control" required />
                    </div>

                    <div class="col-md-6">
                        <label class="form-label">Razón Social</label>
                        <input v-model="empresaForm.razonSocial" type="text" class="form-control" />
                    </div>

                    <div class="col-md-6">
                        <label class="form-label">CUIT</label>
                        <input v-model="empresaForm.cuit" type="text" class="form-control" />
                    </div>

                    <div class="col-md-6">
                        <label class="form-label">Dirección</label>
                        <input v-model="empresaForm.direccion" type="text" class="form-control" />
                    </div>

                    <div class="col-md-6">
                        <label class="form-label">Teléfono</label>
                        <input v-model="empresaForm.telefono" type="text" class="form-control" />
                    </div>

                    <div class="col-md-6">
                        <label class="form-label">Email</label>
                        <input v-model="empresaForm.email" type="email" class="form-control" />
                    </div>
                </div>
            </div>

            <hr class="my-4" />

            <div class="col-12 involucrados-section">
                <h3 class="involucrados-title text-primary mb-3">👥 Involucrados</h3>

                <InvolucradoForm @agregar="agregarInvolucrado" />

                <div class="involucrados-list mt-3">
                    <InvolucradosCard v-for="(inv, idx) in ConvenioEspecificoRequest.insertInvolucradosDto" :key="idx"
                        :involucrado="inv" @eliminar="eliminarInvolucrado(idx)" />
                </div>
            </div>

            <div class="col-12 mt-4">
                <button type="submit" class="btn btn-primary">Cargar Convenio</button>
            </div>
        </form>
    </div>
</template>

<script setup lang="ts">
import InvolucradoForm from '@/Components/InvolucradoForm.vue'
import InvolucradosCard from '@/Components/InvolucradosCard.vue'
import { useCreateConvenioEspecificoForm } from '@/Composables/CreateConvenioEspecificoForm'
import '@/Styles/FormCargaConvEspecifico.css'
import type { InsertInvolucradosDto } from '@/Types/Involucrados/InsertInvolucrados'
import { useRouter } from 'vue-router'
import { useToast } from 'vue-toastification'

const router = useRouter()

const {
    ConvenioEspecificoRequest,
    errorMensaje,
    empresas,
    Carreras,
    cargarNuevaEmpresa,
    ConvenioCreado,
    empresaForm,
    involucradosForm,
    submitForm: submitFormLogic,
    resetForm
} = useCreateConvenioEspecificoForm()

const toast = useToast();

const agregarInvolucrado = (nuevo: InsertInvolucradosDto) => {
    involucradosForm.value.push(nuevo)
}

const eliminarInvolucrado = (index: number) => {
    involucradosForm.value.splice(index, 1)
}

const guardarConvenio = async () => {
    const result = await submitFormLogic()

    if (result) {
        ConvenioCreado.value = result
        resetForm()
        toast.success('Convenio cargado con éxito')
    }
}

const irAlConvenio = () => {
    if (ConvenioCreado.value) {
        router.push({ name: 'VistaConvenioMarco', params: { id: ConvenioCreado.value.ID } })
    }
}

</script>