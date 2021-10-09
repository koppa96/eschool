<template>
  <q-page>
    <q-card class="absolute-full q-ma-lg">
      <h5 class="q-my-none">Órarend</h5>
      <Schedule
        :time-ground="['09:00', '18:00']"
        :week-ground="['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday']"
        :task-detail="[
          [
            {
              dateStart: '09:30',
              dateEnd: '10:30',
              title: '开会'
            },
            {
              dateStart: '11:30',
              dateEnd: '13:50',
              title: '开会'
            }
          ]
        ]"
      />
    </q-card>
  </q-page>
</template>

<script setup lang="ts">
const timeRange = ['7:00', '18:00']
const weekDays = ['Hétfő', 'Kedd', 'Szerda', 'Csütörtök', 'Péntek']
</script>

<style scoped></style>
