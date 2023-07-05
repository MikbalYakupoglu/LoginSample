import { NgModule } from '@angular/core';
import { LoginComponent } from './components/login/login.component';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './components/register/register.component';
import { BrowserModule } from '@angular/platform-browser';
import { MainComponent } from './components/main/main.component';
import { UserComponent } from './components/user/user.component';
import { loginGuard } from './guards/login.guard';
import { NotFoundError } from 'rxjs';

const routes:Routes = [
  {path:"main", component: MainComponent},
  {path:"login", component:LoginComponent},
  {path:"register", component:RegisterComponent},
  {path:"user", component:UserComponent, canActivate:[loginGuard]},
  {path:'', redirectTo:'/main', pathMatch: 'full'},
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
