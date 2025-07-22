import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { GuineaPigDto } from '../models/guinea-pig-dto';
import { RemoveGuineaPigDto } from '../models/remove-guinea-pig-dto';
import { GuineaPigWeightsDto } from '../models/guinea-pigs-weights-dto';
import { environment } from '../environments/environment';

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

  getGuineaPig(email: string, name: string){
    let params = new HttpParams().set('email', email).set('name', name)

    return this.http.get(environment.apiUrl + "guineapig", { params });
  }

  getGuineaPigs(email: string) {
    let params = new HttpParams().set('email', email);

    let token: string | null = "";

    token = localStorage.getItem('token');

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.get<GuineaPigDto[]>(
      environment.apiUrl + 'guineapig',
      { params, headers }
    );
  }
  getGuineaPigWeights(email: string, name: string){
    let params = new HttpParams().set('email', email)

    let token: string | null = "";

    token = localStorage.getItem('token');

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.get<GuineaPigWeightsDto[]>(
      environment.apiUrl + `guineapig/weights/${name}`,
      { params, headers }
    );
  }

  addGuineaPig(email: string, guineaPigDto: GuineaPigDto) {
    let params = new HttpParams().set('email', email);

    let token: string | null = "";

    token = localStorage.getItem('token');

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.post(environment.apiUrl + 'guineapig', guineaPigDto, {params, headers},
    );
  }
  updateWeight(email: string, guineaPig: GuineaPigDto) {

    let params = new HttpParams().set('email', email);

    let token: string | null = "";

    token = localStorage.getItem('token');

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.patch(
      environment.apiUrl + `guineapig/update-weight/${guineaPig.name}`,
      guineaPig,
      { params, headers }
    );
  }

  removeGuineaPig(guineaPig: RemoveGuineaPigDto) {

      let token: string | null = "";

      token = localStorage.getItem('token');
  
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

      let params = new HttpParams()
      .set('email', guineaPig.email);
  
    return this.http.delete(`${environment.apiUrl}guineapig/${guineaPig.name}`, {
      params, headers
    });
  }
}  