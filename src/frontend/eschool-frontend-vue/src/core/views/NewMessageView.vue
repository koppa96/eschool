<template>
  <q-page>
    <q-card class="absolute-full q-ma-lg q-pa-md flex column">
      <h5 class="q-mt-none q-mb-md">Új üzenet</h5>
      <q-form ref="form" class="flex column grow" greedy @submit="send()">
        <q-select
          v-model="recipients"
          :options="recipientList"
          option-label="name"
          option-value="id"
          use-input
          outlined
          multiple
          use-chips
          label="Címzettek"
          :rules="rules.recipients"
          @filter="filterRecipients"
        />
        <q-input
          v-model="subject"
          label="Tárgy"
          outlined
          :rules="rules.subject"
        />
        <q-input
          v-model="text"
          class="text grow"
          label="Üzenet szövege"
          outlined
          autogrow
          :rules="rules.text"
        />
        <div class="flex justify-between">
          <q-btn
            color="negative"
            icon="delete"
            type="button"
            flat
            @click="clear()"
          >
            Elvetés
          </q-btn>
          <q-btn color="primary" icon="send" type="submit">Küldés</q-btn>
        </div>
      </q-form>
    </q-card>
  </q-page>
</template>

<script setup lang="ts">
import { computed, ref } from 'vue'
import { QForm } from 'quasar'
import { useStore } from '@/store/store'
import {
  MessagesClient,
  MessageSendCommand,
  ProblemDetails,
  RecipientDto
} from '@/shared/generated-clients/messaging'
import { createClient } from '@/shared/api'
import { useNotifications } from '@/core/utils/notifications'
import { Rules } from '@/shared/model/rules'
import { required, selectionRequired } from '@/core/utils/validation-functions'

const notifications = useNotifications()
const store = useStore()
const client = createClient(MessagesClient)
const recipientList = ref<RecipientDto[]>([])
const form = ref<QForm>()

const rules: Rules<MessageSendCommand> = {
  recipients: [selectionRequired],
  subject: [required],
  text: [required]
}

const recipients = computed({
  get: () => store.state.core.newMessage.recipients,
  set: (value: RecipientDto[] | undefined) =>
    store.commit('setRecipients', value)
})

const subject = computed({
  get: () => store.state.core.newMessage.subject,
  set: (value: string | undefined) => store.commit('setSubject', value)
})

const text = computed({
  get: () => store.state.core.newMessage.text,
  set: (value: string | undefined) => store.commit('setText', value)
})

async function send(): Promise<void> {
  const message = new MessageSendCommand({
    ...store.state.core.newMessage
  })

  try {
    await client.sendMessage(message)
    notifications.success('Sikeres küldés')
  } catch (e) {
    notifications.failure((e as ProblemDetails)?.detail ?? 'Sikertelen küldés')
  }
}

async function filterRecipients(
  searchText: string,
  update: (callback: () => void) => void
): Promise<void> {
  if (!searchText) {
    return
  }

  const result = await client.listRecipients(searchText, 50, 0)
  update(() => {
    recipientList.value = result.items ?? []
  })
}

function clear(): void {
  subject.value = undefined
  text.value = undefined
  recipients.value = undefined
  form.value?.resetValidation()
}
</script>

<style scoped>
.grow {
  flex-grow: 1;
}

.text >>> .q-field__control.relative-position.row.no-wrap {
  height: 100% !important;
}
</style>
