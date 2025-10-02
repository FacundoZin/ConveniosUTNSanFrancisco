import type { ISearchParamsDto } from "@/Types/Api.Interface";
import { EstadoConvenio } from "@/Types/Filters";

/**
 * Convierte el objeto ISearchParamsDto estructurado en un objeto plano
 * listo para ser usado como query parameters.
 * @param params Objeto ISearchParamsDto (la interfaz definida arriba).
 * @returns Objeto plano de query parameters (e.g., {'ByTitulo.Title': 'Mi título'}).
 */
export function mapSearchParamsToQueryParams(params: ISearchParamsDto): Record<string, any> {
  const queryParams: Record<string, any> = {};

  const processGroup = (group: any, groupName: string) => {
    // Solo procesamos el grupo si existe (porque el grupo es opcional)
    if (!group) return;

    // Iterar sobre las claves internas del objeto anidado 
    for (const key in group) {
      const value = group[key];

      // Como las propiedades internas son REQUERIDAS en TS, asumimos que 'value' tiene contenido.
      // Solo aseguramos que no sea null si por alguna razón runtime lo es.
      if (value !== null && value !== undefined) {
        const finalKey = `${groupName}.${key}`;
        
        // Manejar el enum EstadoConvenio si es necesario
        if (finalKey === 'ByEstado.Estado' && typeof value === 'string') {
           queryParams[finalKey] = EstadoConvenio[value as keyof typeof EstadoConvenio];
        } else {
           queryParams[finalKey] = value;
        }
      }
    }
  };

  // Procesar cada grupo de filtro
  processGroup(params.ByTitulo, 'ByTitulo');
  processGroup(params.ByNumeroResolucion, 'ByNumeroResolucion');
  processGroup(params.ByNumeroConvenio, 'ByNumeroConvenio');
  processGroup(params.ByEmpresa, 'ByEmpresa');
  processGroup(params.ByIsActa, 'ByIsActa');
  processGroup(params.ByIsRefrendado, 'ByIsRefrendado');
  processGroup(params.ByEstado, 'ByEstado');
  processGroup(params.ByCarrera, 'ByCarrera');
  processGroup(params.ByFechaFirma, 'ByFechaFirma');
  processGroup(params.ByFechaFin, 'ByFechaFin');
  processGroup(params.ByAntiguedadDto, 'ByAntiguedadDto');
  processGroup(params.ByProximosAvencer, 'ByProximosAvencer');

  return queryParams;
}