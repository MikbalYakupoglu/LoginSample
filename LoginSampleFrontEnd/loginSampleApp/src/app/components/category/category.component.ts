import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CategoryModel } from 'src/app/models/categoryModel';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  categories:CategoryModel[];
  isCompleted:boolean;

  constructor(private categoryService:CategoryService, private router:Router) { }

  ngOnInit(): void {
    this.isCompleted = false;
    this.getCategories();
  }

  getCategories(){
    this.categoryService.getAllCategories().subscribe((res) => {
      this.categories = res.data;
      this.isCompleted = true;
    })
  }

  routeToCategorizedArticles(categoryName:string) {
      const url = '/articles/category'
      this.router.navigate([url, categoryName]);
    }





    
}