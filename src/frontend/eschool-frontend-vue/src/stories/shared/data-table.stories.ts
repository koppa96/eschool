import { Story } from '@storybook/vue3'
import { Observable } from 'rxjs'
import { action } from '@storybook/addon-actions'
import DataTable from '@/shared/components/DataTable.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { dateToString } from '@/core/utils/display-helpers'
import { Item, mockDataAccess } from '@/stories/shared/mocks/data-table-items'

interface Props {
  title?: string
  addButtonText?: string
  columns: QTableColumn[]
  rowKey?: string
  flat?: boolean
  dataAccess: (
    pageSize: number,
    pageIndex: number
  ) => Promise<PagedListResponse>
  refresh$: Observable<void>
  hasDetails?: boolean
  editable?: boolean
  deletable?: boolean
}

export default {
  title: 'Shared/DataTable',
  component: DataTable,
  argTypes: {
    onViewDetails: {},
    onAdd: {},
    onEdit: {},
    onDelete: {}
  }
}

const Template: Story<Props> = args => ({
  components: { DataTable },
  setup() {
    return { args }
  },
  template: '<DataTable v-bind="args" />'
})

const TemplateWithCustomActions: Story<Props> = args => ({
  components: { DataTable },
  setup() {
    const likeClicked = action('onLikeClicked')
    const dislikeClicked = action('onDislikeClicked')
    return { args, likeClicked, dislikeClicked }
  },
  template: `
    <DataTable v-bind="args">
      <template #actions="{ row }">
        <q-btn
          dense
          round
          flat
          icon="thumb_up"
          @click.stop="likeClicked(row)"
        >
          <q-tooltip>Kedvelés</q-tooltip>
        </q-btn>
        <q-btn
          dense
          round
          flat
          icon="thumb_down"
          @click.stop="dislikeClicked(row)"
        >
          <q-tooltip>Nem kedvelés</q-tooltip>
        </q-btn>
      </template>
    </DataTable>`
})

const TemplateWithCustomTopRight: Story<Props> = args => ({
  components: { DataTable },
  setup() {
    const deleteAllClicked = action('onDeleteAllClicked')
    return { args, deleteAllClicked }
  },
  template: `
    <DataTable v-bind="args">
      <template #top-right>
        <q-btn color="negative" icon="delete" @click="deleteAllClicked()">
          Összes törlése
        </q-btn>
      </template>
    </DataTable>`
})

const columns: QTableColumn<Item>[] = [
  {
    name: 'name',
    label: 'Év',
    align: 'left',
    field: row => row.name
  },
  {
    name: 'birthDate',
    label: 'Születési dátum',
    align: 'left',
    field: row => dateToString(row.birthDate)
  },
  {
    name: 'gender',
    label: 'Nem',
    align: 'left',
    field: row => row.gender
  }
]

export const Emtpy = Template.bind({})
Emtpy.args = {
  title: 'Üres tábla',
  addButtonText: 'Elem hozzáadása',
  columns,
  dataAccess: (pageSize, pageIndex) =>
    Promise.resolve({
      pageIndex,
      pageSize,
      totalCount: 0,
      items: []
    })
}

export const WithData = Template.bind({})
WithData.args = {
  ...Emtpy.args,
  title: 'Adatokkal feltöltött tábla',
  dataAccess: mockDataAccess,
  hasDetails: true,
  editable: true,
  deletable: true
}

export const LoadingForever = Template.bind({})
LoadingForever.args = {
  ...Emtpy.args,
  dataAccess: () => new Promise<PagedListResponse>(() => {})
}

export const DelayedLoading = Template.bind({})
DelayedLoading.args = {
  ...WithData.args,
  dataAccess: (pageSize, pageIndex) => {
    return new Promise<PagedListResponse>(resolve => {
      setTimeout(() => {
        resolve(mockDataAccess(pageSize, pageIndex))
      }, 2000)
    })
  }
}

export const WithCustomActions = TemplateWithCustomActions.bind({})
WithCustomActions.args = {
  ...WithData.args
}

export const WithCustomTopRight = TemplateWithCustomTopRight.bind({})
WithCustomTopRight.args = {
  ...WithData.args
}
