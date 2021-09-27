<template>
  <q-page>
    <DataTable
      class="absolute-full q-ma-lg"
      :title="title"
      add-button-text="Tantárgy felvétele"
      :has-details="false"
      :columns="columns"
      :data-access="fetchData"
      :refresh$="refreshSubject"
      @add="createClassSchoolYearSubject()"
      @edit="editClassSchoolYearSubject($event)"
      @delete="deleteClassSchoolYearSubject($event)"
    />
  </q-page>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router'
import { isString } from 'lodash-es'
import { useQuasar } from 'quasar'
import { computed, ref } from 'vue'
import ClassSchoolYearSubjectCreateDialog from '../../../components/ClassSchoolYearSubjectCreateDialog.vue'
import ClassSchoolYearSubjectEditDialog from '../../../components/ClassSchoolYearSubjectEditDialog.vue'
import DataTable from '@/shared/components/DataTable.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  ClassDetailsResponse,
  ClassesClient,
  ClassSchoolYearSubjectsClient,
  SchoolYearDetailsResponse,
  SchoolYearsClient,
  SubjectListResponse,
  SubjectsClient
} from '@/shared/generated-clients/class-register'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { createClient } from '@/shared/api'
import { useAutocompletingSubject } from '@/core/utils/observable-lifecycle.util'
import { useLoader } from '@/core/utils/loading.utils'
import { useSaveAndDeleteNotifications } from '@/core/utils/save.utils'
import {
  ClassSchoolYearSubjectCreateDialogResult,
  ClassSchoolYearSubjectEditDialogResult
} from '@/features/class-register/models/class-schoolyear-subject.models'
import { displayClass } from '@/core/utils/display-helpers'

interface RouteParameters {
  schoolYearId: string
  classId: string
}

const columns: QTableColumn<SubjectListResponse>[] = [
  {
    name: 'name',
    label: 'Tárgy neve',
    align: 'left',
    field: row => row.name
  }
]

const route = useRoute()
const { dialog } = useQuasar()
const load = useLoader()
const { save, deletion } = useSaveAndDeleteNotifications()
const { schoolYearId, classId } = resolveParameters()
const schoolYearsClient = createClient(SchoolYearsClient)
const classesClient = createClient(ClassesClient)
const subjectsClient = createClient(SubjectsClient)
const client = createClient(ClassSchoolYearSubjectsClient)
const refreshSubject = useAutocompletingSubject()
const schoolYear = ref<SchoolYearDetailsResponse>(
  new SchoolYearDetailsResponse()
)
const _class = ref<ClassDetailsResponse>(new ClassDetailsResponse())

const title = computed(
  () =>
    `${_class.value.grade}. ${_class.value.classType?.name} tantárgyai a ${schoolYear.value.displayName} tanévben`
)

function resolveParameters(): RouteParameters {
  const { schoolYearId, classId } = route.params
  return {
    schoolYearId: isString(schoolYearId) ? schoolYearId : '',
    classId: isString(classId) ? classId : ''
  }
}

async function loadData(): Promise<void> {
  schoolYear.value = await schoolYearsClient.getSchoolYear(schoolYearId)
  _class.value = await classesClient.getClass(classId)
}

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  return client.listClassSchoolYearSubjects(
    schoolYearId,
    classId,
    pageIndex,
    pageSize
  )
}

async function createClassSchoolYearSubject(): Promise<void> {
  const subjects = await load(() => subjectsClient.listSubjects(50, 0))
  dialog({
    component: ClassSchoolYearSubjectCreateDialog,
    componentProps: {
      subjects: subjects.items
    }
  }).onOk(
    save(async (data: ClassSchoolYearSubjectCreateDialogResult) => {
      await client.createClassSchoolYearSubject(
        schoolYearId,
        classId,
        data.subjectId,
        data.teacherIds
      )
      refreshSubject.next()
    })
  )
}

async function editClassSchoolYearSubject(
  subject: SubjectListResponse
): Promise<void> {
  const details = await load(() =>
    client.getDetails(schoolYearId, classId, subject.id)
  )

  dialog({
    component: ClassSchoolYearSubjectEditDialog,
    componentProps: {
      subject,
      initialTeacherIds: details.teachers?.map(teacher => teacher.id)
    }
  }).onOk(
    save(async (data: ClassSchoolYearSubjectEditDialogResult) => {
      await client.editClassSchoolYearSubject(
        schoolYearId,
        classId,
        subject.id,
        data.teacherIds
      )
      refreshSubject.next()
    })
  )
}

function deleteClassSchoolYearSubject(subject: SubjectListResponse): void {
  dialog({
    title: 'Megerősítés szükséges',
    message: `Biztosan törölni szeretné a ${
      subject.name
    } tantárgyat a ${displayClass(_class.value)} osztály tantárgyai közül a ${
      schoolYear.value.displayName
    } tanévben?`,
    ok: 'Igen',
    cancel: 'Nem'
  }).onOk(
    deletion(async () => {
      await client.deleteClassSchoolYearSubject(
        schoolYearId,
        classId,
        subject.id
      )
      refreshSubject.next()
    })
  )
}

loadData()
</script>
