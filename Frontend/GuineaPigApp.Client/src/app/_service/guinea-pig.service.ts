import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GuineaPigInformationDto } from '../_models/guinea-pig-information-dto';
import { BehaviorSubject, Observable } from 'rxjs';
import { PaginationDto } from '../_models/pagination-dto';
import { ProductResult } from '../_models/product-result';
import { GuineaPigDto } from '../_models/guinea-pig-dto';
import { RemoveGuineaPigDto } from '../_models/remove-guinea-pig-dto';
import { GuineaPigWeightsDto } from '../_models/guinea-pigs-weights-dto';
import { environment } from '../_environments/environment_prod';

@Injectable({
  providedIn: 'root',
})
export class GuineaPigService {
  textSubject = new BehaviorSubject<string>('');
  isTextSubject$ = this.textSubject.asObservable();

  constructor(private http: HttpClient) {}
  setCloudText(text: string) {
    this.textSubject.next(text);
  }

  getInformationGuineaPigs(): Observable<GuineaPigInformationDto> {
    return this.http.get<GuineaPigInformationDto>(
      environment.apiUrl + 'guineapig/info'
    );
  }

  getBadProducts(paginationDto: PaginationDto): Observable<ProductResult> {
    let params = new HttpParams()
      .set('PageNumber', paginationDto.PageNumber.toString())
      .set('PageSize', paginationDto.PageSize.toString());
    return this.http.get<ProductResult>(
      environment.apiUrl + 'guineapig/bad-products',
      { params }
    );
  }

  getGoodProducts(paginationDto: PaginationDto): Observable<ProductResult> {
    let params = new HttpParams()
      .set('PageNumber', paginationDto.PageNumber.toString())
      .set('PageSize', paginationDto.PageSize.toString());
    return this.http.get<ProductResult>(
      environment.apiUrl + 'guineapig/good-products',
      { params }
    );
  }

  getGuineaPig(email: string, name: string){
    let params = new HttpParams().set('email', email).set('name', name)

    return this.http.get(environment.apiUrl + "guineapig", { params });
  }

  getGuineaPigs(email: string) {
    let params = new HttpParams().set('email', email);

    return this.http.get<GuineaPigDto[]>(
      environment.apiUrl + 'guineapig/guinea-pigs',
      { params }
    );
  }
  getGuineaPigWeights(email: string, name: string){
    let params = new HttpParams().set('email', email).set('name', name)

    return this.http.get<GuineaPigWeightsDto[]>(
      environment.apiUrl + 'guineapig/weights',
      { params }
    );
  }

  addGuineaPig(email: string, guineaPigDto: GuineaPigDto) {
    let params = new HttpParams().set('email', email);

    return this.http.post(environment.apiUrl + 'guineapig', guineaPigDto, {
      params,
    });
  }
  updateWeight(email: string, guineaPig: GuineaPigDto) {

    let params = new HttpParams().set('email', email);

    return this.http.post(
      environment.apiUrl + 'guineapig/update-weight',
      guineaPig,
      { params }
    );
  }

  removeGuineaPig(guineaPig: RemoveGuineaPigDto) {
    let params = new HttpParams()
      .set('email', guineaPig.email)
      .set('name', guineaPig.name);

      console.log(params + "params");

    return this.http.delete(environment.apiUrl + 'guineapig', {
      params,
    });
  }
}
