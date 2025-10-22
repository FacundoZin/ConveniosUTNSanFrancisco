<template>
  <div class="container mt-4 position-relative">
    <div class="d-flex justify-content-between align-items-center mb-4">
      <h3 class="text-primary">Cargar Convenio Marco</h3>

      <!-- Botón visible solo cuando se crea un convenio -->
      <button @click="IrAConvenio()" class="btn btn-outline-success d-flex align-items-center gap-2"
        title="Ver convenio creado">
        <i class="bi bi-arrow-right-circle"></i>
        Ir al convenio
      </button>
    </div>

    <!-- Mensaje de error -->
    <div v-if="errorMensaje" class="alert alert-danger" role="alert">
      {{ errorMensaje }}
    </div>

    <form @submit.prevent="submitForm" class="row g-3">
      <!-- DATOS DEL CONVENIO -->
      <div class="col-md-6">
        <label class="form-label">Número de Convenio</label>
        <input v-model="ConvenioMarcoRequest.updateConvenioMarcoDto.numeroConvenio" type="text" class="form-control" />
      </div>

      <div class="col-md-6">
        <label class="form-label">Título</label>
        <input v-model="ConvenioMarcoRequest.updateConvenioMarcoDto.titulo" type="text" class="form-control" />
      </div>

      <div class="col-md-6">
        <label class="form-label">Fecha de Firma</label>
        <input v-model="ConvenioMarcoRequest.updateConvenioMarcoDto.fechaFirmaConvenio" type="date"
          class="form-control" />
      </div>

      <div class="col-md-6">
        <label class="form-label">Fecha de Fin</label>
        <input v-model="ConvenioMarcoRequest.updateConvenioMarcoDto.fechaFin" type="date" class="form-control" />
      </div>

      <div class="col-12">
        <label class="form-label">Comentario Opcional</label>
        <textarea v-model="ConvenioMarcoRequest.updateConvenioMarcoDto.comentarioOpcional" class="form-control"
          rows="2"></textarea>
      </div>

      <div class="col-md-4">
        <label class="form-label">Estado</label>
            <select v-model.number="ConvenioMarcoRequest.updateConvenioMarcoDto.estado" class="form-select" required>
              <option value="" disabled>Seleccionar...</option>
              <option :value="0">Borrador</option>
              <option :value="1">Vigente</option>
              <option :value="3">Finalizado</option>
            </select>
      </div>

      <div class="col-md-4">
        <label class="form-label">Número de Resolución</label>
        <input v-model="ConvenioMarcoRequest.updateConvenioMarcoDto.numeroResolucion" type="text"
          class="form-control" />
      </div>

      <div class="col-md-4">
        <label class="form-label">Refrendado</label>
        <select v-model="ConvenioMarcoRequest.updateConvenioMarcoDto.refrendado" class="form-select">
          class="form-select" required>
          <option :value="true">Sí</option>
          <option :value="false">No</option>
        </select>
      </div>

      <hr class="my-4" />

      <hr class="my-4" />
      <h4 class="text-secondary mb-3">Empresa Asociada 🏢</h4>

      <div v-if="infoConvenioMarcoCompleta && infoConvenioMarcoCompleta.empresa" class="col-12">
        <div class="card border-primary shadow-sm">
          <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
            <h5 class="mb-0">
              <i class="bi bi-briefcase me-2"></i>
              Empresa Asociada: {{ infoConvenioMarcoCompleta.empresa.nombre_Empresa }}
            </h5>

            <button @click="infoConvenioMarcoCompleta.empresa = undefined"
              class="btn btn-sm btn-outline-light d-flex align-items-center gap-1" title="Desvincular Empresa">
              <i class="bi bi-x-circle"></i>
              Desvincular
            </button>
          </div>
          <div class="card-body">
            <div class="row g-2">
              <div class="col-md-6">
                <strong>Razón Social:</strong> {{ infoConvenioMarcoCompleta.empresa.razonSocial || 'N/A' }}
              </div>
              <div class="col-md-6">
                <strong>CUIT:</strong> {{ infoConvenioMarcoCompleta.empresa.cuit || 'N/A' }}
              </div>
              <div class="col-md-6">
                <strong>Dirección:</strong> {{ infoConvenioMarcoCompleta.empresa.direccion_Empresa || 'N/A' }}
              </div>
              <div class="col-md-6">
                <strong>Teléfono:</strong> {{ infoConvenioMarcoCompleta.empresa.telefono_Empresa || 'N/A' }}
              </div>
              <div class="col-12">
                <strong>Email:</strong> {{ infoConvenioMarcoCompleta.empresa.email_Empresa || 'N/A' }}
              </div>
            </div>
          </div>
        </div>
      </div>


      <div v-else class="col-12">
        <div class="col-12 mb-3">
          <div class="form-check form-switch">
            <input class="form-check-input" type="checkbox" id="switchNuevaEmpresa" v-model="cargarNuevaEmpresa" />
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
      </div>

      <hr class="my-4" />

      <div class="col-12">
        <h4 class="text-secondary mb-3">Convenios Específicos Asociados 📄</h4>

        <div
          v-if="infoConvenioMarcoCompleta && infoConvenioMarcoCompleta.conveniosEspecificos && infoConvenioMarcoCompleta.conveniosEspecificos.length">
          <div class="table-responsive">
            <table class="table table-striped table-hover align-middle">
              <thead class="table-primary">
                <tr>
                  <th>N° Convenio</th>
                  <th>Título</th>
                  <th>Empresa</th>
                  <th>Fecha Fin</th>
                  <th>Estado</th>
                  <th>Tipo</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="convenio in infoConvenioMarcoCompleta.conveniosEspecificos" :key="convenio.id">
                  <td class="text-nowrap">{{ convenio.numeroConvenio || 'N/A' }}</td>
                  <td>{{ convenio.titulo || 'Sin título' }}</td>
                  <td>{{ convenio.nombreEmpresa || 'Desconocida' }}</td>
                  <td class="text-nowrap">{{ convenio.fechaFin || 'Indefinida' }}</td>
                  <td>
                    <span :class="{
                      'badge rounded-pill': true,
                      'bg-secondary': convenio.estado === 0, 'bg-success': convenio.estado === 1, 'bg-danger': convenio.estado === 2
                    }">
                      {{
                        convenio.estado === 1 ? 'Vigente' :
                          convenio.estado === 2 ? 'Finalizado' :
                            'Borrador'
                      }}
                    </span>
                  </td>
                  <td>
                    <span
                      :class="{ 'badge bg-info text-dark': !convenio.esActa, 'badge bg-secondary': convenio.esActa }">
                      {{ convenio.esActa ? 'Acta' : 'Convenio' }}
                    </span>
                  </td>

                  <td class="text-nowrap">

                    <a href="#" @click.prevent="IrAConvenioEspecifico(convenio.id)" class="text-primary me-2"
                      title="Ir al Convenio Específico">
                      <i class="bi bi-box-arrow-up-right fs-5"></i>
                    </a>

                    <a href="#" @click.prevent="DesvincularConvenioEspecificos(convenio.id)" class="text-danger"
                      title="Desvincular convenio específico">
                      <i class="bi bi-x-circle fs-5"></i>
                    </a>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>

        <div v-else class="alert alert-info border-0 shadow-sm" role="alert">
          <i class="bi bi-info-circle-fill me-2"></i>
          **No hay convenios específicos** asociados a este Convenio Marco.
        </div>
      </div>

      <div class="col-12 mt-4">
        <button type="submit" class="btn btn-primary">Cargar Convenio</button>
      </div>
    </form>

    <div v-if="IsLoading" class="loader-overlay d-flex justify-content-center align-items-center">
      <div class="spinner-border text-primary" role="status" style="width: 3rem; height: 3rem;">
        <span class="visually-hidden">Cargando...</span>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useUpdateConvMarcoComposable } from '@/Composables/UpdateConvMarcoComposable';
import '@/Styles/EditConvenioMarcoForm.css';
import { POSITION, useToast } from 'vue-toastification';

const toast = useToast();
const {
  infoConvenioMarcoCompleta,
  ConvenioMarcoRequest,
  IsLoading,
  errorMensaje,
  cargarNuevaEmpresa,
  empresas,
  empresaForm,
  DesvincularConvenioEspecificos,
  submitForm: submitFormLogic,
  IrAConvenio,
  IrAConvenioEspecifico
} = useUpdateConvMarcoComposable();


const submitForm = async () => {
  try {
    await submitFormLogic();
    toast.success("Convenio editado con éxito");
  } catch (error) {
    toast.error("Error al editar el convenio", { position: POSITION.BOTTOM_CENTER });
  }
};
</script>
