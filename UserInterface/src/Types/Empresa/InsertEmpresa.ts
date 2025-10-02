export interface InsertEmpresaDto {
  id: number | null; // integer($int32) | null
  nombre: string | null;
  razonSocial: string | null;
  cuit: string | null;
  direccion: string | null;
  telefono: string | null;
  email: string | null; // string($email) | null
}