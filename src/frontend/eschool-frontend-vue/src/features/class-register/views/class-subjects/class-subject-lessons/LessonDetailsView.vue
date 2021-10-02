<template>
  <q-page>
    <q-card class="absolute-full q-ma-lg flex column">
      <DetailsHeader
        class="q-pa-md"
        :name="title"
        :deletable="false"
        @edit="openEditDialog()"
      />
      <pre class="q-px-md description">{{ lesson.description }}</pre>
      <DetailsGrid class="q-pa-md" :model-value="detailsGridData" />
      <LessonAbsenceList
        :lesson-id="lessonId"
        :class-id="classId"
        class="fill-card"
      />
    </q-card>
  </q-page>
</template>

<script setup lang="ts">
import { computed, ref } from 'vue'
import { isString } from 'lodash-es'
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

const route = useRoute()
const client = createClient(ClassSchoolYearSubjectLessonsClient)
const lesson = ref(new LessonDetailsResponse())
const lessonId = resolveLessonId()
const classId = resolveClassId()
const { dialog } = useQuasar()
const { save } = useSaveAndDeleteNotifications()
const title = computed(() => lesson.value.title ?? 'Óra részletei')

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

function resolveLessonId(): string {
  if (isString(route.params.lessonId)) {
    return route.params.lessonId
  }

  return ''
}

function resolveClassId(): string {
  if (isString(route.params.classId)) {
    return route.params.classId
  }

  return ''
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
}

.description {
  margin-top: 0;
  margin-bottom: 0;
  font-family: Poppins, 'Roboto', 'Arial', 'sans-serif';
}
</style>
