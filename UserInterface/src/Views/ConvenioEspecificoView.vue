<template>
  <div v-if="errorMessage" class="alert alert-danger alert-dismissible fade show" role="alert">
    <strong>Error:</strong> {{ errorMessage }}
    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"
      @click="errorMessage = ''"></button>
  </div>

  <div class="container mt-4" v-if="Convenio?.id">
    <!-- Info del Convenio Marco -->
    <div class="card mb-4">
      <div class="card-body">
        <h4 class="card-title">
          {{ Convenio.numeroConvenio || 'Sin número' }} - {{ Convenio.titulo || 'Sin título' }}
        </h4>
        <p><strong>Estado:</strong> {{ EstadoConvenioTexto[Convenio.estado] }}</p>
        <p><strong>Fecha firma:</strong> {{ Convenio.fechaFirmaConvenio || 'No disponible' }}</p>
        <p><strong>Fecha de inicio de actividades:</strong> {{ Convenio.fechaInicioActividades || 'No disponible' }}</p>
        <p><strong>Fecha fin:</strong> {{ Convenio.fechaFinConvenio || 'No disponible' }}</p>
        <p><strong>Comentario:</strong> {{ Convenio.comentarioOpcional || 'Sin comentario' }}</p>
        <p>
          <strong>Número de resolución:</strong> {{ Convenio.numeroResolucion || 'No disponible' }}
        </p>
        <p><strong>Refrendado:</strong> {{ Convenio.refrendado ? 'Sí' : 'No' }}</p>
      </div>
    </div>

    <hr class="my-4" />

    <!-- Empresa Asociada -->
    <EmpresaCard v-if="Convenio.empresa" :empresa="Convenio.empresa" @desvincular-empresa="DesvincularEmpresa" />
    <div v-else class="mb-4 text-muted">No hay empresa vinculada.</div>

    <hr class="my-4" />

    <!-- convenio asociado -->
    <h5>Convenio Marco Asociado</h5>
    <div class="mb-4">
      <div v-if="Convenio.convenioMarco">
        <ConvMarcoCard :convenio="Convenio.convenioMarco" @desvincular-marco="desvincularConvenioMarco" class="mb-3" />
      </div>
      <div v-else class="text-muted">No hay un convenio marco asociado.</div>
    </div>

    <hr class="my-4" />

    <!-- involucrados asociado -->
    <h5>Involucrados Asociados</h5>
    <div class="row">
      <div v-if="Convenio?.involucrados && Convenio.involucrados.length > 0" class="d-flex flex-wrap gap-3">
        <InvolucradosViewCard v-for="involucrado in Convenio.involucrados" :key="involucrado.id"
          :involucrado="involucrado" />
      </div>
      <div v-else class="col-12 text-muted">No hay personas involucradas vinculadas.</div>
    </div>

    <hr class="my-4" />

    <hr class="my-4" />

    <!-- carreras involucradas -->
    <h5>Carreras Involucradas</h5>
    <div v-if="Convenio.carrerasInvolucradas && Convenio.carrerasInvolucradas.length > 0">
      <CarrerasCardList :carreras="Convenio.carrerasInvolucradas" />
    </div>
    <div v-else class="text-muted">No hay carreras involucradas.</div>

    <hr class="my-4" />

    <FileUploader :archivos="Convenio?.documentosAdjuntos" @archivo-cargado="CargarDocumento"
      @archivo-eliminado="BorrarDocumento" @archivo-descargado="DescargarDocumento" />

    <!-- Botones finales -->
    <div class="mt-4 d-flex gap-2">
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

const CargarDocumento = async (file: File, nombre: string) => {
  errorMessage.value = ''
  isLoading.value = true
  try {
    const ArchivoCargado = await ApiService.CargarArchivo(nombre, file, Convenio.value!.id)
    isLoading.value = false
    if (ArchivoCargado) {
      toast.success('documento cargado con exito')
      const convenio = Convenio.value!

      if (!convenio.documentosAdjuntos) {
        convenio.documentosAdjuntos = []
      }

      convenio.documentosAdjuntos.push(ArchivoCargado)
    } else {
      errorMessage.value = 'Error al cargar el docuemnto'
    }
  } catch (error) {
    isLoading.value = false
    console.error('Error al cargar documento:', error)
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
          (archivo) => archivo.idArchivo !== id
        )
      }
    } else {
      errorMessage.value = 'ocurrio un error al eliminar el documento'
    }
  } catch (error) {
    isLoading.value = false
    console.error('Error al eliminar documento:', error)
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
