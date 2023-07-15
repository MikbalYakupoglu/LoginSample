import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { delay, timeout } from 'rxjs';
import { ArticleModel } from 'src/app/models/articleModel';
import { CategoryModel } from 'src/app/models/categoryModel';
import { ArticleService } from 'src/app/services/article.service';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-article-update',
  templateUrl: './article-update.component.html',
  styleUrls: ['./article-update.component.css']
})
export class ArticleUpdateComponent implements OnInit{

  categories:CategoryModel[];
  articleUpdateForm:FormGroup;
  selectedCategories:string[];

  articleId:number;
  articleToUpdate:ArticleModel;

  constructor(private articleService:ArticleService, private categoryService:CategoryService,
    private formBuilder:FormBuilder, private router:Router, private activatedRoute:ActivatedRoute) {}

    ngOnInit():void {
      this.getSelectedArticle();
      this.getCategories();
  
      setTimeout(() => {
        this.createArticleUpdateForm();
      }, 200);   

      setTimeout(() => {
        this.setArticleCategoriesToCheckbox();
      }, 250);   

    }

   getSelectedArticle(){
      this.activatedRoute.params.subscribe(params => {        
        this.articleService.getArticle(params['articleId']).subscribe(res => {          
          this.articleToUpdate = res.data;                    
        })
      })
    }

    updateArticle(){
      const selectedCategories = this.getSelectedCategories();
      this.articleUpdateForm.get('categories')?.setValue(selectedCategories);  
      let articleUpdateModel = Object.assign({},this.articleUpdateForm.value);     
                  
      this.articleService.updateArticle(this.articleToUpdate.id ,articleUpdateModel).subscribe(res => {
        alert(res.message);
        window.location.reload();
      },
      errorRes => {
        alert(errorRes.message)
      })
    }
  
    createArticleUpdateForm(){
      this.articleUpdateForm = this.formBuilder.group({
        title:[this.articleToUpdate.title,Validators.required],
        content:[this.articleToUpdate.content,Validators.required],
        categories:[this.articleToUpdate.categories]
      });
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

    setArticleCategoriesToCheckbox(){
      const articleCategories = this.articleToUpdate.categories;
      const checkboxes = document.querySelectorAll('input[type="checkbox"]') as NodeListOf<HTMLInputElement>;         
      checkboxes.forEach((checkbox) => {
        if (articleCategories.includes(checkbox.id)) {
          checkbox.checked = true;
        }
      });
    }


}
