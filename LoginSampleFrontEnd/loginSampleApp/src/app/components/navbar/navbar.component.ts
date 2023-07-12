import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
  export class NavbarComponent implements OnInit{
    isLogged: boolean = false;

    constructor(private authService:AuthService, private router:Router) {}
    
    async ngOnInit(): Promise<void> {
      this.authService.isAuthenticated().then((res) => {
        this.isLogged = res;   
    })
  }

  logout(){    
    this.authService.logout();
    if(this.router.url === '/home' ){
      location.reload();
    }
    this.router.navigate(['']);
  }
}
