import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CategoryModel } from 'src/app/models/categoryModel';
import { ArticleService } from 'src/app/services/article.service';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-article-add',
  templateUrl: './article-add.component.html',
  styleUrls: ['./article-add.component.css']
})
export class ArticleAddComponent implements OnInit{

  categories:CategoryModel[];
  articleCreateForm:FormGroup;
  selectedCategories:string[];

  constructor(private articleService:ArticleService, private categoryService:CategoryService,
    private formBuilder:FormBuilder, private router:Router) {}


  ngOnInit(): void {
    this.createArticleCreateForm();
    this.getCategories();
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
