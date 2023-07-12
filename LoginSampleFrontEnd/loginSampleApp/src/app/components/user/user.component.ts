import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserModel } from 'src/app/models/userModel';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})

export class UserComponent implements OnInit{

  userModel : UserModel;
  isCompleted:boolean = false;

  constructor(private userService:UserService, private router:Router, private authService:AuthService) {}

  ngOnInit(): void {
    this.isCompleted = false;
    this.getUser();
  }

  getUser(){
    this.userService.getLoginedUser().subscribe((res) => {
      this.userModel = res.data;
      this.isCompleted = true;
    })    
  }

  delete(){
    this.userService.deleteUser(this.userModel.id).subscribe((res) => {
      alert(res.message);
      this.authService.logout();
      this.router.navigate(['main']);
    },
    (errorRes) => {
      alert(errorRes.error.message);
    })
  }
  
}
