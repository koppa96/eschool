<template>
  <q-page>
    <q-card class="absolute-full q-ma-lg q-pa-md flex column">
      <h4 class="q-my-sm">{{ title }}</h4>
      <h5 class="q-my-sm">Feladatleírás</h5>
      <pre class="description">{{ homework?.description }}</pre>
      <Solution :model-value="solution" :allow-mutation="false" />
      <Review
        :model-value="solution?.review"
        class="fill-card"
        allow-mutation
        @change="saveReview($event)"
      />
    </q-card>
  </q-page>
</template>

<script setup lang="ts">
import { computed, ref } from 'vue'
import { useRoute } from 'vue-router'
import { createClient } from '@/shared/api'
import {
  FilesClient,
  HomeworkDetailsResponse,
  HomeworksClient,
  HomeworkSolutionResponse,
  HomeworkSolutionsClient,
  SolutionsClient,
  Body
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
const { homeworkId, solutionId } = resolveParams()
const title = computed(() => homework.value?.title ?? 'Házi feladat részletei')
const filesClient = createClient(FilesClient)

function resolveParams(): { homeworkId: string; solutionId: string } {
  return route.params as any
}

async function loadData(): Promise<void> {
  const [homeworkResponse, solutionResponse] = await load(() =>
    Promise.all([
      client.getHomework(homeworkId),
      solutionsClient.getSolution2(solutionId)
    ])
  )

  homework.value = homeworkResponse
  solution.value = solutionResponse
}

async function saveReview(review: Body): Promise<void> {
  await save(async () => {
    await solutionsClient.createOrEditReview2(solutionId, review)
    await solutionsClient.getSolution2(solutionId)
  })()
}

loadData()
</script>

<style scoped>
.description {
  margin-top: 0;
  margin-bottom: 0;
  font-family: Poppins, 'Roboto', 'Arial', 'sans-serif';
}

.fill-card {
  flex-grow: 1;
  min-height: 1px;
}
</style>
