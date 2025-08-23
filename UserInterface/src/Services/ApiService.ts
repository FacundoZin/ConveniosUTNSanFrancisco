import type { CargarConvenioEspecificoRequestDto, CargarConvenioMarcoRequestDto, ConvenioQueryObject, UpdateConvenioEspecificoDto, UpdateConvenioMarcoDto, UploadConvenioDocument } from '@/Types/Api.Interface';
import type { ConveniosResponse } from '@/Types/Models';
import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5194/api',
});


export default{

    GetConvenios: (params: ConvenioQueryObject) => {
        return api.get<ConveniosResponse>('/Convenios', { params });
    },

    GetConvenioMarcoCompleto: (id: number) => {
        return api.get(`/ConveniosMarcos/${id}`);
    },

    GetConvenioEspecificoCompleto: (id: number) => {
        return api.get(`/ConveniosEspecificos/${id}`);

    },

    DeleteConvenioMarco: (id: number) => {
        return api.delete(`/ConveniosMarcos/${id}`);
    },

    DeleteConvenioEspecifico: (id: number) => {
        return api.delete(`/ConveniosEspecificos/${id}`)
    },

    CreateConvenioMarco: (Dto: CargarConvenioMarcoRequestDto) => {
        return api.post(`/ConveniosMarcos`, Dto);
    },

    CreateConvenioEspecifico: (Dto: CargarConvenioEspecificoRequestDto) => {
        return api.post(`/ConveniosEspecificos`, Dto);
    },

    EditarConvenioMarco: (Dto: UpdateConvenioMarcoDto) => {
        return api.put(`/ConveniosMarcos`, Dto)
    },

    EditarConvenioEspecifico: (Dto: UpdateConvenioEspecificoDto) => {
        return api.put(`/ConveniosEspecificos`, Dto)
    },

    UploadDocument: (data: UploadConvenioDocument) => {

        const formData = new FormData();
        formData.append('file', data.File);
        formData.append('convenioId', data.ConvenioId.toString());
        formData.append('convenioType', data.ConvenioType);

        return api.post(`/Documents`, formData, {
            headers: {
                'Content-Type': 'multipart/form-data',
      }})
    },

    DownloadDocument: async (id: number, ConvenioType: string) => {
        const Document = await api.get(`/Documents/${id}/${ConvenioType}`,
      {
        responseType: 'blob', 
      });

      return Document.data; 
    }


}
