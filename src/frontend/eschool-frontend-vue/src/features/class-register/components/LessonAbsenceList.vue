<template>
  <DataTable
    title="Hiányzó diákok"
    add-button-text="Hiányzás rögzítése"
    :columns="columns"
    :data-access="fetchData"
    :refresh$="refreshSubject"
    :editable="false"
    :has-details="false"
    flat
    @add="createAbsence()"
    @delete="deleteAbsence($event)"
  />
</template>

<script setup lang="ts">
import { useQuasar } from 'quasar'
import AbsenceCreateDialog from './AbsenceCreateDialog.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  AbsencesClient,
  ClassSchoolYearSubjectLessonsClient,
  LessonAbsenceListResponse
} from '@/shared/generated-clients/class-register'
import { absenceState } from '@/core/utils/display-helpers'
import { useAutocompletingSubject } from '@/core/utils/observable-lifecycle.util'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { createClient } from '@/shared/api'
import { useConfirmDialog } from '@/core/utils/dialogs'
import { useSaveAndDeleteNotifications } from '@/core/utils/save.utils'
import DataTable from '@/shared/components/DataTable.vue'

const props = defineProps<{
  lessonId: string
  classId: string
  schoolYearId: string
  subjectId: string
}>()

const columns: QTableColumn<LessonAbsenceListResponse>[] = [
  {
    name: 'name',
    label: 'Diák neve',
    align: 'left',
    field: row => row.student?.name
  },
  {
    name: 'status',
    label: 'Hiányzás állapota',
    align: 'left',
    field: row => absenceState(row.absenceState)
  }
]
const refreshSubject = useAutocompletingSubject()
const client = createClient(AbsencesClient)
const lessonsClient = createClient(ClassSchoolYearSubjectLessonsClient)
const confirm = useConfirmDialog()
const { save, deletion } = useSaveAndDeleteNotifications()
const { dialog } = useQuasar()

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  return client.listLessonAbsences2(
    props.lessonId,
    pageSize,
    pageIndex,
    props.schoolYearId,
    props.classId,
    props.subjectId
  )
}

function createAbsence(): void {
  dialog({
    component: AbsenceCreateDialog,
    componentProps: {
      classId: props.classId
    }
  }).onOk(
    save(async (studentId: string) => {
      await lessonsClient.createAbsence(props.lessonId, studentId)
      refreshSubject.next()
    })
  )
}

async function deleteAbsence(
  absence: LessonAbsenceListResponse
): Promise<void> {
  const result = await confirm('Biztosan törölni szeretné a hiányzást?')

  if (result) {
    await deletion(async () => {
      await client.deleteAbsence(absence.id)
      refreshSubject.next()
    })()
  }
}
</script>
