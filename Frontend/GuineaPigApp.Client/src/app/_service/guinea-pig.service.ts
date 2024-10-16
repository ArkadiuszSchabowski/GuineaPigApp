import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { GuineaPigDto } from '../_models/guinea-pig-dto';
import { RemoveGuineaPigDto } from '../_models/remove-guinea-pig-dto';
import { GuineaPigWeightsDto } from '../_models/guinea-pigs-weights-dto';
import { environment } from '../_environments/environment_dev';

@Injectable({
  providedIn: 'root',
})
export class GuineaPigService {
  token: string | null = "";
  textSubject = new BehaviorSubject<string>('');
  isTextSubject$ = this.textSubject.asObservable();

  constructor(private http: HttpClient) {}
  setCloudText(text: string) {
    this.textSubject.next(text);
  }

  getGuineaPig(email: string, name: string){
    let params = new HttpParams().set('email', email).set('name', name)

    return this.http.get(environment.apiUrl + "guineapig", { params });
  }

  getGuineaPigs(email: string) {
    let params = new HttpParams().set('email', email);

    this.token = localStorage.getItem('token');

    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.token}`);

    return this.http.get<GuineaPigDto[]>(
      environment.apiUrl + 'guineapig/guinea-pigs',
      { params, headers }
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

    this.token = localStorage.getItem('token');

    console.log(this.token);

    const headers = new HttpHeaders().set('Authorization', `Bearer ${this.token}`);

    return this.http.post(environment.apiUrl + 'guineapig', guineaPigDto, {params, headers},
    );
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
