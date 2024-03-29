import { RouteLocationNormalizedLoaded } from 'vue-router'

export interface BreadCrumbItem {
  path: string
  name: string
  disabled: boolean
}

export interface RouteMetaWithName {
  name: string
}

export interface RouteMetaWithResolve {
  resolveName: (route: RouteLocationNormalizedLoaded) => Promise<string>
}

export type RouteMeta =
  | RouteMetaWithName
  | (RouteMetaWithResolve & {
      disabled?: boolean
    })

export function isRouteMeta(value: any): value is RouteMeta {
  return value.name || value.resolveName
}

export function isRouteMetaWithName(value: any): value is RouteMetaWithName {
  return value.name
}
