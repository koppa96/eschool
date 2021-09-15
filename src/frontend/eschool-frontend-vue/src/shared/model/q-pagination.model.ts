export interface QPagination {
  /**
   * Column name (from column definition)
   */
  sortBy?: string
  /**
   * Is sorting in descending order?
   */
  descending?: boolean
  /**
   * Page number (1-based)
   */
  page: number
  /**
   * How many rows per page? 0 means Infinite
   */
  rowsPerPage: number
  /**
   * For server-side fetching only. How many total database rows are there to be added to the table. If set, causes the QTable to emit @request when data is required.
   */
  rowsNumber?: number
}
