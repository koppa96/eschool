<template>
  <q-page>
    <q-card class="absolute-full q-ma-lg q-pa-md">
      <h4 class="q-my-sm">{{ title }}</h4>
      <h5 class="q-my-sm">Feladatleírás</h5>
      <pre class="description">{{ homework?.description }}</pre>
      <Solution
        :model-value="solution"
        allow-mutation
        @create="createSolution()"
        @submit="submitSolution()"
        @deleteFile="deleteFile($event)"
        @upload="uploadFile($event)"
      />
      <Review :model-value="solution?.review" :allow-mutation="false" />
    </q-card>
  </q-page>
</template>

<script setup lang="ts">
import { computed, ref } from 'vue'
import { useRoute } from 'vue-router'
import { createClient } from '@/shared/api'
import {
  FileResponse,
  FilesClient,
  HomeworkDetailsResponse,
  HomeworksClient,
  HomeworkSolutionResponse,
  HomeworkSolutionsClient,
  SolutionsClient
} from '@/shared/generated-clients/home-assignments'
import { useLoader } from '@/core/utils/loading.utils'
import Solution from '@/features/home-assignments/components/Solution.vue'
import { useSaveAndDeleteNotifications } from '@/core/utils/save.utils'
import { useConfirmDialog } from '@/core/utils/dialogs'
import Review from '@/features/home-assignments/components/Review.vue'

const confirm = useConfirmDialog()
const { save, deletion } = useSaveAndDeleteNotifications()
const load = useLoader()
const route = useRoute()
const client = createClient(HomeworksClient)
const solutionsClient = createClient(SolutionsClient)
const homeworkSolutionsClient = createClient(HomeworkSolutionsClient)
const homework = ref<HomeworkDetailsResponse>()
const solution = ref<HomeworkSolutionResponse | null>()
const homeworkId = resolveParams()
const title = computed(() => homework.value?.title ?? 'Házi feladat részletei')
const filesClient = createClient(FilesClient)

function resolveParams(): string {
  return route.params.homeworkId as string
}

async function loadData(): Promise<void> {
  const [homeworkResponse, solutionResponse] = await load(() =>
    Promise.all([
      client.getHomework(homeworkId),
      solutionsClient.getMySolution(homeworkId)
    ])
  )

  homework.value = homeworkResponse
  solution.value = solutionResponse
}

async function createSolution(): Promise<void> {
  solution.value = await homeworkSolutionsClient.createSolution(homeworkId)
}

async function submitSolution(): Promise<void> {
  const result = await confirm(
    'Biztosan szeretné beadni a házi feladat megoldást? A megoldás beadás után nem szerkeszthető.'
  )

  if (result) {
    await save(async () => {
      solution.value = await solutionsClient.submitSolution2(solution.value!.id)
    })()
  }
}

async function uploadFile(file: File): Promise<void> {
  await save(async () => {
    solution.value = await solutionsClient.uploadFile(
      solution.value!.id,
      homeworkId,
      { fileName: file.name, data: file }
    )
  })()
}

async function deleteFile(file: FileResponse): Promise<void> {
  const result = await confirm(
    'Biztosan törölni szeretné a házi feladat megoldást? Ez az összes fájl törlésével jár.'
  )

  if (result) {
    await deletion(async () => {
      solution.value = await filesClient.deleteFile3(file.id)
    })()
  }
}

loadData()
</script>

<style scoped>
.description {
  margin-top: 0;
  margin-bottom: 0;
  font-family: Poppins, 'Roboto', 'Arial', 'sans-serif';
}
</style>
