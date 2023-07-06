import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserModel } from '../models/userModel';
import { Observable } from 'rxjs';
import { SingleResultModel } from '../models/singleResultModel';
import { ResultModel } from '../models/resultModel';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient:HttpClient, private cookieService: CookieService) { }

  apiUrl = 'https://localhost:7142/users/';

  getUser() : Observable<SingleResultModel<UserModel>>{
    let newPath = this.apiUrl + 'get';
    return this.httpClient.get<SingleResultModel<UserModel>>(newPath);
  }

  delete(userModel:UserModel) : Observable<ResultModel> {
    let newPath =  this.apiUrl + 'delete';    
    return this.httpClient.post<ResultModel>(newPath, userModel);
  }

  logout(){
    this.cookieService.delete('token');
  }
}
