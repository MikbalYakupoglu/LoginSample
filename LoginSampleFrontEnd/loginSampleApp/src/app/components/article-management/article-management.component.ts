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

  categories:CategoryModel[];

  isCreateActive: boolean = false;
  isUpdateActive: boolean = false;


  articleCreateForm:FormGroup;
  selectedCategories:string[];

  constructor(private articleService:ArticleService, private categoryService:CategoryService,
    private formBuilder:FormBuilder, private router:Router){
    this.isCreateActive = false;
    this.isUpdateActive = false;
  }

  ngOnInit(): void {
    this.getUsersArticles();
    this.createArticleCreateForm();
    this.getCategories();
  }

  getUsersArticles(){
    this.articleService.getAllArticles().subscribe((res) => {
      this.usersArticles = res.data;
    })
  }

  routeCreate(){
    this.isCreateActive = true;;
  }

  createArticle(){
    const selectedCategories = this.getSelectedCategories();
    this.articleCreateForm.get('categories')?.setValue(selectedCategories);

    let articleCreateModel = Object.assign({},this.articleCreateForm.value);     
                
    
    this.articleService.createArticle(articleCreateModel).subscribe((res) => {
      alert(res.message);
      this.router.navigate(['/articles/manage']);
    },
    (errorRes) => { 
      alert(errorRes.error.message);
    })
  }

  createArticleCreateForm(){
    this.articleCreateForm = this.formBuilder.group({
      title:["",Validators.required],
      content:["",Validators.required],
      categories: new FormControl('')
    });
  }


  routeUpdate(article:ArticleModel){
    this.isUpdateActive = true;
    this.articleToUpdate = article;
  }

  activeDeletePanel(articleId:number){
    if(confirm('Makaleyi Silmek İstediğine Emin Misin ?')){
      this.articleService.deleteArticle(articleId).subscribe(res => {
        alert(res.message);
        window.location.reload();
      })
    }
  }

  getCategories(){
    this.categoryService.getAllCategories().subscribe(res => {
      this.categories = res.data;
    })
  }

  getSelectedCategories() {
    const checkedCategories: any[] = [];
    const checkboxes = document.querySelectorAll('input[type="checkbox"]:checked');        
    checkboxes.forEach(c => checkedCategories.push(c.id));
    return checkedCategories;
  }

}
