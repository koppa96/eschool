<template>
  <q-page>
    <DataTable
      class="absolute-full q-ma-lg"
      :title="title"
      add-button-text="Tantárgy felvétele"
      :columns="columns"
      :data-access="fetchData"
      :refresh$="refreshSubject"
    >
      <template #actions>
        <q-btn dense round flat icon="event_busy" />
        <q-btn dense round flat icon="edit" />
        <q-btn color="negative" dense round flat icon="delete" />
      </template>
    </DataTable>
  </q-page>
</template>

<script setup lang="ts">
import { isString } from 'lodash-es'
import { useRoute } from 'vue-router'
import { computed, ref } from 'vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  ClassSchoolYearSubjectLessonsClient,
  LessonListResponse,
  SubjectDetailsResponse,
  SubjectsClient
} from '@/shared/generated-clients/class-register'
import { dateToString, yesOrNo } from '@/core/utils/display-helpers'
import { createClient } from '@/shared/api'
import { useAutocompletingSubject } from '@/core/utils/observable-lifecycle.util'
import DataTable from '@/shared/components/DataTable.vue'
import { PagedListResponse } from '@/shared/model/paged-list-response'

interface RouteParameters {
  classId: string
  schoolYearId: string
  subjectId: string
}

const columns: QTableColumn<LessonListResponse>[] = [
  {
    name: 'classroom',
    label: 'Tanterem',
    align: 'left',
    field: row => row.classroom?.name
  },
  {
    name: 'startsAt',
    label: 'Kezdés időpontja',
    align: 'left',
    field: row => dateToString(row.startsAt)
  },
  {
    name: 'endsAt',
    label: 'Befejezés időpontja',
    align: 'left',
    field: row => dateToString(row.endsAt)
  },
  {
    name: 'canceled',
    label: 'Elmarad',
    align: 'left',
    field: row => yesOrNo(row.canceled)
  }
]

const route = useRoute()
const subjectsClient = createClient(SubjectsClient)
const client = createClient(ClassSchoolYearSubjectLessonsClient)
const { schoolYearId, classId, subjectId } = resolveParameters()
const refreshSubject = useAutocompletingSubject()
const subject = ref(new SubjectDetailsResponse())

const title = computed(() => subject.value.name)

function resolveParameters(): RouteParameters {
  const { schoolYearId, classId, subjectId } = route.params
  return {
    schoolYearId: isString(schoolYearId) ? schoolYearId : '',
    classId: isString(classId) ? classId : '',
    subjectId: isString(subjectId) ? subjectId : ''
  }
}

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  return client.listLessons(
    schoolYearId,
    classId,
    subjectId,
    pageSize,
    pageIndex
  )
}

async function loadData(): Promise<void> {
  subject.value = await subjectsClient.getSubject(subjectId)
}

loadData()
</script>

<style scoped></style>
