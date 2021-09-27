<template>
  <q-breadcrumbs active-color="white" class="text-blue-grey-2">
    <template #separator>
      <q-icon size="1.5em" name="chevron_right" color="white" />
    </template>

    <q-breadcrumbs-el
      v-for="item in items"
      :key="item.path"
      :label="item.name"
      :to="item.path"
    />
  </q-breadcrumbs>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router'
import { ref, watch } from 'vue'
import {
  BreadCrumbItem,
  isRouteMeta,
  isRouteMetaWithName
} from '@/core/models/breadcrumb.models'

const route = useRoute()
const items = ref<BreadCrumbItem[]>([])
const paramRegex = /:[a-zA-Z0-9]+/g

watch(
  () => route.matched,
  value => {
    items.value = []

    for (const item of value) {
      if (!isRouteMeta(item.meta)) {
        continue
      }

      let path = item.path
      const params = path.match(paramRegex)
      if (params?.length && params?.length > 0) {
        for (const param of params) {
          const paramValue = route.params[param.substr(1)] as string
          path = path.replace(param, paramValue)
        }
      }

      const breadCrumbItem: BreadCrumbItem = {
        path,
        name: ''
      }

      const index = items.value.push(breadCrumbItem) - 1
      if (isRouteMetaWithName(item.meta)) {
        breadCrumbItem.name = item.meta.name
      } else {
        item.meta.resolveName(route).then(name => {
          items.value.splice(index, 1, {
            path,
            name
          })
        })
      }
    }
  }
)
</script>

<style scoped></style>
