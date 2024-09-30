import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../_environments/environment_prod';
import { UserDto } from '../_models/user-dto';
import { UpdateUserDto } from '../_models/update-user-dto';

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
  updateProfileInformation(model: UpdateUserDto){
    return this.http.patch(this.baseUrl + "user/update", model)
  }
}
