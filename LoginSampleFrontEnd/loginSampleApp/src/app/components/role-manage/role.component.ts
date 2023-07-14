import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RoleModel } from 'src/app/models/roleModel';
import { RoleService } from 'src/app/services/role.service';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})
export class RoleComponent {
  
  roleAddForm:FormGroup;
  roles:RoleModel[];
  constructor(private formBuilder:FormBuilder, private roleService:RoleService) { }

  ngOnInit(): void {
    this.createRoleAddForm();
    this.getRoles();
  }

  createRoleAddForm(){
    this.roleAddForm = this.formBuilder.group({
      name:["",Validators.required]
    });
  }

  getRoles(){
    this.roleService.getAllRoles().subscribe(response=>{
      this.roles = response.data;
    });
  }

  createRole(){
    if (this.roleAddForm.valid) {
      let roleModel = Object.assign({},this.roleAddForm.value);
      this.roleService.createRoles(roleModel).subscribe(response=>{
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

  deleteRole(colorId:number){
    this.roleService.deleteRoles(colorId).subscribe((response)=>{
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
