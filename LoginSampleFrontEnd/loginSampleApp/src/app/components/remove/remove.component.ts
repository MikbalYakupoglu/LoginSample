import { Component } from '@angular/core';
import { UserModel } from 'src/app/models/userModel';

@Component({
  selector: 'app-remove',
  templateUrl: './remove.component.html',
  styleUrls: ['./remove.component.css']
})
export class RemoveComponent {

  constructor() { }

  userModel:UserModel;


  remove(){
    
  }
}
