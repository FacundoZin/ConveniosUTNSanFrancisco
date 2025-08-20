import type { CargarConvenioEspecificoRequestDto, CargarConvenioMarcoRequestDto, ConvenioFilters, UpdateConvenioEspecificoDto, UpdateConvenioMarcoDto } from '@/Types';
import axios from 'axios'

const api = axios.create({
  baseURL: 'http://localhost:5000/api', // ajustá al endpoint real
});


export default{

    GetConvenios: (params: ConvenioFilters) => {
        return api.get('/convenios', { params })},
        
    GetConvenioMarcoCompleto: (id: number) => {
        return api.get(`/ConveniosMarcos/${id}`)},

    GetConvenioEspecificoCompleto: (id: number) => {
        return api.get(`/ConveniosEspecificos/${id}`)},

    DeleteConvenioMarco: (id: number) => {
        return api.delete(`/ConveniosMarcos/${id}`)
    },

    DeleteConvenioEspecifico: (id: number) => {
        return api.delete(`/ConveniosEspecificos/${id}`)
    },

    CreateConvenioMarco: (Dto: CargarConvenioMarcoRequestDto) => {
        return api.post(`/ConveniosMarcos`, {Dto})
    },

    CreateConvenioEspecifico: (Dto: CargarConvenioEspecificoRequestDto) => {
        return api.post(`/ConveniosEspecificos`, {Dto})
    },

    EditarConvenioMarco: (Dto: UpdateConvenioMarcoDto) => {
        return api.put(`/ConveniosMarcos`, {Dto})
    },

    EditarConvenioEspecifico: (Dto: UpdateConvenioEspecificoDto) => {
        return api.put(`/ConveniosEspecificos`, {Dto})
    },

    
}


