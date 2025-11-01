import type {
  ConvenioEspecificoDto,
  ConvenioMarcoDto,
  ListConveniosDto,
} from '@/Types/ViewModels/ViewModels'

export function CreateListConveniosDto(
  data: ConvenioMarcoDto[] | ConvenioEspecificoDto[] | null,
): ListConveniosDto {
  let convenios: any
  let convenioType: '' | 'marco' | 'especifico' = ''

  if (!data || data.length === 0) {
    convenios = []
    convenioType = ''
  } else if (data[0].convenioType === 'marco') {
    convenios = data
    convenioType = 'marco'
  } else if (data[0].convenioType === 'especifico') {
    convenios = data
    convenioType = 'especifico'
  }

  return { data: convenios, Type: convenioType }
}
