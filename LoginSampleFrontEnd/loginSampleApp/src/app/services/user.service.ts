import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserModel } from '../models/userModel';
import { Observable } from 'rxjs';
import { SingleResponseModel } from '../models/singleResponseModel';
import { ResponseModel } from '../models/responseModel';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient:HttpClient, private cookieService: CookieService) { }

  apiUrl = 'https://localhost:7142/users/';

  getLoginedUser() : Observable<SingleResponseModel<UserModel>>{
    let newPath = this.apiUrl + 'getlogineduser';
    return this.httpClient.get<SingleResponseModel<UserModel>>(newPath);
  }
  

  getUser(userId: number) : Observable<SingleResponseModel<UserModel>>{
    let newPath = this.apiUrl + 'get?userId='+userId;
    return this.httpClient.get<SingleResponseModel<UserModel>>(newPath);
  }

  getAllUsers() : Observable<SingleResponseModel<UserModel>>{
    let newPath = this.apiUrl + 'getall';
    return this.httpClient.get<SingleResponseModel<UserModel>>(newPath);
  }

  deleteUser(userId:number) : Observable<ResponseModel> {
    let newPath =  this.apiUrl + 'delete?deleteId=' + userId;    
    return this.httpClient.delete<ResponseModel>(newPath);
  }


  addRoleToUser(userId:number, roleIds:number[]) : Observable<SingleResponseModel<UserModel>>{
    const formData = this.addRolesIntoForm(roleIds);

    let newPath = this.apiUrl + 'addrole?userId='+userId;
    return this.httpClient.post<SingleResponseModel<UserModel>>(newPath, formData);
  }

  removeRoleFromUser(userId:number, roleIds:number[]) : Observable<SingleResponseModel<UserModel>>{
    const formData = this.addRolesIntoForm(roleIds);

    let newPath = this.apiUrl + 'removerole?userId='+userId;
    return this.httpClient.post<SingleResponseModel<UserModel>>(newPath,formData);
  }


  logout(){
    this.cookieService.delete('token');
  }

  private addRolesIntoForm(userIds:number[]): FormData{
    let formData = new FormData();
    for (let index = 0; index < userIds.length; index++) {
      formData.append("roleIds", userIds[index].toString());
    }

    return formData;
  }
}
