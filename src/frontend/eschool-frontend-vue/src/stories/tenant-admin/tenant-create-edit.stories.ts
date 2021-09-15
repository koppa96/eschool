import { Story } from '@storybook/vue3'
import { ref } from 'vue'
import TenantCreateEdit from '@/features/tenant-admin/components/TenantCreateEditDialog.vue'
import { TenantDetailsResponse } from '@/shared/generated-clients/identity-provider'
import { Dialog } from '@/core/utils/dialog'

interface Props {
  tenantToEdit: TenantDetailsResponse
}

export default {
  title: 'TenantAdmin/TenantCreateEdit',
  component: TenantCreateEdit,
  argTypes: {
    onSave: {},
    onCancel: {}
  }
}

const Template: Story<Props> = args => ({
  components: { TenantCreateEdit },
  setup() {
    const dialog = ref<Dialog>()

    function showDialog(): void {
      dialog.value?.open()
    }

    return { args, showDialog }
  },
  template:
    '<div style="width: 800px"> <TenantCreateEditDialog ref="dialog" v-bind="args" /> <q-btn color="primary" @click="showDialog()">Megjelenítés</q-btn> </div>'
})

export const Hidden = Template.bind({})

export const Filled = Template.bind({})
Filled.args = {
  tenantToEdit: new TenantDetailsResponse({
    id: '2527de7e-0797-4e39-a403-5229df03fef9',
    name: 'Lovassy László Gimnázium',
    address: '8200 Veszprém, Cserhát ltp. 10',
    officialEmailAddress: 'info@lovassy.hu',
    omIdentifier: '032484',
    headMaster: 'Schultz Zoltán'
  })
}
