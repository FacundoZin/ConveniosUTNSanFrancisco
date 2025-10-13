import type { Result } from '@/Common/Result';
import type { CargarConvenioEspecificoRequestDto } from '@/Types/ConvenioEspecifico/CreateConvenioEspecifico';
import type { UpdateConvenioEspecificoRequestDto } from '@/Types/ConvenioEspecifico/UpdateConvenioEspecifico';
import type { CargarConvenioMarcoRequestDto, InsertConvenioMarcoDto } from '@/Types/ConvenioMarco/CreateConvenioMarcoRequest';
import type { UpdateConvenioMarcoRequetsDto } from '@/Types/ConvenioMarco/UpdateConvenioMarco';
import type { ComboBoxEmpresasDto } from '@/Types/Empresa/ComboBoxEmpresaDto';
import type { IConvenioQueryObject } from '@/Types/Filters';
import type { ConvenioCreated, ConvenioEspecificoDto, ConvenioMarcoDto, InfoConvenioEspecificoDto, InfoConvenioMarcoDto } from '@/Types/ViewModels/ViewModels';
import axios from 'axios';


const API_URL = 'http://localhost:8888/api';


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

    static async GetConvenioMarcoCompleto(id: number): Promise<Result<InfoConvenioMarcoDto>>{
        try{
            const response = await axios.get(`${API_URL}/ConveniosMarcos/${id}`)
            return{isSuccess: true, value: response.data, status: response.status}
        }catch(Ex: any){
            return {isSuccess: false, error: {message: getErrorMessage(Ex), status: Ex.response?.status}}
        }
    }

    static async GetConvenioEspecificoCompleto(id: number): Promise<Result<InfoConvenioEspecificoDto>>{
        try{
            const response = await axios.get(`${API_URL}/ConveniosEspecificos/${id}`)
            return{isSuccess: true, value: response.data, status: response.status}
        }catch(Ex: any){
            return {isSuccess: false, error: {message: getErrorMessage(Ex), status: Ex.response?.status}}
        }
    }

    static async DeleteConvenioMarco(id: number):Promise<Result<null>>{
        try{
            const response =  await axios.delete(`${API_URL}/ConveniosMarcos/${id}`)
            return{isSuccess: true, value: null, status: response.status}
        }catch(Ex:any){
            return {isSuccess: false, error: {message: getErrorMessage(Ex), status: Ex.response?.status}}
        }     
    }

    static async DeleteConvenioEspecifico(id: number):Promise<Result<null>>{
        try{
            const response = await axios.delete(`${API_URL}/ConveniosEspecificos/${id}`)
            return{isSuccess: true, value: null, status: response.status}
        }catch(Ex:any){
            return {isSuccess: false, error: {message: getErrorMessage(Ex), status: Ex.response?.status}}
        }     
    }

    static async CreateConvenioMarco(Dto: CargarConvenioMarcoRequestDto):Promise<Result<ConvenioCreated>>{
        try{
            const response = await axios.post(`${API_URL}/ConveniosMarcos`, Dto)
            return{isSuccess: true, value: response.data, status: response.status}
        }catch(Ex: any){
            return { isSuccess:false, error: { message: getErrorMessage(Ex), status: Ex.response?.status}}
        }
    }

    static async CreateConvenioEspecifico(Dto: CargarConvenioEspecificoRequestDto):Promise<Result<ConvenioCreated>>{
        try{
            const response = await axios.post(`${API_URL}/ConveniosEspecificos`, Dto)
            return{isSuccess: true, value: response.data, status: response.status}
        }catch(Ex: any){
            return { isSuccess:false, error: { message: getErrorMessage(Ex), status: Ex.response?.status}}
        }
    }

    static async EditarConvenioMarco(Dto: UpdateConvenioMarcoRequetsDto): Promise<Result<null>>{
        try{
            const response = await axios.put(`${API_URL}/ConveniosMarcos`, Dto)
            return{isSuccess: true, value: null, status: response.status}
        }catch(Ex: any){
            return { isSuccess:false, error: { message: getErrorMessage(Ex), status: Ex.response?.status}}
        }
    }
    
    static async EditarConvenioEspecifico(Dto: UpdateConvenioMarcoRequetsDto): Promise<Result<null>>{
        try{
            const response = await axios.put(`${API_URL}/ConveniosEspecificos`, Dto)
            return{isSuccess: true, value: null, status: response.status}
        }catch(Ex: any){
            return { isSuccess:false, error: { message: getErrorMessage(Ex), status: Ex.response?.status}}
        }
    }

    static async GetEmpresas(): Promise<ComboBoxEmpresasDto>{
       return await axios.get(`${API_URL}/Empresa`) 
    }
}
