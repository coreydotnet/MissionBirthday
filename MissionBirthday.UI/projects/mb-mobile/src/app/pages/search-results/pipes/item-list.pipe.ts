import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'itemList'
})
export class ItemListPipe implements PipeTransform {

  transform(value: string[]): string {
    return value.join(',');
  }

}
