export interface IPaginatedItems<T> {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: T[];
}
