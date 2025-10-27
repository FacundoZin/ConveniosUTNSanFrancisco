export interface InsertArchivoDto {
  nombreArchivo: string
  file: File
  convenioEspecificoId?: number
  convenioMarcoId?: number
}
