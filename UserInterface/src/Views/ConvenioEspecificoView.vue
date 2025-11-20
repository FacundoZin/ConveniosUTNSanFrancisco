<template>
  <div class="container mt-4" v-if="Convenio?.id">
    <!-- Info del Convenio Especifico -->
    <h5>Información del Convenio</h5>

<div class="card shadow-sm mb-4">
  <div class="card-body position-relative">

    <h6 class="card-title text-primary mb-3 pe-4">
      {{ Convenio.titulo || 'Sin título' }}
    </h6>

    <div class="card-text">

      <div class="d-flex align-items-center mb-2">
        <i class="bi bi-calendar-check me-2 text-muted"></i>
        <small><strong>Fecha firma:</strong> {{ Convenio.fechaFirmaConvenio || ' -' }}</small>
      </div>

      <div class="d-flex align-items-center mb-2">
        <i class="bi bi-calendar-event me-2 text-muted"></i>
        <small><strong>Fecha de inicio de actividades:</strong> {{ Convenio.fechaInicioActividades || ' -' }}</small>
      </div>

      <div class="d-flex align-items-center mb-2">
        <i class="bi bi-calendar-x me-2 text-muted"></i>
        <small><strong>Fecha fin:</strong> {{ Convenio.fechaFinConvenio || ' -' }}</small>
      </div>

      <div class="d-flex align-items-start mb-2">
        <i class="bi bi-chat-left-text me-2 text-muted"></i>
        <small class="text-break"><strong>Comentario:</strong> {{ Convenio.comentarioOpcional || ' -' }}</small>
      </div>

      <div class="d-flex align-items-center mb-2">
        <i class="bi bi-hash me-2 text-muted"></i>
        <small><strong>Número de convenio:</strong> {{ Convenio.numeroconvenio || ' -' }}</small>
      </div>

      <div class="d-flex align-items-center mb-2">
        <i class="bi bi-hash me-2 text-muted"></i>
        <small><strong>Número de resolución:</strong> {{ Convenio.numeroResolucion || ' -' }}</small>
      </div>

      <div class="mt-3 pt-2 border-top d-flex flex-wrap gap-2">
        <span class="badge bg-info text-dark border">
          {{ EstadoConvenioTexto[Convenio.estado] }}
        </span>

        <span v-if="Convenio.refrendado" class="badge bg-success text-white border">
          Refrendado
        </span>
      </div>

    </div>

  </div>
</div>

    <hr class="my-4" />

    <!-- Empresa Asociada -->
    <h5>Informacion De Empresa</h5>
    <EmpresaCard
      v-if="Convenio.empresa"
      :empresa="Convenio.empresa"
      @desvincular-empresa="DesvincularEmpresa"
    />
    <div v-else class="col-12">
      <div class="card shadow-sm p-3 text-center" style="background-color: #f8f9fa">
        <div class="card-body">
          <h6 class="card-title mb-2">Sin empresa</h6>
          <p class="text-muted mb-0">Aún no hay una empresa asociada a este convenio especifico.</p>
        </div>
      </div>
    </div>

    <hr class="my-4" />

    <!-- convenio asociado -->
    <h5>Convenio Marco Asociado</h5>
    <div class="mb-4">
      <div v-if="Convenio.convenioMarco">
        <ConvMarcoCard
          :convenio="Convenio.convenioMarco"
          @desvincular-marco="desvincularConvenioMarco"
          class="mb-3"
        />
      </div>
      <div v-else class="col-12">
        <div class="card shadow-sm p-3 text-center" style="background-color: #f8f9fa">
          <div class="card-body">
            <h6 class="card-title mb-2">Sin convenio marco</h6>
            <p class="text-muted mb-0">
              Aún no hay un convenio marco asociado a este convenio especifico.
            </p>
          </div>
        </div>
      </div>
    </div>

    <hr class="my-4" />

    <!-- involucrados asociado -->
    <h5>Involucrados Asociados</h5>
    <div class="row">
      <div
        v-if="Convenio?.involucrados && Convenio.involucrados.length > 0"
        class="d-flex flex-wrap gap-3"
      >
        <InvolucradosViewCard
          v-for="involucrado in Convenio.involucrados"
          :key="involucrado.id"
          :involucrado="involucrado"
        />
      </div>
      <div v-else class="col-12 text-muted">
        <div class="card shadow-sm p-3 text-center" style="background-color: #f8f9fa">
          <div class="card-body">
            <h6 class="card-title mb-2">Sin involucrados</h6>
            <p class="text-muted mb-0">Aún no hay involucrados asociados a este convenio.</p>
          </div>
        </div>
      </div>
    </div>

    <hr class="my-4" />

    <!-- carreras involucradas -->
    <h5>Carreras Involucradas</h5>
    <div v-if="Convenio.carrerasInvolucradas && Convenio.carrerasInvolucradas.length > 0">
      <CarrerasCardList :carreras="Convenio.carrerasInvolucradas" />
    </div>
    <div v-else class="text-muted">
      <div class="card shadow-sm p-3 text-center" style="background-color: #f8f9fa">
        <div class="card-body">
          <h6 class="card-title mb-2">Sin carreras involucradas</h6>
          <p class="text-muted mb-0">Aún no hay carreras involucradas en este convenio</p>
        </div>
      </div>
    </div>

    <hr class="my-4" />

    <FileUploader
      :archivos="Convenio?.documentosAdjuntos"
      @archivo-cargado="CargarDocumento"
      @archivo-eliminado="BorrarDocumento"
      @archivo-descargado="DescargarDocumento"
    />

    <div
      v-if="errorMessage"
      class="alert alert-danger alert-dismissible fade show gap-3"
      role="alert"
    >
      <strong>Error:</strong> {{ errorMessage }}
      <button
        type="button"
        class="btn-close"
        data-bs-dismiss="alert"
        aria-label="Close"
        @click="errorMessage = ''"
      ></button>
    </div>

    <hr class="my-4" />

    <!-- Botones finales -->
    <div class="mt-5 d-flex gap-3 justify-content-center">
      <button class="btn btn-primary" @click="editConvenio">Editar Convenio</button>
      <button class="btn btn-danger" @click="DeleteConvenio">Eliminar Convenio</button>
    </div>
  </div>

  <!--  mensaje mientras carga -->
  <div v-else class="loader-overlay d-flex justify-content-center align-items-center">
    <div class="spinner-border text-primary" role="status" style="width: 3rem; height: 3rem">
      <span class="visually-hidden">Cargando...</span>
    </div>
  </div>
</template>

<script setup lang="ts">
import CarrerasCardList from '@/Components/CarrerasCardList.vue'
import ConvMarcoCard from '@/Components/ConvMarcoCard.vue'
import EmpresaCard from '@/Components/EmpresaCard.vue'
import FileUploader from '@/Components/FileUploader.vue'
import InvolucradosViewCard from '@/Components/InvolucradosViewCard.vue'
import router from '@/router'
import ApiService from '@/Services/ApiService'
import { EstadoConvenioTexto } from '@/Types/Enums/Enums'
import type { InfoConvenioEspecificoDto } from '@/Types/ViewModels/ViewModels'
import { isAxiosError } from 'axios'
import { onMounted, ref } from 'vue'
import { useRoute } from 'vue-router'
import { POSITION, useToast } from 'vue-toastification'

const isLoading = ref(false)
const errorMessage = ref<string>('')
const toast = useToast()
const route = useRoute()
const idparam = route.params.id
let id = 0

if (Array.isArray(idparam)) {
  id = parseInt(idparam[0])
} else {
  id = parseInt(idparam)
}

const Convenio = ref<InfoConvenioEspecificoDto | null>(null)

onMounted(async () => {
  isLoading.value = true
  try {
    const response = await ApiService.GetConvenioEspecificoCompleto(id)
    isLoading.value = false
    if (response.isSuccess) {
      Convenio.value = response.value
    }
  } catch (error) {
    isLoading.value = false
    errorMessage.value = 'error al acceder a los datos del convenio especifico'
    if (isAxiosError(error) && error.response) {
      console.log(error.response.data, error.response.status)
    } else {
      console.log(error)
    }
  }
})

const editConvenio = () => {
  if (Convenio.value) {
    router.push({
      name: 'EditConvenioEspecifico',
      params: { id: Convenio.value.id },
    })
  }
}

const DeleteConvenio = async () => {
  isLoading.value = true
  try {
    if (Convenio.value) {
      const response = await ApiService.DeleteConvenioEspecifico(Convenio.value.id)
      if (response.isSuccess) {
        isLoading.value = false
        toast.success(`"${Convenio.value.titulo}" eliminado con éxito`)
        router.push({ name: 'ListaConvenios' })
      }
    }
  } catch (error) {
    isLoading.value = false
    toast.error(`Error al eliminar el convenio`, { position: POSITION.BOTTOM_CENTER })
    if (isAxiosError(error) && error.response) {
      console.log(`Error: ${error.response.data.message}, ${error.response.data}`)
    } else {
      console.log(`Lo sentimos, algo ha salido mal. ${error}`)
    }
  }
}

const DesvincularEmpresa = async () => {
  errorMessage.value = ''
  isLoading.value = true

  const response = await ApiService.DesvincularEmpresaDeMarco(id)
  if (!response.isSuccess) {
    errorMessage.value = response.error.message
  }
  isLoading.value = false
}

const desvincularConvenioMarco = async () => {
  errorMessage.value = ''
  isLoading.value = true

  const response = await ApiService.DesvincularConvenioMarco(id)

  if (!response.isSuccess) {
    errorMessage.value = response.error.message
  }

  isLoading.value = false
}

const CargarDocumento = async ({ file, nombre }: { file: File; nombre: string }) => {
  errorMessage.value = ''
  isLoading.value = true

  try {
    const ArchivoCargado = await ApiService.CargarArchivoToMarco(nombre, file, Convenio.value!.id)

    isLoading.value = false

    if (ArchivoCargado) {
      toast.success('documento cargado con exito')

      const convenio = Convenio.value!
      convenio.documentosAdjuntos ??= []
      convenio.documentosAdjuntos = [...convenio.documentosAdjuntos, ArchivoCargado]
    } else {
      errorMessage.value = 'Error al cargar el docuemnto'
    }
  } catch (error) {
    isLoading.value = false
    errorMessage.value = 'Error al cargar el docuemnto'
  }
}

const BorrarDocumento = async (id: number) => {
  errorMessage.value = ''
  isLoading.value = true
  try {
    const exito = await ApiService.EliminarArchivo(id)
    isLoading.value = false
    if (exito) {
      toast.success('documento eliminado correctamente')
      const convenio = Convenio.value

      if (convenio && convenio.documentosAdjuntos) {
        convenio.documentosAdjuntos = convenio.documentosAdjuntos.filter(
          (archivo) => archivo.idArchivo !== id,
        )
      }
    } else {
      errorMessage.value = 'ocurrio un error al eliminar el documento'
    }
  } catch (error) {
    isLoading.value = false
    errorMessage.value = 'ocurrio un error al eliminar el documento'
  }
}

const DescargarDocumento = async (id: number, nombre: string) => {
  errorMessage.value = ''
  isLoading.value = true
  try {
    await ApiService.DescargarArchivo(id, nombre)
    isLoading.value = false
  } catch (error) {
    isLoading.value = false
    console.error('Error al descargar el archivo:', error)
    errorMessage.value = 'ocurrio un error al descargar el documento'
  }
}
</script>
