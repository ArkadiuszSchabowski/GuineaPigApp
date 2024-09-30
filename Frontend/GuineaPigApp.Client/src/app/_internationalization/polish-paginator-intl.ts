import { MatPaginatorIntl } from '@angular/material/paginator';

export class PolishPaginatorIntl extends MatPaginatorIntl {

  override itemsPerPageLabel: string = "Ilość pozycji na stronę:";
  override nextPageLabel: string = "Następna strona";
  override previousPageLabel: string = "Poprzednia strona";

  override getRangeLabel = (page: number, pageSize: number, length: number) => {
    if (length === 0 || pageSize === 0) {
      return `0 z ${length}`;
    }
    const startIndex = page * pageSize;
    const endIndex = startIndex < length ?
        Math.min(startIndex + pageSize, length) :
        startIndex + pageSize;
    return `${startIndex + 1} - ${endIndex} z ${length}`;
  };
}
