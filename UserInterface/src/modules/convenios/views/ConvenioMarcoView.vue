<template>
  <div class="container mt-4" v-if="Convenio?.id">
    <!-- Back Button -->
    <div class="mb-4">
      <button 
        class="btn btn-link text-decoration-none p-0 d-flex align-items-center gap-2 text-primary fw-semibold transition-all hover-translate-x"
        @click="router.back()"
      >
        <i class="bi bi-arrow-left-circle-fill fs-4"></i>
        <span>Volver</span>
      </button>
    </div>

    <!-- Info del Convenio Marco -->
    <h5>Información del convenio</h5>

    <div class="card shadow-sm mb-4">
      <div class="card-body position-relative">
        <h6 class="card-title text-primary mb-3 pe-4">
          {{ Convenio.titulo || 'Sin título' }}
        </h6>

        <div class="card-text">
          <div class="d-flex align-items-center mb-2">
            <i class="bi bi-calendar-check me-2 text-muted"></i>
            <small><strong>Fecha firma:</strong> {{ Convenio.fechaFirmaConvenio || '-' }}</small>
          </div>

          <div class="d-flex align-items-center mb-2">
            <i class="bi bi-calendar-x me-2 text-muted"></i>
            <small><strong>Fecha fin:</strong> {{ Convenio.fechaFin || ' -' }}</small>
          </div>

          <div class="d-flex align-items-start mb-2">
            <i class="bi bi-chat-left-text me-2 text-muted"></i>
            <small class="text-break">
              <strong>Comentario:</strong> {{ Convenio.comentarioOpcional || ' -' }}
            </small>
          </div>

          <div class="d-flex align-items-center mb-2">
            <i class="bi bi-hash me-2 text-muted"></i>
            <small
              ><strong>Número de resolución:</strong> {{ Convenio.numeroResolucion || ' -' }}</small
            >
          </div>

          <div class="d-flex align-items-center mb-2">
            <i class="bi bi-hash me-2 text-muted"></i>
            <small
              ><strong>Número de convenio:</strong> {{ Convenio.numeroconvenio || ' -' }}</small
            >
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
    <h5>Informacion de la empresa asociada</h5>
    <EmpresaCardReadOnly v-if="Convenio.empresa" :empresa="Convenio.empresa" />
    <div v-else class="col-12">
      <div class="card shadow-sm p-3 text-center" style="background-color: #f8f9fa">
        <div class="card-body">
          <p class="text-muted mb-0">Aún no hay una empresa asociada a este convenio marco.</p>
        </div>
      </div>
    </div>

    <hr class="my-4" />

    <!-- Convenios Especficos -->
    <h5>Convenios Especficos</h5>
    <div class="row">
      <template v-if="Convenio.conveniosEspecificos && Convenio.conveniosEspecificos.length > 0">
        <div class="col-md-4 mb-3" v-for="ce in Convenio.conveniosEspecificos" :key="ce.id">
          <ConvEspecificoCardReadOnly :convenio="ce" />
        </div>
      </template>
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
      <button class="btn btn-danger" @click="showDeleteModal = true">Eliminar Convenio</button>
    </div>

    <ConfirmacionModal
      :show="showDeleteModal"
      titulo="Eliminar Convenio Marco"
      :mensaje="`¿Estás seguro de que querés eliminar el convenio &quot;${Convenio?.titulo}&quot;? Esta acción no se puede deshacer.`"
      textoConfirmar="Sí, eliminar"
      textoCancelar="Cancelar"
      tipo="danger"
      @confirmar="confirmarDelete"
      @cancelar="showDeleteModal = false"
    />
  </div>

  <!--  mensaje mientras carga -->
  <div v-else class="loader-overlay d-flex justify-content-center align-items-center">
    <div class="spinner-border text-primary" role="status" style="width: 3rem; height: 3rem">
      <span class="visually-hidden">Cargando...</span>
    </div>
  </div>
</template>

<script setup lang="ts">
import ConfirmacionModal from '@/modules/shared/components/ConfirmacionModal.vue'
import ConvEspecificoCardReadOnly from '@/modules/convenios/components/ConvEspecificoCardReadOnly.vue'
import EmpresaCardReadOnly from '@/modules/empresas/components/EmpresaCardReadOnly.vue'
import FileUploader from '@/modules/convenios/components/FileUploader.vue'
import router from '@/router'
import ConvenioService from '@/modules/convenios/services/ConvenioService'
import DocumentService from '@/modules/shared/services/DocumentService'
import { EstadoConvenioTexto } from '@/Types/Enums/Enums'
import type { InfoConvenioMarcoDto } from '@/Types/ViewModels/ViewModels'
import { isAxiosError } from 'axios'
import { onMounted, ref, watch } from 'vue'
import { useRoute } from 'vue-router'
import { POSITION, useToast } from 'vue-toastification'
import { useConvenioStore } from '../stores/convenioStore'
import { storeToRefs } from 'pinia'

const isLoading = ref(false)
const showDeleteModal = ref(false)
const errorMessage = ref<string>('')
const toast = useToast()
const route = useRoute()

const getId = () => {
  const idparam = route.params.id
  if (Array.isArray(idparam)) return parseInt(idparam[0])
  return parseInt(idparam as string)
}

const store = useConvenioStore()
const { currentConvenioMarco: Convenio } = storeToRefs(store)

const fetchConvenio = async (forceLoad = false) => {
  const id = getId()
  if (Number.isNaN(id)) return

  // Fix asimetría Marco→Específico: siempre refetch desde servidor.
  // El cache de Pinia causaba que el Marco no mostrara el nuevo Específico vinculado.
  // Se mantiene lastMarcoId solo como referencia, pero no como bloqueo de fetch.
  // forceLoad se conserva por compatibilidad; el early-return stale se elimina.

  isLoading.value = true
  try {
    const response = await ConvenioService.GetConvenioMarcoCompleto(id)
    isLoading.value = false
    if (response.isSuccess) {
      store.setConvenioMarco(response.value)
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
}

onMounted(async () => {
  await fetchConvenio()
})

watch(
  () => route.params.id,
  async () => {
    await fetchConvenio(true)
  },
)

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
      const response = await ConvenioService.DeleteConvenioMarco(Convenio.value.id)
      if (response.isSuccess) {
        isLoading.value = false
        toast.success(`"${Convenio.value.titulo}" eliminado con �xito`)
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

const confirmarDelete = async () => {
  showDeleteModal.value = false
  await DeleteConvenio()
}

// Funciones de desvinculaci�n eliminadas (c�digo muerto)

const CargarDocumento = async ({ file, nombre }: { file: File; nombre: string }) => {
  errorMessage.value = ''
  isLoading.value = true

  try {
    const ArchivoCargado = await DocumentService.CargarArchivoToMarco(
      nombre,
      file,
      Convenio.value!.id,
    )

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
    const exito = await DocumentService.EliminarArchivo(id)
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
    await DocumentService.DescargarArchivo(id, nombre)
    isLoading.value = false
  } catch (error) {
    isLoading.value = false
    console.error('Error al descargar el archivo:', error)
    errorMessage.value = 'ocurrio un error al descargar el documento'
  }
}
</script>
