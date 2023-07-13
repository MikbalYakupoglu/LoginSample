import { Component, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CategoryModel } from 'src/app/models/categoryModel';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-category-add',
  templateUrl: './category-add.component.html',
  styleUrls: ['./category-add.component.css']
})
export class CategoryAddComponent {

  categoryAddForm:FormGroup;
  categories:CategoryModel[];
  constructor(private formBuilder:FormBuilder, private categoryService:CategoryService) { }

  ngOnInit(): void {
    this.createCategoryAddForm();
    this.getCategories();
  }

  createCategoryAddForm(){
    this.categoryAddForm = this.formBuilder.group({
      name:["",Validators.required]
    });
  }

  getCategories(){
    this.categoryService.getAllCategories().subscribe(response=>{
      this.categories = response.data;
    });
  }

  createCategory(){
    if (this.categoryAddForm.valid) {
      let categoryModel = Object.assign({},this.categoryAddForm.value);
      this.categoryService.createCategory(categoryModel).subscribe(response=>{
          alert(response.message)
          setTimeout(() => {
            window.location.reload();        
          }, 500);      
      },
      errorResponse=>{
        if(errorResponse.error.ValidationErrors){
          errorResponse.error.ValidationErrors.forEach((error:any) => {
            alert(error.ErrorMessage);
          })
        }
        else if(errorResponse.error.message){ // from responseModel
          console.warn("a");
          
          alert(errorResponse.error.message);
        }
        else{ // from Exception
          alert(errorResponse.error.Message);
        }
      })
    }

    else{
      alert("Form Eksik Bırakılamaz.");
    }
  }

  deleteCategory(colorId:number){
    this.categoryService.deleteCategory(colorId).subscribe((response)=>{
      alert(response.message);
      setTimeout(() => {
        window.location.reload();        
      }, 500);
    },
    errorResponse => {
      if (errorResponse.error.message) {
        alert(errorResponse.error.message);
      }
      else if (errorResponse.error.Message) {
        alert(errorResponse.error.Message);
        }
    });

  }

}
