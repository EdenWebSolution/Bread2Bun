export class PaginationModel<T>  {
  totalRecords: number;
  details: Array<T>;

  constructor() {
    this.details = new Array<T>();
    this.totalRecords = 0;
  }
}
