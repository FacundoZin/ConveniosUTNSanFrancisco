import type {
  ConvenioEspecificoDto,
  ConvenioMarcoDto,
  ListConveniosDto,
} from '@/Types/ViewModels/ViewModels'

export function CreateListConveniosDto(
  data: ConvenioMarcoDto | ConvenioEspecificoDto | null,
): ListConveniosDto {
  if (data?.convenioType === 'especifico') {
    return { data: [data], Type: 'especifico' }
  }

  if (data?.convenioType === 'marco') {
    return { data: [data], Type: 'marco' }
  }

  return { data: [], Type: '' }
}
