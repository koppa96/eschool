import { Module } from 'vuex'
import {
  IMessageSendCommand,
  RecipientDto
} from '@/shared/generated-clients/class-register'
import { State } from '@/store/state'

export interface CoreState {
  newMessage: IMessageSendCommand
}

export const coreModule: Module<CoreState, State> = {
  state: () => ({
    newMessage: {
      text: undefined,
      subject: undefined,
      recipients: undefined
    }
  }),
  mutations: {
    setText(state: CoreState, text: string): void {
      state.newMessage.text = text
    },
    setSubject(state: CoreState, subject: string): void {
      state.newMessage.subject = subject
    },
    setRecipients(state: CoreState, recipients: RecipientDto[]): void {
      state.newMessage.recipients = recipients
    }
  }
}
