import { Story } from '@storybook/vue3'
import DetailsGrid from '@/shared/components/DetailsGrid.vue'
import { DetailsGridItem } from '@/shared/model/details-grid.model'
import {
  dateTimeToString,
  dateToString,
  yesOrNo
} from '@/core/utils/display-helpers'

interface Props {
  modelValue: DetailsGridItem[]
}

export default {
  title: 'Shared/DetailsGrid',
  component: DetailsGrid
}

const Template: Story<Props> = args => ({
  components: { DetailsGrid },
  setup() {
    return { args }
  },
  template: '<DetailsGrid v-bind="args" />'
})

export const Default = Template.bind({})
Default.args = {
  modelValue: [
    {
      name: 'String adat',
      value: 'Adat'
    },
    {
      name: 'Dátum',
      value: dateToString(new Date())
    },
    {
      name: 'Dátum és idő',
      value: dateTimeToString(new Date())
    },
    {
      name: 'Igen vagy nem',
      value: yesOrNo(true)
    },
    {
      name: 'Opcionális',
      value: undefined
    }
  ]
}
