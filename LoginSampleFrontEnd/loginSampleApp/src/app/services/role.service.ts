import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SingleResponseModel } from '../models/singleResponseModel';
import { Observable } from 'rxjs';
import { ListResponseModel } from '../models/listResponseModel';
import { RoleModel } from '../models/roleModel';
import { ResponseModel } from '../models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private httpClient:HttpClient) { }

  apiUrl = 'https://localhost:7142/roles/';

  getAllRoles() : Observable<ListResponseModel<RoleModel>>{
    let newPath = this.apiUrl + 'getall';
    return this.httpClient.get<ListResponseModel<RoleModel>>(newPath);
  }

  createRoles(roleId:RoleModel) : Observable<ResponseModel>{
    let newPath = this.apiUrl + 'create';
    return this.httpClient.post<ResponseModel>(newPath,roleId);
  }

  // updateRoles(roleId:number, newRoles:RoleModel) : Observable<ResponseModel>{
  //   let newPath = this.apiUrl + 'update?roleId=' + roleId;
  //   return this.httpClient.patch<ResponseModel>(newPath, newRoles);
  // }

  deleteRoles(roleId:number) : Observable<ResponseModel>{
    let newPath = this.apiUrl + 'delete?roleId=' + roleId;
    return this.httpClient.delete<ResponseModel>(newPath);
  }
}
