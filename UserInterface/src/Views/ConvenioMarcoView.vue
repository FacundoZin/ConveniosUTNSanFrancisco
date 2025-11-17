<template>
  <div class="container mt-4" v-if="Convenio?.id">
    <!-- Info del Convenio Marco -->
    <h5>Informacion del convenio</h5>
    <div class="card mb-4 bg-light">
      <div class="card-body">
        <h4 class="card-title">
          {{ Convenio.titulo || 'Sin título' }}
        </h4>
        <p><strong>Estado:</strong> {{ EstadoConvenioTexto[Convenio.estado] }}</p>
        <p><strong>Fecha firma:</strong> {{ Convenio.fechaFirmaConvenio || '-' }}</p>
        <p><strong>Fecha fin:</strong> {{ Convenio.fechaFin || ' -' }}</p>
        <p>
          <strong>Comentario:</strong>
          {{ Convenio.comentarioOpcional || ' -' }}
        </p>
        <p>
          <strong>Número de resolución:</strong>
          {{ Convenio.numeroResolucion || ' -' }}
        </p>
        <p>
          <strong>Número de convenio:</strong>
          {{ Convenio.numeroConvenio || ' -' }}
        </p>
        <p><strong>Refrendado:</strong> {{ Convenio.refrendado ? 'Sí' : 'No' }}</p>
      </div>
    </div>

    <hr class="my-4" />

    <!-- Empresa Asociada -->
    <h5>Informacion de la empresa asociada</h5>
    <EmpresaCard
      v-if="Convenio.empresa"
      :empresa="Convenio.empresa"
      @desvincular-empresa="DesvincularEmpresa"
    />
    <div v-else class="col-12">
      <div class="card shadow-sm p-3 text-center" style="background-color: #f8f9fa">
        <div class="card-body">
          <p class="text-muted mb-0">Aún no hay una empresa asociada a este convenio marco.</p>
        </div>
      </div>
    </div>

    <hr class="my-4" />

    <!-- Convenios Específicos -->
    <h5>Convenios Específicos</h5>
    <div class="row">
      <div v-if="Convenio.conveniosEspecificos && Convenio.conveniosEspecificos.length > 0">
        <div class="col-md-4 mb-3" v-for="ce in Convenio.conveniosEspecificos" :key="ce.id">
          <ConvEspecificoCard
            :convenio="ce"
            @desvincular-especifico="desvincularConvenioEspecifico"
          />
        </div>
      </div>
      <div v-else class="col-12">
        <div class="card shadow-sm p-3 text-center" style="background-color: #f8f9fa">
          <div class="card-body">
            <h6 class="card-title mb-2">Sin convenios específicos</h6>
            <p class="text-muted mb-0">
              Aún no hay convenios específicos vinculados a este convenio marco.
            </p>
          </div>
        </div>
      </div>
    </div>

    <hr class="my-4" />

    <FileUploader
      :archivos="Convenio?.archivosAdjuntos"
      @archivo-cargado="CargarDocumento"
      class="mb-3"
      @archivo-eliminado="BorrarDocumento"
      @archivo-descargado="DescargarDocumento"
    />

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

    <hr class="my-4" />

    <!-- Botones finales -->
    <div class="mt-5 d-flex gap-3 justify-content-center">
      <button class="btn btn-primary" @click="editConvenio">Editar Convenio</button>
      <button class="btn btn-primary" @click="CargarEspecifico">Cargar Convenio Específico</button>
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
import { EstadoConvenioTexto } from '@/Types/Enums/Enums'
import type { InfoConvenioMarcoDto } from '@/Types/ViewModels/ViewModels'
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

const Convenio = ref<InfoConvenioMarcoDto | null>(null)

onMounted(async () => {
  isLoading.value = true
  try {
    const response = await ApiService.GetConvenioMarcoCompleto(id)
    isLoading.value = false
    if (response.isSuccess) {
      Convenio.value = response.value
      console.log('Convenio.value =', JSON.parse(JSON.stringify(Convenio.value)))
    }
  } catch (error) {
    isLoading.value = false
    errorMessage.value = 'error al acceder a los datos del convenio marco'
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
      name: 'EditConvenioMarco',
      params: { id: Convenio.value.id },
    })
  }
}

const CargarEspecifico = () => {
  if (Convenio.value) {
    router.push({
      name: 'CreateConvenioEspecifico',
      params: { id: Convenio.value.id },
    })
  }
}

const DeleteConvenio = async () => {
  isLoading.value = true
  try {
    if (Convenio.value) {
      const response = await ApiService.DeleteConvenioMarco(Convenio.value.id)
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

const desvincularConvenioEspecifico = async (idConvenioEspecifico: number) => {
  errorMessage.value = ''
  isLoading.value = true

  const response = await ApiService.DesvincularConvenioEspecifico(id, idConvenioEspecifico)

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
      convenio.archivosAdjuntos ??= []
      convenio.archivosAdjuntos = [...convenio.archivosAdjuntos, ArchivoCargado]
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

      if (convenio && convenio.archivosAdjuntos) {
        convenio.archivosAdjuntos = convenio.archivosAdjuntos.filter(
          (archivo) => archivo.idArchivo !== id,
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
