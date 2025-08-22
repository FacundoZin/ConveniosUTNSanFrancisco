<script setup lang="ts">
import ApiService from '@/Services/ApiService';
import type { ConvenioEspecificoCompleto, ConvenioMarcoCompleto } from '@/Types/Models';
import { isAxiosError } from 'axios';
import { onMounted, ref } from 'vue';
import { useRoute } from 'vue-router';


const router = useRoute();
const idparam= router.params.id;
let id = 0;

if (Array.isArray(idparam)) {
  id = parseInt(idparam[0]);
} else {
  id = parseInt(idparam)
}

const ConvenioMarco = ref<ConvenioEspecificoCompleto | null>(null);
const errorMensaje = ref('');

onMounted( async() => {
    try{
        const response = await ApiService.GetConvenioEspecificoCompleto(id);
        ConvenioMarco.value = response.data;
    }catch(error){
        if(isAxiosError(error) && error.response){
        errorMensaje.value = ` ${error.response.data}`;
    }else{
        errorMensaje.value = "Lo sentimos, algo a salido mal"
    }
    }
})
</script>