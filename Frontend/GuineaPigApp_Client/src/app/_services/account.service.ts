import {
  HttpClient,
  HttpHeaders,
  HttpParams,
  HttpResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterUserDto } from '../models/add/register-user-dto';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { LoginUserDto } from '../models/add/login-user-dto';
import { ChangePasswordDto } from '../models/add/change-password-dto';
import { environment } from '../environments/environment';
import { RegisterStepOneDto } from '../models/add/register-step-one-dto';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;

  currentUserSource = new BehaviorSubject<string | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
  token: string | null = '';

  constructor(public http: HttpClient) {
    this.setUser();
  }
  setUser() {
    this.token = localStorage.getItem('token');
    if (this.token != null) {
      this.currentUserSource.next(this.token);
    }
  }

  register(registerUserDto: RegisterUserDto): Observable<RegisterUserDto> {
    return this.http.post<RegisterUserDto>(
      this.baseUrl + 'account/register',
      registerUserDto
    );
  }

  validateRegisterStepOne(
    dto: RegisterStepOneDto
  ): Observable<RegisterStepOneDto> {
    return this.http.post<RegisterStepOneDto>(
      this.baseUrl + 'account/register/step1/validate',
      dto
    );
  }

  checkEmail(email: string): Observable<HttpResponse<any>> {
    let params = new HttpParams().set('email', email);

    return this.http.get<any>(this.baseUrl + 'account/check-email', {
      params,
      observe: 'response',
    });
  }

  login(loginUserDto: LoginUserDto) {
    return this.http
      .post<any>(this.baseUrl + 'account/login', loginUserDto)
      .pipe(
        map((response) => {
          if (response.message) {
            this.token = response.message;
          }
          if (this.token) {
            localStorage.setItem('token', this.token);
            this.currentUserSource.next(this.token);
          }
        })
      );
  }
  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
  }
  changePassword(changePasswordDto: ChangePasswordDto) {
    const token = localStorage.getItem('token');

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.post<ChangePasswordDto>(
      this.baseUrl + 'account/change-password',
      changePasswordDto,
      { headers }
    );
  }
  removeProfile(email: string) {
    const token = localStorage.getItem('token');

    let params = new HttpParams().set('email', email);

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.delete(this.baseUrl + 'account', { params, headers });
  }
}
