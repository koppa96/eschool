import { Story } from '@storybook/vue3'
import { action } from '@storybook/addon-actions'
import { ref } from 'vue'
import DatePicker from '@/shared/components/DatePicker.vue'
import { ValidationFunction } from '@/core/utils/validation-functions'

interface Props {
  modelValue: Date | string | null
  label: string
  rules?: ValidationFunction[]
  disable?: boolean
}

export default {
  title: 'Shared/DatePicker',
  component: DatePicker
}

const Template: Story<Props> = args => ({
  components: { DatePicker },
  setup() {
    const handler = action('update:model-value')
    const modelValue = ref<Date | string | null>(args.modelValue)

    function handleSelection(value: string): void {
      modelValue.value = value
      handler(value)
    }

    return { args, modelValue, handleSelection }
  },
  template:
    '<DatePicker style="width: 50%" v-bind="args" :model-value="modelValue" @update:model-value="handleSelection($event)" />'
})

export const Empty = Template.bind({})
Empty.args = {
  label: 'Dátum választása',
  modelValue: null
}

export const WithDateValue = Template.bind({})
WithDateValue.args = {
  label: 'Dátum választása',
  modelValue: new Date()
}

export const WithStringValue = Template.bind({})
WithStringValue.args = {
  label: 'Dátum választása',
  modelValue: '2021-09-28'
}

export const DisabledEmpty = Template.bind({})
DisabledEmpty.args = {
  ...Empty.args,
  disable: true
}

export const DisabledWithValue = Template.bind({})
DisabledWithValue.args = {
  ...DisabledEmpty.args,
  modelValue: new Date()
}
