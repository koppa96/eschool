<template>
  <q-page class="q-pa-lg flex column">
    <DataTable
      class="full-page-table"
      :title="subject.name"
      add-button-text="Tanár hozzárendelése"
      :columns="columns"
      :data-access="fetchData"
      :refresh$="refreshSubject"
      @add="createSubjectTeacher()"
      @delete="deleteSubjectTeacher($event)"
    />
  </q-page>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useRoute } from 'vue-router'
import { isString } from 'lodash-es'
import { Subject } from 'rxjs'
import { useQuasar } from 'quasar'
import SubjectTeacherCreateDialog from '../components/SubjectTeacherCreateDialog.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  SubjectListResponse,
  SubjectsClient,
  UserRoleListResponse
} from '@/shared/generated-clients/class-register'
import { createClient } from '@/shared/api'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import DataTable from '@/shared/components/DataTable.vue'
import { useNotifications } from '@/core/utils/notifications'

const columns: QTableColumn<UserRoleListResponse>[] = [
  {
    name: 'name',
    label: 'Tanár neve',
    align: 'left',
    field: row => row.name
  }
]

const notifications = useNotifications()
const quasar = useQuasar()
const refreshSubject = new Subject<void>()
const route = useRoute()
const subjectsClient = createClient(SubjectsClient)
const subject = ref<SubjectListResponse>(new SubjectListResponse())
const subjectId = resolveSubjectId()

function resolveSubjectId(): string {
  if (isString(route.params.id)) {
    return route.params.id
  }

  return ''
}

async function loadData(): Promise<void> {
  subject.value = await subjectsClient.getSubject(subjectId)
}

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  return subjectsClient.getTeachers(subjectId, pageSize, pageIndex)
}

function createSubjectTeacher(): void {
  quasar
    .dialog({
      component: SubjectTeacherCreateDialog
    })
    .onOk(async (teacherId: string) => {
      try {
        await subjectsClient.assignTeacherToSubject(subjectId, teacherId)
        refreshSubject.next()
        notifications.success('Sikeres mentés')
      } catch (err) {
        notifications.failure('Sikertelen mentés')
      }
    })
}

function deleteSubjectTeacher(subjectTeacher: UserRoleListResponse): void {
  quasar
    .dialog({
      title: 'Megerősítés szükséges',
      message: `Biztos benne, hogy szeretné ${subjectTeacher.name} tanárt eltávolítani a ${subject.value.name} tanárok közül?`,
      cancel: 'Nem',
      ok: 'Igen'
    })
    .onOk(async () => {
      try {
        await subjectsClient.unassignTeacherFromSubject(
          subjectId,
          subjectTeacher.id
        )
        refreshSubject.next()
        notifications.success('Sikeres mentés')
      } catch (err) {
        notifications.failure('Sikertelen mentés')
      }
    })
}

loadData()
</script>

<style scoped></style>
