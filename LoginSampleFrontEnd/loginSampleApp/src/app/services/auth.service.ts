import { Injectable } from '@angular/core';
import { LoginModel } from '../models/loginModel';
import { Observable } from 'rxjs';
import { TokenModel } from '../models/tokenModel';
import { RegisterModel } from '../models/registerModel';
import { HttpClient, HttpResponse, HttpStatusCode } from '@angular/common/http'
import { ResultModel } from '../models/resultModel';
import { CookieService } from 'ngx-cookie-service';
import { DatePipe } from '@angular/common';
import { SingleResultModel } from '../models/singleResultModel';
import { Token } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpClient:HttpClient, private cookieService: CookieService) { }

  apiUrl = 'https://localhost:7142/auth/';

  login(loginModel:LoginModel) : Observable<SingleResultModel<TokenModel>>{
    let newPath = this.apiUrl + 'login';
    return this.httpClient.post<SingleResultModel<TokenModel>>(newPath, loginModel);
  }

  register(registerModel: RegisterModel) : Observable<SingleResultModel<TokenModel>>{
    let newPath = this.apiUrl + 'register';
    return this.httpClient.post<SingleResultModel<TokenModel>>(newPath, registerModel);
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

    deleteTokenIfExpired(){
      
    }


}