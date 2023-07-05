import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{

  registerForm : FormGroup;

  constructor(private formBuilder:FormBuilder, private authService:AuthService,
    private router:Router) {}

  ngOnInit(): void {
    this.createRegisterForm();
  }

  createRegisterForm(){
    this.registerForm = this.formBuilder.group({
      email:["",Validators.required],
      password:["",Validators.required],
      phone:["",Validators.required]
    })
  }

  register(){
    let registerModel = Object.assign({}, this.registerForm.value);

    if(this.registerForm.valid){
      this.authService.register(registerModel).subscribe((res) => {
        alert(res.message);
        this.authService.writeTokenToCookie(res.data.token, res.data.expirationDate);
        this.router.navigate(['']);
      },
      (errorRes) => {
        alert(errorRes.error.message);
      })
    }
    else{
      alert("Form boş bırakılamaz.")
    }
    
  }


}
