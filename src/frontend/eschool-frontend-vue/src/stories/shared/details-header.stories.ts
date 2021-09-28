import { Story } from '@storybook/vue3'
import DetailsHeader from '@/shared/components/DetailsHeader.vue'

interface Props {
  name?: string
  editable?: boolean
  deletable?: boolean
}

export default {
  title: 'Shared/DetailsHeader',
  component: DetailsHeader,
  argTypes: {
    onEdit: {},
    onDelete: {}
  }
}

const Template: Story<Props> = args => ({
  components: { DetailsHeader },
  setup() {
    return { args }
  },
  template: '<DetailsHeader v-bind="args" />'
})

export const Default = Template.bind({})
Default.args = {
  name: 'Ez egy cím'
}

export const NotEditable = Template.bind({})
NotEditable.args = {
  name: 'Ez nem szerkeszthető',
  editable: false
}

export const NotDeletable = Template.bind({})
NotDeletable.args = {
  name: 'Ez nem törölhető',
  deletable: false
}
