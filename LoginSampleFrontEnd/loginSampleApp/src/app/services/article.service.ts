import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ArticleModel } from '../models/articleModel';
import { Observable } from 'rxjs';
import { SingleResponseModel } from '../models/singleResponseModel';
import { ListResponseModel } from '../models/listResponseModel';
import { ResponseModel } from '../models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {

  constructor(private httpClient:HttpClient) { }

  apiUrl = 'https://localhost:7142/articles/';

  getArticle(articleId:number) : Observable<SingleResponseModel<ArticleModel>>{
    let newPath = this.apiUrl + 'get?articleId='+articleId;
    return this.httpClient.get<SingleResponseModel<ArticleModel>>(newPath);
  }

  getAllArticles() : Observable<ListResponseModel<ArticleModel>>{
    let newPath = this.apiUrl + 'getall';
    return this.httpClient.get<ListResponseModel<ArticleModel>>(newPath);
  }

  getAllArticlesByCategory(categoryName:string) : Observable<ListResponseModel<ArticleModel>>{
    let newPath = this.apiUrl + 'getallbycategory?categoryName='+ categoryName;
    return this.httpClient.get<ListResponseModel<ArticleModel>>(newPath);
  }

  createArticle(article:ArticleModel) : Observable<ResponseModel>{
    let newPath = this.apiUrl + 'create';
    return this.httpClient.post<ResponseModel>(newPath,article);
  }

  deleteArticle(articleId:number) : Observable<ResponseModel>{
    let newPath = this.apiUrl + 'delete?articleId=' + articleId;
    return this.httpClient.delete<ResponseModel>(newPath);
  }

  updateArticle(articleId:number, newArticle:ArticleModel) : Observable<ResponseModel>{
    let newPath = this.apiUrl + 'update?articleId=' + articleId;
    return this.httpClient.patch<ResponseModel>(newPath, newArticle);
  }

}
