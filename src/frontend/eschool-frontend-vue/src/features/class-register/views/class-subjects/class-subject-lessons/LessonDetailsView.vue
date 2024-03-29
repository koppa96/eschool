<template>
  <q-page>
    <q-card class="absolute-full q-ma-lg flex column no-wrap">
      <DetailsHeader
        class="q-pa-md"
        :name="title"
        :deletable="false"
        @edit="openEditDialog()"
      />
      <q-expansion-item default-opened>
        <template #header>
          <h6 class="q-my-none">Részletek</h6>
        </template>
        <pre class="q-px-md description">{{ lesson.description }}</pre>
        <DetailsGrid class="q-pa-md" :model-value="detailsGridData" />
      </q-expansion-item>
      <q-tabs v-model="tab" inline-label align="left" active-color="primary">
        <q-tab name="absences" icon="alarm_off" label="Hiányzások" />
        <q-tab name="homeworks" icon="home" label="Házi feladatok" />
      </q-tabs>
      <q-tab-panels v-model="tab" animated class="fill-card">
        <q-tab-panel name="absences" class="q-pa-none">
          <LessonAbsenceList
            class="h-100"
            :lesson-id="lessonId"
            :class-id="classId"
            :school-year-id="schoolYearId"
            :subject-id="subjectId"
          />
        </q-tab-panel>
        <q-tab-panel name="homeworks" class="q-pa-none">
          <LessonHomeAssignmentList class="h-100" :lesson-id="lessonId" />
        </q-tab-panel>
      </q-tab-panels>
    </q-card>
  </q-page>
</template>

<script setup lang="ts">
import { computed, ref } from 'vue'
import { useRoute } from 'vue-router'
import { useQuasar } from 'quasar'
import LessonCreateEditDialog from '../../../components/LessonCreateEditDialog.vue'
import DetailsHeader from '@/shared/components/DetailsHeader.vue'
import DetailsGrid from '@/shared/components/DetailsGrid.vue'
import LessonAbsenceList from '@/features/class-register/components/LessonAbsenceList.vue'
import { createClient } from '@/shared/api'
import {
  ClassSchoolYearSubjectLessonsClient,
  LessonDetailsResponse,
  LessonEditCommand
} from '@/shared/generated-clients/class-register'
import { DetailsGridItem } from '@/shared/model/details-grid.model'
import {
  dateTimeToString,
  displayClass,
  yesOrNo
} from '@/core/utils/display-helpers'
import { useSaveAndDeleteNotifications } from '@/core/utils/save.utils'
import LessonHomeAssignmentList from '@/features/home-assignments/components/LessonHomeAssignmentList.vue'

interface Params {
  classId: string
  schoolYearId: string
  subjectId: string
  lessonId: string
}

const route = useRoute()
const client = createClient(ClassSchoolYearSubjectLessonsClient)
const lesson = ref(new LessonDetailsResponse())
const { lessonId, subjectId, classId, schoolYearId } = resolveParams()
const { dialog } = useQuasar()
const { save } = useSaveAndDeleteNotifications()
const title = computed(() => lesson.value.title ?? 'Óra részletei')
const tab = ref('absences')

const detailsGridData = computed<DetailsGridItem[]>(() => {
  return [
    {
      name: 'Osztály',
      value: displayClass(lesson.value.class)
    },
    {
      name: 'Tantárgy',
      value: lesson.value.subject?.name
    },
    {
      name: 'Tanév',
      value: lesson.value.schoolYear?.displayName
    },
    {
      name: 'Tanterem',
      value: lesson.value.classroom?.name
    },
    {
      name: 'Kezdés időpontja',
      value: dateTimeToString(lesson.value.startsAt)
    },
    {
      name: 'Befejezés időpontja',
      value: dateTimeToString(lesson.value.endsAt)
    },
    {
      name: 'Elmarad',
      value: yesOrNo(lesson.value.canceled)
    }
  ]
})

function resolveParams(): Params {
  return (route.params as unknown) as Params
}

function openEditDialog(): void {
  dialog({
    component: LessonCreateEditDialog,
    componentProps: {
      lessonToEdit: lesson.value
    }
  }).onOk(
    save(async (data: LessonEditCommand) => {
      lesson.value = await client.editLesson(lessonId, data)
    })
  )
}

async function loadData(): Promise<void> {
  lesson.value = await client.getLesson(lessonId)
}

loadData()
</script>

<style scoped>
.fill-card {
  flex-grow: 1;
  min-height: 1px;
}

.description {
  margin-top: 0;
  margin-bottom: 0;
  font-family: Poppins, 'Roboto', 'Arial', 'sans-serif';
}

.h-100 {
  height: 100%;
}
</style>
