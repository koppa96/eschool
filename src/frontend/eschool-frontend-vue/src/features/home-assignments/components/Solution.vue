<template>
  <div class="flex items-center justify-between">
    <h5 class="q-my-sm">Megoldás</h5>
    <q-btn
      v-if="!modelValue"
      color="primary"
      icon="add"
      @click="emit('create')"
    >
      Megoldás hozzáadása
    </q-btn>
    <q-btn
      v-else-if="!modelValue.turnInDate"
      color="positive"
      icon="cloud_upload"
      @click="emit('submit')"
    >
      Beadás
    </q-btn>
    <q-btn v-else icon="cloud_upload" flat disable>
      Beadva {{ dateTimeToString(modelValue.turnInDate) }}
    </q-btn>
  </div>
  <div v-if="modelValue">
    <div class="flex column items-center">
      <div
        v-for="file in modelValue.files"
        :key="file.id"
        class="flex justify-between file"
      >
        <span>{{ file.fileName }}</span>
        <div class="flex">
          <q-btn
            color="primary"
            icon="file_download"
            round
            flat
            @click="download(file)"
          >
            <q-tooltip>Letöltés</q-tooltip>
          </q-btn>
          <q-btn
            v-if="allowMutation"
            color="negative"
            icon="delete"
            round
            flat
            :disable="!!modelValue.turnInDate"
            @click="emit('deleteFile', file)"
          >
            <q-tooltip>Törlés</q-tooltip>
          </q-btn>
        </div>
      </div>
      <div v-if="allowMutation" class="flex file">
        <q-file
          v-model="selectedFile"
          class="grow"
          outlined
          clear-icon="close"
          clearable
          :disable="!!modelValue.turnInDate"
          label="Fájl kiválasztása"
        >
          <template #prepend>
            <q-icon name="attach_file" />
          </template>
        </q-file>
        <q-btn
          class="q-ml-sm"
          color="primary"
          flat
          :disable="!selectedFile || !!modelValue.turnInDate"
          icon="file_upload"
          @click="emit('upload', selectedFile)"
        >
          Feltöltés
        </q-btn>
      </div>
    </div>
  </div>
  <div v-else class="flex justify-center">
    <span>Még nincs létrehozva megoldás a feladathoz.</span>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import axios from 'axios'
import {
  FileResponse,
  FilesClient,
  HomeworkSolutionResponse,
  Body
} from '@/shared/generated-clients/home-assignments'
import { dateTimeToString } from '@/core/utils/display-helpers'
import { getBaseUrlFor } from '@/shared/api'

const props = defineProps<{
  modelValue: HomeworkSolutionResponse | null
  allowMutation: boolean
}>()

const emit = defineEmits<{
  (event: 'upload', file: File): void
  (event: 'create'): void
  (event: 'deleteFile', file: FileResponse): void
  (event: 'submit'): void
}>()

const selectedFile = ref<File>()
const review = ref<Body | HomeworkReviewEd>()
async function download(file: FileResponse): Promise<void> {
  const homeAssignmentsBaseUrl = getBaseUrlFor(FilesClient)
  if (homeAssignmentsBaseUrl) {
    const { data } = await axios.get<Blob>(
      `${homeAssignmentsBaseUrl}/api/files/${file.id}`,
      {
        responseType: 'blob'
      }
    )

    const url = URL.createObjectURL(data)

    const anchor = document.createElement('a')
    anchor.href = url
    anchor.download = file.fileName!
    anchor.click()

    URL.revokeObjectURL(url)
  }
}
</script>

<style scoped>
.file {
  width: 25vw;
}

.grow {
  flex-grow: 1;
  min-width: 0;
}
</style>
