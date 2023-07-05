import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl ,Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import {CookieService} from 'ngx-cookie-service';
import { sampleTime } from 'rxjs';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  loginForm : FormGroup;

  constructor(private formBuilder:FormBuilder, private authService:AuthService,
    private router: Router, private cookieService:CookieService) { }

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
        // let expireDate = new Date(res.data.expirationDate);
        // this.cookieService.set("token", res.data.token, expireDate);
        this.authService.writeTokenToCookie(res.data.token,res.data.expirationDate);
        alert("Giriş Başarılı");  
        this.router.navigate(['main']);
      },      
      (errorRes) => {
        alert(errorRes.error.message)
      });
    }
  }
}
