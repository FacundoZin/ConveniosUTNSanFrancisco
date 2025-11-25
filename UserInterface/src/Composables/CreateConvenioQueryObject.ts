import type { IConvenioQueryObject } from '@/Types/Filters'
import { reactive, ref, watch } from 'vue'

export function useConvenioQuery() {
  // El objeto reactivo que contiene los filtros
  const queryObject = reactive<IConvenioQueryObject>({
    ByTitulo: null,
    ByEmpresa: null,
    ByAntiguedadDto: null,
    ByCarrera: null,
    ByEstado: null,
    ByIsActa: null,
    ByIsRefrendado: null,
    ByNumeroConvenio: null,
    ByFechaFin: null,
    ByNumeroResolucion: null,
    ByFechaFirma: null,
    ByProximosAvencer: null,
    ByMes: null,
    ByAño: null,
    ByDesdeHastaDto: null,
    CountFirmadosByMesDto: null,
    countFirmadosByRangoDto: null,
  })

  // Guardamos la última propiedad que se modificó
  const lastModifiedKey = ref<keyof IConvenioQueryObject | null>(null)

  // Función de limpieza interna que usará el watcher
  const clearExcept = (keepFilter: keyof IConvenioQueryObject) => {
    for (const key in queryObject) {
      if (key !== keepFilter) {
        queryObject[key as keyof IConvenioQueryObject] = null
      }
    }
  }

  // Función pública para limpiar todos los filtros manualmente
  const clearAllFilters = () => {
    for (const key in queryObject) {
      queryObject[key as keyof IConvenioQueryObject] = null
    }
    lastModifiedKey.value = null
  }

  // Observador profundo del objeto queryObject
  watch(
    () => queryObject,
    (newValue, oldValue) => {
      let currentModifiedKey: keyof IConvenioQueryObject | null = null

      // Encontramos la clave que ha cambiado
      for (const key in newValue) {
        // Comparamos el valor actual con el anterior
        if (
          newValue[key as keyof IConvenioQueryObject] !==
          oldValue[key as keyof IConvenioQueryObject]
        ) {
          // Nos aseguramos de que el nuevo valor no sea nulo, para evitar que la limpieza se active al borrar un filtro
          if (newValue[key as keyof IConvenioQueryObject] !== null) {
            currentModifiedKey = key as keyof IConvenioQueryObject
            break // Salimos del bucle una vez que encontramos el cambio
          }
        }
      }

      // Si se encontró una clave modificada y es diferente a la última
      if (currentModifiedKey && currentModifiedKey !== lastModifiedKey.value) {
        clearExcept(currentModifiedKey)
        lastModifiedKey.value = currentModifiedKey
      }
    },
    { deep: true }, // Opción para observar cambios en propiedades anidadas
  )

  return {
    queryObject,
    clearAllFilters,
  }
}
