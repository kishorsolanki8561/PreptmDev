import { PAGE_SIZE } from "../fixed-values";

export class Index {
    page = 1;
    pageSize = PAGE_SIZE;
    orderBy = '';
    orderByAsc:  number;
    constructor() {
        this.orderByAsc = 0;
    }
}
