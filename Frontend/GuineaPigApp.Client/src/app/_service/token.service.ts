import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  email: string = "";

  constructor() { }
  
  getEmailFromToken(): string {

    let token: any = localStorage.getItem('token');
  
      let decodedToken: any = jwtDecode(token);

        this.email = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
        return this.email;
  }
  getGuineaPigIdFromToken(): number{
    let token: any = localStorage.getItem('token');
  
    let decodedToken: any = jwtDecode(token);

    let guineaPigId: number;

    guineaPigId = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/guineaPigId']
    return guineaPigId;
  }
}
