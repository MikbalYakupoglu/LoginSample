import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private authService:AuthService) { }

  isLogged: boolean;

  async ngOnInit(): Promise<void> {
    this.isLogged = await this.authService.isAuthenticated();
  }
}
