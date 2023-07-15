import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ArticleModel } from 'src/app/models/articleModel';
import { CategoryModel } from 'src/app/models/categoryModel';
import { ArticleService } from 'src/app/services/article.service';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-article-management',
  templateUrl: './article-management.component.html',
  styleUrls: ['./article-management.component.css']
})
export class ArticleManagementComponent implements OnInit{

  isWriterHasAnyArticle:boolean = true;
  usersArticles:ArticleModel[];
  articleToUpdate: ArticleModel;



  constructor(private articleService:ArticleService, private categoryService:CategoryService,
    private formBuilder:FormBuilder, private router:Router){ }

  ngOnInit(): void {
    this.getUsersArticles();
  }

  getUsersArticles(){
    this.articleService.getAllArticles().subscribe((res) => {
      this.usersArticles = res.data; // seçilen userin articleleri alınacak
    })
  }

  routeCreate(){
    this.router.navigate(['/articles/manage/create'])
  }

  routeUpdate(articleId:number){;
    this.router.navigate(['/articles/manage/update', articleId])
  }

  activeDeletePanel(articleId:number){
    if(confirm('Makaleyi Silmek İstediğine Emin Misin ?')){
      this.articleService.deleteArticle(articleId).subscribe(res => {
        alert(res.message);
        window.location.reload();
      })
    }
  }



}
