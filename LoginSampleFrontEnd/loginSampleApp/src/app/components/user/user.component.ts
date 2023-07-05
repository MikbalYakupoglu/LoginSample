import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserModel } from 'src/app/models/userModel';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})

export class UserComponent implements OnInit{

  userModel : UserModel;
  isCompleted:boolean = false;

  constructor(private userService:UserService, private router:Router) {}

  ngOnInit(): void {
    this.isCompleted = false;
    this.getUser();
  }

  getUser(){
    this.userService.getUser().subscribe((res) => {
      console.log(res.data);
      this.userModel = res.data;
      this.isCompleted = true;
    })    
  }

  remove(){
    this.userService.remove(this.userModel).subscribe((res) => {
      console.log(res);
      alert(res.message);
      this.userService.logout();
      this.router.navigate(['main']);
    },
    (errorRes) => {
      alert(errorRes.error.message);
    })
  }
  
}