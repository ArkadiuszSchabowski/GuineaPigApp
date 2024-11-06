import { HttpClient, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterUserDto } from '../_models/register-user-dto';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { LoginUserDto } from '../_models/login-user-dto';
import { ChangePasswordDto } from '../_models/change-password-dto';
import { environment } from '../_environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AccountService {

  baseUrl = environment.apiUrl;

  currentUserSource = new BehaviorSubject<string | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
  token: string | null= "";

  constructor(public http: HttpClient) {
    this.setUser();
  }
  setUser() {
    this.token = localStorage.getItem('token');
    if(this.token != null){
      this.currentUserSource.next(this.token);
    }
  }

  registerUser(registerUserDto: RegisterUserDto): Observable<RegisterUserDto> {
    return this.http.post<RegisterUserDto>(this.baseUrl + 'account/register',registerUserDto);
  }
  
  checkEmail(email: string): Observable<HttpResponse<any>> {
  
    let params = new HttpParams().set('email', email);

    return this.http.get<any>(this.baseUrl + "account/check-email", { params, observe: 'response' });
  }
  
  login(loginUserDto: LoginUserDto){
    return this.http.post<any>(this.baseUrl + 'account/login', loginUserDto).pipe(
      map((response) => {
        if(response.message){
          this.token = response.message;
        }
        if (this.token) {
          localStorage.setItem('token', this.token);
          this.currentUserSource.next(this.token);
        }
      })
    )
  }
  logout(){
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
  }
  changePassword(changePasswordDto: ChangePasswordDto){

    const token = localStorage.getItem('token');

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.post<ChangePasswordDto>(this.baseUrl + "account/change-password", changePasswordDto, {headers})
  }
  removeProfile(email: string){

    const token = localStorage.getItem('token');
  
    let params = new HttpParams().set('email', email);
  
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
  
    return this.http.delete(this.baseUrl + "account", { params, headers });
  }
}
