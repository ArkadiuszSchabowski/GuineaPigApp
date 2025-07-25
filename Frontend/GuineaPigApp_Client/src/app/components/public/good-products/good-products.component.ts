import { Component, OnInit } from '@angular/core';
import { PaginationDto } from '../../../models/pagination-dto';
import { PageEvent } from '@angular/material/paginator';
import { ProductDto } from '../../../models/product-dto';
import { GuineaPigService } from 'src/app/_services/guinea-pig.service';
import { BaseComponent } from 'src/app/shared/base.component';
import { ThemeHelper } from 'src/app/_services/theme-helper.service';
import { ProductService } from 'src/app/_services/product.service';

@Component({
  selector: 'app-good-products',
  templateUrl: './good-products.component.html',
  styleUrls: ['./good-products.component.scss']
})
export class GoodProductsComponent extends BaseComponent implements OnInit{

  backgroundUrl: string = "assets/images/backgrounds/no-login/goodProducts.jpg"
  override cloudText: string = "Co za pyszności! Pamiętaj o porze karmienia!"

  products: ProductDto[] = [];
  totalCount: number | undefined= undefined;
  pagination: PaginationDto = new PaginationDto();

  constructor(guineaPigService: GuineaPigService, public themeHelper: ThemeHelper, public productService: ProductService){
    super(guineaPigService);
  }


  override ngOnInit(): void {
    super.ngOnInit();
    this.getGoodProducts();
    this.themeHelper.setTheme();
    this.themeHelper.setBackground(this.backgroundUrl);
  }

  getGoodProducts(){

    this.productService.getGoodProducts(this.pagination).subscribe({
      next: response => {
        this.products = response.products;
        this.totalCount = response.totalCount;
      },
      error: () => {}
    })
  };
  changePage(event: PageEvent){
    
    this.pagination.PageNumber = event.pageIndex + 1;
    this.pagination.PageSize = event.pageSize;

    this.productService.getGoodProducts(this.pagination).subscribe({
      next: response => {
        this.products = response.products
        this.totalCount = response.totalCount
      },

      error: () => {}
    })
  }
}
