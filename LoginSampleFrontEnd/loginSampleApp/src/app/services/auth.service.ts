import { Injectable } from '@angular/core';
import { LoginModel } from '../models/loginModel';
import { Observable } from 'rxjs';
import { TokenModel } from '../models/tokenModel';
import { RegisterModel } from '../models/registerModel';
import { HttpClient } from '@angular/common/http'
import { ResultModel } from '../models/resultModel';
import { CookieService } from 'ngx-cookie-service';
import { DatePipe } from '@angular/common';
import { SingleResultModel } from '../models/singleResultModel';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpClient:HttpClient, private cookieService: CookieService) { }

  apiUrl = 'https://localhost:7142/user/';

  login(loginModel:LoginModel) : Observable<SingleResultModel<TokenModel>>{
    let newPath = this.apiUrl + 'login';
    return this.httpClient.post<SingleResultModel<TokenModel>>(newPath, loginModel);
  }

  register(registerModel: RegisterModel) : Observable<ResultModel>{
    let newPath = this.apiUrl + 'register';
    return this.httpClient.post<ResultModel>(newPath, registerModel);
  }
  
  isAuthenticated(){
    if (this.cookieService.check("token")) {
      return true;
    }
    else{
      return false;
    }
  }
}
