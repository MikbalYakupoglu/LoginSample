import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CategoryModel } from '../models/categoryModel';
import { SingleResponseModel } from '../models/singleResponseModel';
import { Observable } from 'rxjs';
import { ListResponseModel } from '../models/listResponseModel';
import { ResponseModel } from '../models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private httpClient:HttpClient) { }

  apiUrl = 'https://localhost:7142/categories/';

  getCategory() : Observable<SingleResponseModel<CategoryModel>>{
    let newPath = this.apiUrl + 'get';
    return this.httpClient.get<SingleResponseModel<CategoryModel>>(newPath);
  }

  getAllCategories() : Observable<ListResponseModel<CategoryModel>>{
    let newPath = this.apiUrl + 'getall';
    return this.httpClient.get<ListResponseModel<CategoryModel>>(newPath);
  }

  createCategory(category:CategoryModel) : Observable<ResponseModel>{
    let newPath = this.apiUrl + 'create';
    return this.httpClient.post<ResponseModel>(newPath,category);
  }

  deleteCategory(categoryId:number) : Observable<ResponseModel>{
    let newPath = this.apiUrl + 'delete?CategoryId=' + categoryId;
    return this.httpClient.delete<ResponseModel>(newPath);
  }

  // updateCategory(categoryId:number, newCategory:CategoryModel) : Observable<ResponseModel>{
  //   let newPath = this.apiUrl + 'update?CategoryId=' + categoryId;
  //   return this.httpClient.patch<ResponseModel>(newPath, newCategory);
  // }
}
