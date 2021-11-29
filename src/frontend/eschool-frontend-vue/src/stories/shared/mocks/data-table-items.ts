import { PagedListResponse } from '@/shared/model/paged-list-response'
import 'linq-extensions'

export interface Item {
  id: number
  name: string
  birthDate: Date
  gender: string
}

export const mockItems: Item[] = [
  {
    id: 1,
    name: 'Teszt Elek',
    birthDate: new Date(1980, 1, 5),
    gender: 'Férfi'
  },
  {
    id: 2,
    name: 'Teszt Erika',
    birthDate: new Date(1994, 10, 23),
    gender: 'Nő'
  },
  {
    id: 3,
    name: 'Teszt Anna',
    birthDate: new Date(1987, 9, 2),
    gender: 'Nő'
  },
  {
    id: 4,
    name: 'Teszt Barnabás',
    birthDate: new Date(1967, 8, 21),
    gender: 'Férfi'
  },
  {
    id: 5,
    name: 'Teszt András',
    birthDate: new Date(1980, 1, 5),
    gender: 'Férfi'
  },
  {
    id: 6,
    name: 'Teszt Júlia',
    birthDate: new Date(1994, 10, 23),
    gender: 'Nő'
  },
  {
    id: 7,
    name: 'Teszt Krisztina',
    birthDate: new Date(1987, 9, 2),
    gender: 'Nő'
  },
  {
    id: 8,
    name: 'Teszt Péter',
    birthDate: new Date(1967, 8, 21),
    gender: 'Férfi'
  }
]

export function mockDataAccess(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  return Promise.resolve({
    pageSize,
    pageIndex,
    totalCount: mockItems.length,
    items: mockItems
      .skip(pageIndex * pageSize)
      .take(pageSize)
      .toArray()
  })
}
