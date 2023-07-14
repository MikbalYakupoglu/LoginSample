import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl ,Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import {CookieService} from 'ngx-cookie-service';
import { sampleTime } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  loginForm : FormGroup;

  constructor(private formBuilder:FormBuilder, private authService:AuthService,
    private router: Router) { }

  ngOnInit(): void {
    this.createLoginForm();
  }

  createLoginForm(){
    this.loginForm = this.formBuilder.group({
      email:["",Validators.required],
      password:["",Validators.required]
    });
  }

  login(){
    let loginModel = Object.assign({},this.loginForm.value); 
    if(this.loginForm.valid){
      this.authService.login(loginModel).subscribe((res) => {
        this.authService.writeTokenToCookie(res.data.token,res.data.expirationDate);
        alert("Giriş Başarılı");  
        this.router.navigate(['']);
        
      },      
      (errorRes) => {
        if(errorRes.error.message !== undefined){
          alert(errorRes.error.message)
        }
        else{
          alert(errorRes.error)
        }
        window.location.reload();
      });
    }
    else{
      alert("Form boş bırakılamaz.");
    }
  }
}
