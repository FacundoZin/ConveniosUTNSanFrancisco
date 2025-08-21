<script setup lang="ts">
import type { ConvenioQueryObject } from '@/Types/Api.Interface';

const props = defineProps<{query: ConvenioQueryObject }>()
</script>

<template>
  <div>
    <label>
      <input type="checkbox" v-model="props.query.ProximosAterminar" />
      Próximos a terminar
    </label>

    <label>
      <input type="checkbox" v-model="props.query.AntiguedadAscendente" />
      Antigüedad Ascendente
    </label>

    <label>
      <input type="checkbox" v-model="props.query.AntiguedadDescendente" />
      Antigüedad Descendente
    </label>
  </div>
</template>