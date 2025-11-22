import type {
  ConvenioEspecificoDto,
  ConvenioMarcoDto,
  ListConveniosDto,
} from '@/Types/ViewModels/ViewModels'

export function CreateListConveniosDto(
  data: ConvenioMarcoDto[] | ConvenioEspecificoDto[] | null,
  type?: 'marco' | 'especifico' | '',
): ListConveniosDto {
  let convenios: any = []
  let convenioType: 'marco' | 'especifico' | '' = ''

  if (!data || data.length === 0) {
    convenios = []
    convenioType = type || ''
  } else if (data[0].convenioType === 'marco') {
    convenios = data
    convenioType = 'marco'
  } else if (data[0].convenioType === 'especifico') {
    convenios = data
    convenioType = 'especifico'
  } else {
    // Si no tiene convenioType definido, usar el type pasado como parámetro
    convenios = data
    convenioType = type || ''
  }

  return { data: convenios, Type: convenioType }
}

