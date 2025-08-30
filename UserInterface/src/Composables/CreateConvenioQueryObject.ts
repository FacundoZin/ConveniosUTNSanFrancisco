import type { ConvenioQueryObject } from "@/Types/Api.Interface";

export function createConvenioQuery(): ConvenioQueryObject {
  return {
    TituloConvenio: '',
    Nombre_empresa: '',
    ProximosAterminar: false,
    AntiguedadAscendente: false,
    AntiguedadDescendente: false,
    PaginaActual: 1,
    CantidadResultados: 10
  };
}