export class PaginationBase {
  skip: number;
  take: number;
  searchQuery: string;
  orderBy: string;
  constructor() {
    this.skip = 0;
    this.take = 10;
    this.searchQuery = '';
    this.orderBy = '';
  }
}
