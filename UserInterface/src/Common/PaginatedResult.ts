export interface PaginatedResult<T> {
  exit: boolean
  data: T
  errormessage: string
  errorcode: number
  totalItems: number
  totalPages: number
  currentPage: number
  pageSize: number
}
