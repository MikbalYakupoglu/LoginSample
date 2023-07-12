import { Injectable } from '@angular/core';
import { LoginModel } from '../models/loginModel';
import { Observable } from 'rxjs';
import { TokenModel } from '../models/tokenModel';
import { RegisterModel } from '../models/registerModel';
import { HttpClient, HttpStatusCode } from '@angular/common/http'
import { CookieService } from 'ngx-cookie-service';
import { SingleResponseModel } from '../models/singleResponseModel';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
})
export class AuthService {

  constructor(private httpClient:HttpClient, private cookieService: CookieService,
    private jwtHelper:JwtHelperService) { }

  apiUrl = 'https://localhost:7142/auth/';

  login(loginModel:LoginModel) : Observable<SingleResponseModel<TokenModel>>{
    let newPath = this.apiUrl + 'login';
    return this.httpClient.post<SingleResponseModel<TokenModel>>(newPath, loginModel);
  }

  register(registerModel: RegisterModel) : Observable<SingleResponseModel<TokenModel>>{
    let newPath = this.apiUrl + 'register';
    return this.httpClient.post<SingleResponseModel<TokenModel>>(newPath, registerModel);
  }

  validateToken(){
    let newPath = this.apiUrl + 'verify';
    return this.httpClient.get(newPath, {observe: 'response'});
  } 



  writeTokenToCookie(token:string, expirationDate:Date){
    let expireDate = new Date(expirationDate);
    this.cookieService.set("token", token, expireDate);
  }

  isAuthenticated(): Promise<boolean>{
      return new Promise<boolean>((resolve, reject) => {
        this.validateToken().subscribe((res) => {
          if(res.status === HttpStatusCode.Ok){           
            resolve(true);
          }
          else{
            resolve(false);
          }
        },
        (errorRes) => {
          resolve(false);
        })
      });        
    }

    logout(){
      this.cookieService.delete('token');
    }

    // deleteTokenIfExpired(){
      
    // }

    getUserRoles():string[]{
      const token = this.cookieService.get('token');
      const decodedToken = this.jwtHelper.decodeToken(token);
      const roles = decodedToken['role'];

       return roles;
    }

}