import { createStore, Store, useStore as baseUseStore } from 'vuex'
import { InjectionKey } from 'vue'
import { coreModule } from '@/core/store/store'
import { State } from '@/store/state'

export const key: InjectionKey<Store<State>> = Symbol('Store key')

export const store = createStore({
  modules: {
    core: coreModule
  }
})

export function useStore(): Store<State> {
  return baseUseStore(key)
}
