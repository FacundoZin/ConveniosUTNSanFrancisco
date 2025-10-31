import type { IConvenioQueryObject } from '@/Types/Filters'

export interface FilterPayload<Key extends keyof IConvenioQueryObject, Value> {
  key: Key
  value: Value
}
