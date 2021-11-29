<template>
  <q-page>
    <q-card class="absolute-full q-ma-lg flex column no-wrap">
      <DetailsHeader
        class="q-pa-md"
        :name="title"
        @edit="editClass()"
        @delete="deleteClass()"
      />
      <DetailsGrid class="q-pa-md" :model-value="detailsGridData" />
      <ClassStudentList :class-id="classId" class="fill-card" />
    </q-card>
  </q-page>
</template>

<script setup lang="ts">
import { isString } from 'lodash-es'
import { useRoute, useRouter } from 'vue-router'
import { computed, ref } from 'vue'
import { useQuasar } from 'quasar'
import {
  ClassDetailsResponse,
  ClassEditCommand,
  ClassesClient
} from '@/shared/generated-clients/class-register'
import { createClient } from '@/shared/api'
import DetailsHeader from '@/shared/components/DetailsHeader.vue'
import DetailsGrid from '@/shared/components/DetailsGrid.vue'
import { DetailsGridItem } from '@/shared/model/details-grid.model'
import { useSaveAndDeleteNotifications } from '@/core/utils/save.utils'
import { displayClass, yesOrNo } from '@/core/utils/display-helpers'
import ClassStudentList from '@/features/class-register/components/ClassStudentList.vue'
import ClassEditDialog from '@/features/class-register/components/ClassEditDialog.vue'
import { useLoader } from '@/core/utils/loading.utils'
import { useConfirmDialog } from '@/core/utils/dialogs'

const confirm = useConfirmDialog()
const load = useLoader()
const router = useRouter()
const route = useRoute()
const client = createClient(ClassesClient)
const classId = resolveClassId()
const _class = ref<ClassDetailsResponse>(new ClassDetailsResponse())

const { dialog } = useQuasar()
const { save, deletion } = useSaveAndDeleteNotifications()

const title = computed(() => displayClass(_class.value))
const detailsGridData = computed<DetailsGridItem[]>(() => {
  return [
    {
      name: 'Évfolyam',
      value: `${_class.value.grade}`
    },
    {
      name: 'Tagozat',
      value: _class.value.classType?.name
    },
    {
      name: 'Osztályfőnök',
      value: _class.value.headTeacher?.name
    },
    {
      name: 'Végzett',
      value: yesOrNo(_class.value.didFinish)
    },
    {
      name: 'Kezdés tanéve',
      value: _class.value.startingSchoolYear?.displayName
    },
    {
      name: 'Végzés tanéve',
      value: _class.value.finishingSchoolYear?.displayName
    }
  ]
})

function resolveClassId(): string {
  if (isString(route.params.id)) {
    return route.params.id
  }

  return ''
}

async function loadData(): Promise<void> {
  _class.value = await client.getClass(classId)
}

async function editClass(): Promise<void> {
  const details = await load(() => client.getClass(_class.value.id))
  dialog({
    component: ClassEditDialog,
    componentProps: {
      classToEdit: details
    }
  }).onOk(
    save(async (data: ClassEditCommand) => {
      _class.value = await client.editClass(_class.value.id, data)
    })
  )
}

async function deleteClass(): Promise<void> {
  const result = await confirm(
    `Biztosan törölni szeretné a ${displayClass(_class.value)} osztályt?`
  )

  if (result) {
    await deletion(async () => {
      await client.deleteClass(_class.value.id)
      await router.push('/classes')
    })()
  }
}

loadData()
</script>

<style scoped>
.fill-card {
  flex-grow: 1;
  min-height: 1px;
}
</style>
