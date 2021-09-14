import TenantCreateEdit from '../../features/tenant-admin/components/TenantCreateEdit.vue'

export default {
  title: 'TenantAdmin/TenantCreateEdit',
  component: TenantCreateEdit
}

const Template = args => ({
  components: { TenantCreateEdit },
  template: '<TenantCreateEdit />'
})

export const Empty = Template.bind({})
