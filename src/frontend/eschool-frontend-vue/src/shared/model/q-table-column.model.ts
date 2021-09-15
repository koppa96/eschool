export interface QTableColumn<TItem = any> {
  /**
   * Unique id, identifies column, (used by pagination.sortBy, 'body-cell-[name]' slot, ...)
   */
  name: string
  /**
   * Label for header
   */
  label: string
  /**
   * Row Object property to determine value for this column or function which maps to the required property
   */
  field: string | ((item: TItem) => any)
  /**
   * If we use visible-columns, this col will always be visible
   */
  required?: boolean
  /**
   * Horizontal alignment of cells in this column
   */
  align?: string
  /**
   * Tell QTable you want this column sortable
   */
  sortable?: boolean
  /**
   * Compare function if you have some custom data or want a specific way to compare two rows
   */
  sort?: (left: TItem, right: TItem) => number
  /**
   * Set column sort order: 'ad' (ascending-descending) or 'da' (descending-ascending); Overrides the 'column-sort-order' prop
   */
  sortOrder?: 'ad' | 'da'
  /**
   * Function you can apply to format your data
   */
  format?: (item: TItem) => any
  /**
   * Style to apply on normal cells of the column
   */
  style?: string | Function
  /**
   * Classes to add on normal cells of the column
   */
  classes?: string | Function
  /**
   * Style to apply on header cells of the column
   */
  headerStyle?: string
  /**
   * Classes to add on header cells of the column
   */
  headerClasses?: string
}
