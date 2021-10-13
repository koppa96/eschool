<template>
  <div class="flex column">
    <div class="flex justify-between items-center">
      <h5 class="q-my-sm">Értékelés</h5>
      <q-btn
        v-if="allowMutation || !data"
        color="primary"
        @click="createReview()"
      >
        Értékelés hozzáadása
      </q-btn>
    </div>
    <q-form
      v-if="allowMutation && !!data"
      greedy
      class="flex column grow"
      @submit="emit('change', data)"
    >
      <q-select
        v-model="data.outcome"
        label="Eredmény"
        :options="homeworkReviewOutcomes"
        option-value="id"
        option-label="name"
        emit-value
        map-options
        outlined
        :rules="rules.outcome"
      />
      <q-input
        v-model="data.comment"
        class="text grow"
        label="Megjegyzés"
        autogrow
        outlined
      />
      <div class="flex justify-end">
        <q-btn color="positive" icon="save_alt" class="q-mt-md" type="submit">
          Értékelés mentése
        </q-btn>
      </div>
    </q-form>
    <div v-else-if="!allowMutation && !!modelValue">
      <h6 class="q-my-sm">{{ homeworkReviewOutcome(modelValue.outcome) }}</h6>
      <DetailsGrid class="q-my-sm" :model-value="detailsGridData" />
      <span><strong>Megjegyzés:</strong></span>
      <pre class="description">{{ modelValue.comment }}</pre>
    </div>
    <div v-else class="flex justify-center">
      <span>A megoldás még nem került értékelésre.</span>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, ref, watch } from 'vue'
import {
  HomeworkReviewResponse,
  Body
} from '@/shared/generated-clients/home-assignments'
import { homeworkReviewOutcomes } from '@/features/home-assignments/models/homework-review-outcomes'
import { Rules } from '@/shared/model/rules'
import { required } from '@/core/utils/validation-functions'
import {
  dateTimeToString,
  homeworkReviewOutcome
} from '@/core/utils/display-helpers'
import DetailsGrid from '@/shared/components/DetailsGrid.vue'
import { DetailsGridItem } from '@/shared/model/details-grid.model'

const props = defineProps<{
  modelValue: HomeworkReviewResponse
  allowMutation: boolean
}>()

const data = ref<Body>()
const detailsGridData = computed<DetailsGridItem[]>(() =>
  props.modelValue
    ? [
        {
          name: 'Értékelés ideje',
          value: props.modelValue.lastModifiedAt
            ? dateTimeToString(props.modelValue.lastModifiedAt)
            : dateTimeToString(props.modelValue.createdAt)
        },
        {
          name: 'Értékelő',
          value:
            props.modelValue.lastModifiedBy?.name ??
            props.modelValue.createdBy?.name
        }
      ]
    : []
)

const emit = defineEmits<{
  (event: 'change', value: Body): void
}>()

function createReview(): void {
  data.value = new Body()
}

const rules: Rules<Body> = {
  outcome: [required]
}

watch(
  () => props.modelValue,
  value => {
    if (value) {
      data.value = new Body({
        ...value
      })
    }
  },
  { immediate: true }
)
</script>

<style scoped>
.description {
  margin-top: 0;
  margin-bottom: 0;
  font-family: Poppins, 'Roboto', 'Arial', 'sans-serif';
}

.grow {
  flex-grow: 1;
}

.text >>> .q-field__control.relative-position.row.no-wrap {
  height: 100% !important;
}
</style>
