import type { Result } from '@/Common/Result';
import type { IConvenioQueryObject } from '@/Types/Filters';
import type { ConvenioEspecificoDto, ConvenioMarcoDto } from '@/Types/ViewModels/ViewModels';
import axios from 'axios';


const API_URL = 'http://localhost:8888/api',


const getErrorMessage = (error: any) => {
  return error.response?.data?.message || 'Error de conexión o error desconocido.'
}

export class ApiService{

    static async GetConvenios(params: IConvenioQueryObject): Promise<Result<ConvenioEspecificoDto|ConvenioMarcoDto>>{
        try{
            const response = await axios.get(`${API_URL}/Convenios`, {params})
            return{isSuccess: true, value: response.data, status: response.status}
        }catch(Ex: any){
            return { isSuccess:false, error: { message: getErrorMessage(Ex), status: Ex.response?.status}}
        }

    }

    GetConvenios: async (params: IConvenioQueryObject) => {
        return api.get<ConveniosResponse>('/Convenios', { params });
    },

    GetConvenioMarcoCompleto: async (id: number) => {
        return api.get(`/ConveniosMarcos/${id}`);
    },

    GetConvenioEspecificoCompleto: async (id: number) => {
        return api.get(`/ConveniosEspecificos/${id}`);

    },

    DeleteConvenioMarco: async (id: number) => {
        return api.delete(`/ConveniosMarcos/${id}`);
    },

    DeleteConvenioEspecifico: async (id: number) => {
        return api.delete(`/ConveniosEspecificos/${id}`);
    },

    CreateConvenioMarco: async (Dto: CargarConvenioMarcoRequestDto) => {
        return api.post(`/ConveniosMarcos`, Dto);
    },

    CreateConvenioEspecifico: async (Dto: CargarConvenioEspecificoRequestDto) => {
        return api.post(`/ConveniosEspecificos`, Dto);
    },

    EditarConvenioMarco: async (Dto: UpdateConvenioMarcoDto) => {
        return api.put(`/ConveniosMarcos`, Dto);
    },

    EditarConvenioEspecifico: async (Dto: UpdateConvenioEspecificoDto) => {
        return api.put(`/ConveniosEspecificos`, Dto);
    },

    UploadDocument: async (data: UploadConvenioDocument) => {

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
