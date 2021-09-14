import TenantCreateEdit from '@/features/tenant-admin/components/TenantCreateEdit.vue'

export default {
  title: 'TenantAdmin/TenantCreateEdit',
  component: TenantCreateEdit,
  argTypes: {
    onSave: {},
    onCancel: {}
  }
}

const Template = args => ({
  components: { TenantCreateEdit },
  setup() {
    return { args }
  },
  template:
    '<div style="width: 800px"> <TenantCreateEdit v-bind="args" /> </div>'
})

export const Empty = Template.bind({})

export const Filled = Template.bind({})
Filled.args = {
  initialValue: {
    name: 'Lovassy László Gimnázium',
    address: '8200 Veszprém, Cserhát ltp. 10',
    officialEmailAddress: 'info@lovassy.hu',
    omIdentifier: '032484',
    headMaster: 'Schultz Zoltán'
  }
}
