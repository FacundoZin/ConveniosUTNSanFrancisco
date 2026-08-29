export const API_URL = 'http://localhost:8888/api'

export const getErrorMessage = (error: any): string => {
  if (typeof error?.response?.data === 'string' && error.response.data.trim() !== '') {
    return error.response.data
  }
  return error?.response?.data?.message || 'Error de conexión o error desconocido.'
}
