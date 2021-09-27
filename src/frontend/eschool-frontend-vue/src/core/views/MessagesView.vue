<template>
  <q-page>
    <q-card class="absolute-full q-ma-lg flex column">
      <h4 class="text-left q-my-sm q-pa-md">Üzeneteim</h4>
      <q-separator />
      <q-tabs v-model="tab" dense>
        <q-tab name="incoming" label="Bejövő üzenetek" />
        <q-tab name="sent" label="Kimenő üzenetek" />
      </q-tabs>
      <q-separator />
      <q-tab-panels v-model="tab" animated>
        <q-tab-panel name="incoming">
          BEJÖVŐ :)
        </q-tab-panel>
        <q-tab-panel name="sent">
          KIMENŐ (:
        </q-tab-panel>
      </q-tab-panels>
    </q-card>
  </q-page>
</template>

<script setup lang="ts">
import { ref } from 'vue'

const tab = ref('incoming')
</script>

<style scoped></style>
