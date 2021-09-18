export interface PagedListResponse<TItem = any> {
  pageIndex: number
  pageSize: number
  totalCount: number
  items: TItem[] | undefined
}
