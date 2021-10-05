<template>
  <div
    class="inline-block q-pa-sm grade-value relative-position"
    @click="showDetails = true"
  >
    <transition name="fade">
      <q-card v-if="showDetails" class="absolute z-top q-pa-md grade-card">
        <div class="flex justify-between items-center q-mb-sm">
          <h6 class="q-my-none">Részletek</h6>
          <div class="flex">
            <q-btn dense round flat icon="edit" size="sm">
              <q-tooltip>Szerkesztés</q-tooltip>
            </q-btn>
            <q-btn dense round flat icon="delete" color="negative" size="sm">
              <q-tooltip>Törlés</q-tooltip>
            </q-btn>
            <q-btn
              dense
              round
              flat
              icon="close"
              size="sm"
              @click.stop="closeDetails()"
            >
              <q-tooltip>Bezárás</q-tooltip>
            </q-btn>
          </div>
        </div>
        <div><strong>Érték: </strong> {{ gradeValue(grade.value) }}</div>
        <div>
          <strong>Típus: </strong> {{ grade.gradeKind?.name }} (x{{
            grade.gradeKind.averageMultiplier
          }})
        </div>
        <div>
          <strong>Beírva: </strong> {{ dateTimeToString(grade.writtenIn) }}
        </div>
      </q-card>
    </transition>
    <span>{{ gradeValueNumber(grade.value) }}</span>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { GradeListResponse } from '@/shared/generated-clients/class-register'
import {
  gradeValueNumber,
  gradeValue,
  dateTimeToString
} from '@/core/utils/display-helpers'

const props = defineProps<{
  grade: GradeListResponse
}>()

const showDetails = ref(false)

function closeDetails(): void {
  console.log('closeDetails')
  showDetails.value = false
}
</script>

<style scoped lang="scss">
.grade-value {
  cursor: pointer;
  user-select: none;

  &:hover {
    color: #027be3;
  }

  .grade-card {
    color: black;
    width: 250px;
    top: 20px;
    left: -50%;
  }
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.5s;
}
.fade-enter,
.fade-leave-to {
  opacity: 0;
}
</style>
