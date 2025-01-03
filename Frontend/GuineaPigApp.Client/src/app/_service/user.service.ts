import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserDto } from '../_models/user-dto';
import { UpdateUserDto } from '../_models/update-user-dto';
import { environment } from '../_environments/environment';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl: string = environment.apiUrl;
  
  constructor(private http: HttpClient) { 

  }
  
  getUserInformation(email: string){

    let params = new HttpParams().set("email", email)

    return this.http.get<UserDto>(this.baseUrl + "user", {params})
  }
  updateProfileInformation(email: string, model: UpdateUserDto){

    let params = new HttpParams().set("email", email)

    return this.http.patch(this.baseUrl + "user", model, {params})
  }
}
