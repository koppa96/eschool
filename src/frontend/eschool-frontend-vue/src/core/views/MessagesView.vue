<template>
  <q-page>
    <q-card class="absolute-full q-ma-lg flex column">
      <h4 class="text-left q-my-sm q-pa-md">Üzeneteim</h4>
      <q-separator />
      <q-tabs
        v-model="tab"
        dense
        align="justify"
        narrow-indicator
        active-color="primary"
        indicator-color="primary"
      >
        <q-tab name="incoming" label="Bejövő üzenetek" icon="inbox" />
        <q-tab name="sent" label="Kimenő üzenetek" icon="outbox" />
      </q-tabs>
      <q-separator />
      <q-tab-panels v-model="tab">
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
