
export interface IPaginatedItems<T> {
    pageindex: number;
    pageSize: number;
    count: number;
    data: T[];
}