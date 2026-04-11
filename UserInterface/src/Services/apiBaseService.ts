export const API_URL = 'http://localhost:8888/api'

export const getErrorMessage = (error: any): string => {
  return error.response?.data?.message || 'Error de conexión o error desconocido.'
}
