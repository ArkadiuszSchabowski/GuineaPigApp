import { Component, OnInit } from '@angular/core';
import { GuineaPigService } from '../../../_services/guinea-pig.service';
import { PaginationDto } from '../../../models/pagination-dto';
import { PageEvent } from '@angular/material/paginator';
import { ProductDto } from '../../../models/product-dto';
import { ProductResult } from '../../../models/product-result';
import { BaseComponent } from 'src/app/shared/base.component';
import { ThemeHelper } from 'src/app/_services/theme-helper.service';
import { ProductService } from 'src/app/_services/product.service';

@Component({
  selector: 'app-bad-products',
  templateUrl: './bad-products.component.html',
  styleUrls: ['./bad-products.component.scss']
})
export class BadProductsComponent extends BaseComponent implements OnInit{

  backgroundUrl: string = "assets/images/backgrounds/no-login/badProducts.jpg"
  override cloudText: string = "Proszę pamiętaj, by nigdy nie dawać mi tych produktów! Niektóre z nich są nawet śmiertelnie szkodliwe!"

  products: ProductDto[] = [];
  totalCount: number | undefined = undefined;
  pagination: PaginationDto = new PaginationDto();

  constructor(guineaPigService: GuineaPigService, public themeHelper: ThemeHelper, public productService: ProductService){
    super(guineaPigService);
  }
  override ngOnInit(): void {
    super.ngOnInit();
    this.getBadProducts(this.pagination);
    this.themeHelper.setTheme();
    this.themeHelper.setBackground(this.backgroundUrl);
  }

  getBadProducts(paginationDto: PaginationDto){

    this.productService.getBadProducts(paginationDto).subscribe({
      next: (response: ProductResult) => {
        this.products = response.products;
        this.totalCount = response.totalCount;
      },
      error: () => {}
    })
  };

  changePage(event: PageEvent){
  
    this.pagination.PageSize = event.pageSize;
    this.pagination.PageNumber = event.pageIndex + 1;

    this.productService.getBadProducts(this.pagination).subscribe({
      next: response => this.products = response.products,
      error: () => {}
    })
  }
}
