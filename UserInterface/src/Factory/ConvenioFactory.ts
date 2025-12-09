import type {
  ConvenioEspecificoDto,
  ConvenioMarcoDto,
  ListConveniosDto,
} from '@/Types/ViewModels/ViewModels'

export function CreateListConveniosDto(
  data: ConvenioMarcoDto[] | ConvenioEspecificoDto[] | any | null,
  type?: 'marco' | 'especifico' | 'ambos' | '',
): ListConveniosDto {
  let convenios: any = []
  let convenioType: 'marco' | 'especifico' | 'ambos' | '' = ''

  if (!data || (Array.isArray(data) && data.length === 0)) {
    convenios = []
    convenioType = type || ''
  } else if ('conveniosMarcos' in (data as any) && 'convenioEspecificos' in (data as any)) {
    // Caso "Ambos": la API devuelve un objeto con ambas listas
    return {
      conveniosMarcos: (data as any).conveniosMarcos,
      conveniosEspecificos: (data as any).convenioEspecificos,
      Type: 'ambos',
      data: [],
    }
  } else if (Array.isArray(data) && data.length > 0) {
    if (data[0].convenioType === 'marco') {
      convenios = data
      convenioType = 'marco'
    } else if (data[0].convenioType === 'especifico') {
      convenios = data
      convenioType = 'especifico'
    } else {
      // Fallback
      convenios = data
      convenioType = type || ''
    }
  } else {
    // Caso default por si data no es array ni el objeto esperado (aunque el check inicial cubre null/empty)
    convenios = []
    convenioType = type || ''
  }

  return { data: convenios, Type: convenioType } as ListConveniosDto
}
