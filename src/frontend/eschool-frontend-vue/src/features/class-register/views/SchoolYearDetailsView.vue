<template>
  <q-page>
    <q-card class="absolute-full q-ma-lg flex column">
      <DetailsHeader
        class="q-pa-md"
        :name="`${schoolYear.displayName} tanév`"
        @edit="openEditDialog()"
        @delete="openDeleteDialog()"
      />
      <DetailsGrid class="q-pa-md" :model-value="detailsGridData" />
      <ClassSchoolYearList :school-year-id="schoolYearId" class="fill-card" />
    </q-card>
  </q-page>
</template>

<script setup lang="ts">
import { isString } from 'lodash-es'
import { useRoute, useRouter } from 'vue-router'
import { computed, ref } from 'vue'
import { useQuasar } from 'quasar'
import SchoolYearCreateEditDialog from '../components/SchoolYearCreateEditDialog.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  ClassListResponse,
  ClassSchoolYearsClient,
  SchoolYearDetailsResponse,
  SchoolYearEditCommand,
  SchoolYearsClient
} from '@/shared/generated-clients/class-register'
import { createClient } from '@/shared/api'
import DetailsHeader from '@/shared/components/DetailsHeader.vue'
import DetailsGrid from '@/shared/components/DetailsGrid.vue'
import { DetailsGridItem } from '@/shared/model/details-grid.model'
import { useSaveAndDeleteNotifications } from '@/core/utils/save.utils'
import ClassSchoolYearList from '@/features/class-register/components/ClassSchoolYearList.vue'
import { dateToString } from '@/core/utils/display-helpers'

const columns: QTableColumn<ClassListResponse>[] = [
  {
    name: 'grade',
    label: 'Évfolyam',
    align: 'left',
    field: row => row.grade
  },
  {
    name: 'classType',
    label: 'Tagozat',
    align: 'left',
    field: row => row.classType?.name
  },
  {
    name: 'finishingSchoolYear',
    label: 'Végzés éve',
    align: 'left',
    field: row => row.finishingSchoolYear?.displayName
  }
]

const router = useRouter()
const route = useRoute()

const schoolYearId = resolveSchoolYearId()
const schoolYear = ref<SchoolYearDetailsResponse>(
  new SchoolYearDetailsResponse()
)
const schoolYearsClient = createClient(SchoolYearsClient)
const classSchoolYearsClient = createClient(ClassSchoolYearsClient)
const { dialog } = useQuasar()
const { save, deletion } = useSaveAndDeleteNotifications()

const detailsGridData = computed<DetailsGridItem[]>(() => {
  return [
    {
      name: 'Tanév kezdete',
      value: dateToString(schoolYear.value.startsAt)
    },
    {
      name: 'Félévzárás',
      value: dateToString(schoolYear.value.endOfFirstHalf)
    },
    {
      name: 'Tanévzárás',
      value: dateToString(schoolYear.value.endsAt)
    }
  ]
})

function resolveSchoolYearId(): string {
  if (isString(route.params.id)) {
    return route.params.id
  }

  return ''
}

async function loadData(): Promise<void> {
  schoolYear.value = await schoolYearsClient.getSchoolYear(schoolYearId)
}

function openEditDialog(): void {
  dialog({
    component: SchoolYearCreateEditDialog,
    componentProps: {
      schoolYearToEdit: schoolYear.value
    }
  }).onOk(
    save(async (data: SchoolYearEditCommand) => {
      schoolYear.value = await schoolYearsClient.editSchoolYear(
        schoolYearId,
        data
      )
    })
  )
}

function openDeleteDialog(): void {
  dialog({
    title: 'Megerősítés szükséges',
    message: `Biztosan törölni szeretné a ${schoolYear.value.displayName} tanévet?`,
    ok: 'Igen',
    cancel: 'Nem'
  }).onOk(
    deletion(async () => {
      await schoolYearsClient.deleteSchoolYear(schoolYearId)
      await router.push('/school-years')
    })
  )
}

loadData()
</script>

<style scoped>
.fill-card {
  flex-grow: 1;
}
</style>
