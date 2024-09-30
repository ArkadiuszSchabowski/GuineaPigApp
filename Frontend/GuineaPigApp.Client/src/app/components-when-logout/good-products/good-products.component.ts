import { Component, OnInit } from '@angular/core';
import { PaginationDto } from '../../_models/pagination-dto';
import { PageEvent } from '@angular/material/paginator';
import { ProductDto } from '../../_models/product-dto';
import { GuineaPigService } from 'src/app/_service/guinea-pig.service';
import { BaseComponent } from 'src/app/_shared/base.component';
import { ThemeHelper } from 'src/app/_service/themeHelper.service';

@Component({
  selector: 'app-good-products',
  templateUrl: './good-products.component.html',
  styleUrls: ['./good-products.component.scss']
})
export class GoodProductsComponent extends BaseComponent implements OnInit{

  backgroundUrl: string = "assets/images/backgrounds/no-login/goodProducts.jpg"
  override cloudText: string = "Co za pyszności! Pamiętaj o porze karmienia!"

  products: ProductDto[] = [];
  counter: number | undefined= undefined;
  pagination: PaginationDto = new PaginationDto();

  constructor(guineaPigService: GuineaPigService, public themeHelper: ThemeHelper){
    super(guineaPigService);
  }


  override ngOnInit(): void {
    super.ngOnInit();
    this.getGoodProductsInformation();
    this.themeHelper.setTheme();
    this.themeHelper.setBackground(this.backgroundUrl);
  }

  getGoodProductsInformation(){

    this.guineaPigService.getGoodProducts(this.pagination).subscribe({
      next: response => {
        this.products = response.products;
        this.counter = response.counter;
      },
      error: () => {}
    })
  };
  changePage(event: PageEvent){
    
    this.pagination.PageNumber = event.pageIndex + 1;
    this.pagination.PageSize = event.pageSize;

    this.guineaPigService.getGoodProducts(this.pagination).subscribe({
      next: response => {
        this.products = response.products
        this.counter = response.counter
      },

      error: () => {}
    })
  }
}
