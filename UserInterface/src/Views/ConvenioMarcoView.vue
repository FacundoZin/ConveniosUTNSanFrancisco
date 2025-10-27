<template>
  <div v-if="errorMessage" class="alert alert-danger alert-dismissible fade show" role="alert">
    <strong>Error:</strong> {{ errorMessage }}
    <button
      type="button"
      class="btn-close"
      data-bs-dismiss="alert"
      aria-label="Close"
      @click="errorMessage = ''"
    ></button>
  </div>

  <div class="container mt-4" v-if="convenio">
    <!-- Info del Convenio Marco -->
    <div class="card mb-4">
      <div class="card-body">
        <h4 class="card-title">
          {{ convenio.numeroConvenio || 'Sin número' }} - {{ convenio.titulo || 'Sin título' }}
        </h4>
        <p><strong>Estado:</strong> {{ convenio.estado }}</p>
        <p><strong>Fecha firma:</strong> {{ convenio.fechaFirmaConvenio || 'No disponible' }}</p>
        <p><strong>Fecha fin:</strong> {{ convenio.fechaFin || 'No disponible' }}</p>
        <p><strong>Comentario:</strong> {{ convenio.comentarioOpcional || 'Sin comentario' }}</p>
        <p>
          <strong>Número de resolución:</strong> {{ convenio.numeroResolucion || 'No disponible' }}
        </p>
        <p><strong>Refrendado:</strong> {{ convenio.refrendado ? 'Sí' : 'No' }}</p>
      </div>
    </div>

    <hr class="my-4" />

    <!-- Empresa Asociada -->
    <EmpresaCard
      v-if="convenio.empresa"
      :empresa="convenio.empresa"
      @desvincular-empresa="DesvincularEmpresa"
    />
    <div v-else class="mb-4 text-muted">No hay empresa vinculada.</div>

    <hr class="my-4" />

    <!-- Convenios Específicos -->
    <h5>Convenios Específicos</h5>
    <div class="row">
      <div v-if="convenio.conveniosEspecificos && convenio.conveniosEspecificos.length">
        <div class="col-md-4 mb-3" v-for="ce in convenio.conveniosEspecificos" :key="ce.id">
          <ConvEspecificoCard
            :convenio="ce"
            @desvincular-especifico="desvincularConvenioEspecifico"
          />
        </div>
      </div>
      <div v-else class="col-12 text-muted">No hay convenios específicos vinculados.</div>
    </div>

    <hr class="my-4" />

    <FileUploader
      :archivos="convenio.archivosAdjuntos"
      @archivo-cargado="CargarDocumento"
      @archivo-eliminado="BorrarDocumento"
      @archivo-descargado="DescargarDocumento"
    />

    <!-- Botones finales -->
    <div class="mt-4 d-flex gap-2">
      <button class="btn btn-primary" @click="editConvenio">Editar Convenio</button>
      <button class="btn btn-success" @click="CargarEspecifico">Cargar Convenio Específico</button>
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
import ConvEspecificoCard from '@/Components/ConvEspecificoCard.vue'
import EmpresaCard from '@/Components/EmpresaCard.vue'
import FileUploader from '@/Components/FileUploader.vue'
import router from '@/router'
import ApiService from '@/Services/ApiService'
import type { InfoConvenioMarcoDto, ViewArchivoDto } from '@/Types/ViewModels/ViewModels'
import { isAxiosError } from 'axios'
import { onMounted, ref, type Ref } from 'vue'
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

const convenio = ref<InfoConvenioMarcoDto | null>(null)

onMounted(async () => {
  isLoading.value = true
  try {
    const response = await ApiService.GetConvenioMarcoCompleto(id)
    isLoading.value = false
    if (response.isSuccess) {
      convenio.value = response.value
    }
  } catch (error) {
    isLoading.value = false
    toast.error('error al acceder a los datos del convenio marco', {
      position: POSITION.BOTTOM_CENTER,
    })
    if (isAxiosError(error) && error.response) {
      console.log(error.response.data, error.response.status)
    } else {
      console.log(error)
    }
  }
})

const editConvenio = () => {
  if (convenio.value) {
    router.push({
      name: 'EditConvenioMarco',
      params: { id: convenio.value.id },
    })
  }
}

const CargarEspecifico = () => {
  if (convenio.value) {
    router.push({
      name: 'CreateConvenioEspecifico',
      params: { id: convenio.value.id },
    })
  }
}

const DeleteConvenio = async () => {
  isLoading.value = true
  try {
    if (convenio.value) {
      const response = await ApiService.DeleteConvenioMarco(convenio.value.id)
      if (response.isSuccess) {
        isLoading.value = false
        toast.success(`"${convenio.value.titulo}" eliminado con éxito`)
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

const desvincularConvenioEspecifico = async (idConvenioEspecifico: number) => {
  errorMessage.value = ''
  isLoading.value = true

  const response = await ApiService.DesvincularConvenioEspecifico(id, idConvenioEspecifico)

  if (!response.isSuccess) {
    errorMessage.value = response.error.message
  }

  isLoading.value = false
}

const CargarDocumento = async (file: File, nombre: string) => {
  errorMessage.value = ''
  isLoading.value = true
  try {
    const exito = await ApiService.CargarArchivo(nombre, file, convenio.value!.id)
    isLoading.value = false
    if (exito) {
      toast.success('documento cargado con exito')
      await cargarArchivos() // vuelve a traer la lista de archivos del convenio
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
      await cargarArchivos()
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

const cargarArchivos = async (id: number):{

  isLoading.value = true;

  try {
    const response = await ApiService.GetArchivosConvMarco(id); // espera al API
    isLoading.value = false;

    if (response.isSuccess) {
      return response.value;
    } else {
      errorMessage.value = 'Error al cargar los archivos';
      return [];
    }
  } catch (error) {
    isLoading.value = false;
    console.error('Error al cargar archivos:', error);
    errorMessage.value = 'Error al cargar los archivos';
    return [];
  }
}
</script>
