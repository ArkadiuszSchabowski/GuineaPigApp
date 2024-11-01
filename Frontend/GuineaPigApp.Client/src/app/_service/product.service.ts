import { Injectable } from '@angular/core';
import { PaginationDto } from '../_models/pagination-dto';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductResult } from '../_models/product-result';
import { environment } from '../_environments/environment_prod';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }

  getBadProducts(paginationDto: PaginationDto): Observable<ProductResult> {
    let params = new HttpParams()
      .set('PageNumber', paginationDto.PageNumber.toString())
      .set('PageSize', paginationDto.PageSize.toString());
    return this.http.get<ProductResult>(
      environment.apiUrl + 'product/bad',
      { params }
    );
  }

  getGoodProducts(paginationDto: PaginationDto): Observable<ProductResult> {
    let params = new HttpParams()
      .set('PageNumber', paginationDto.PageNumber.toString())
      .set('PageSize', paginationDto.PageSize.toString());
    return this.http.get<ProductResult>(
      environment.apiUrl + 'product/good',
      { params }
    );
  }
}
