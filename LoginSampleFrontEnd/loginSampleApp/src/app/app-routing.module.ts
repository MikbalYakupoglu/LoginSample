import { NgModule } from '@angular/core';
import { LoginComponent } from './components/login/login.component';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './components/register/register.component';
import { BrowserModule } from '@angular/platform-browser';
import { UserComponent } from './components/user/user.component';
import { loginGuard } from './guards/login.guard';
import { NotFoundError } from 'rxjs';
import { alreadyLoginedGuard } from './guards/already-logined.guard';
import { HomeComponent } from './components/home/home.component';
import { CategoryComponent } from './components/category/category.component';
import { roleGuard } from './guards/role.guard';
import { AuthorizationRoles } from './heleprs/authorizationRoles';

const routes: Routes = [
  { path: "home", component: HomeComponent },
  { path: "login", component: LoginComponent, canActivate: [alreadyLoginedGuard] },
  { path: "register", component: RegisterComponent, canActivate: [alreadyLoginedGuard] },
  { path: "user", component: UserComponent, canActivate: [loginGuard] },
  { path: "categories", component: CategoryComponent, canActivate: [loginGuard, roleGuard], data: {roles: [AuthorizationRoles.Admin]} },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
