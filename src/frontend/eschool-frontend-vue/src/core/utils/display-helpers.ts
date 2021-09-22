export function dateToString(date: any): string {
  const _date = new Date(date)
  if (isNaN(_date.getTime())) {
    return ''
  }

  return _date.toLocaleDateString('hu-HU')
}
